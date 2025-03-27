using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace user_program;

public class ReadPE
{
    // ========== DOS Header ==========
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct IMAGE_DOS_HEADER
    {
        public ushort e_magic;
        public ushort e_cblp, e_cp, e_crlc, e_cparhdr, e_minalloc, e_maxalloc;
        public ushort e_ss, e_sp, e_csum, e_ip, e_cs, e_lfarlc, e_ovno;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] e_res;
        public ushort e_oemid, e_oeminfo;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public ushort[] e_res2;
        public int e_lfanew;
    }

    // ========== PE File Header ==========
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct IMAGE_FILE_HEADER
    {
        public ushort Machine;
        public ushort NumberOfSections;
        public uint TimeDateStamp;
        public uint PointerToSymbolTable;
        public uint NumberOfSymbols;
        public ushort SizeOfOptionalHeader;
        public ushort Characteristics;
    }

    // ========== Data Directory ==========
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct IMAGE_DATA_DIRECTORY
    {
        public uint VirtualAddress;
        public uint Size;
    }

    // ========== Optional Header (32비트) ==========
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct IMAGE_OPTIONAL_HEADER32
    {
        public ushort Magic;
        public byte MajorLinkerVersion, MinorLinkerVersion;
        public uint SizeOfCode, SizeOfInitializedData, SizeOfUninitializedData;
        public uint AddressOfEntryPoint, BaseOfCode, BaseOfData;
        public uint ImageBase;
        public uint SectionAlignment, FileAlignment;
        public ushort MajorOperatingSystemVersion, MinorOperatingSystemVersion;
        public ushort MajorImageVersion, MinorImageVersion;
        public ushort MajorSubsystemVersion, MinorSubsystemVersion;
        public uint Win32VersionValue, SizeOfImage, SizeOfHeaders, CheckSum;
        public ushort Subsystem, DllCharacteristics;
        public uint SizeOfStackReserve, SizeOfStackCommit, SizeOfHeapReserve, SizeOfHeapCommit;
        public uint LoaderFlags, NumberOfRvaAndSizes;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public IMAGE_DATA_DIRECTORY[] DataDirectory;
    }

    // ========== Optional Header (64비트) ==========
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct IMAGE_OPTIONAL_HEADER64
    {
        public ushort Magic;
        public byte MajorLinkerVersion, MinorLinkerVersion;
        public uint SizeOfCode, SizeOfInitializedData, SizeOfUninitializedData;
        public uint AddressOfEntryPoint, BaseOfCode;
        public ulong ImageBase;
        public uint SectionAlignment, FileAlignment;
        public ushort MajorOperatingSystemVersion, MinorOperatingSystemVersion;
        public ushort MajorImageVersion, MinorImageVersion;
        public ushort MajorSubsystemVersion, MinorSubsystemVersion;
        public uint Win32VersionValue, SizeOfImage, SizeOfHeaders, CheckSum;
        public ushort Subsystem, DllCharacteristics;
        public ulong SizeOfStackReserve, SizeOfStackCommit, SizeOfHeapReserve, SizeOfHeapCommit;
        public uint LoaderFlags, NumberOfRvaAndSizes;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public IMAGE_DATA_DIRECTORY[] DataDirectory;
    }

    // ========== Section Header ==========
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct IMAGE_SECTION_HEADER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Name;
        public uint VirtualSize, VirtualAddress, SizeOfRawData, PointerToRawData;
        public uint PointerToRelocations, PointerToLinenumbers;
        public ushort NumberOfRelocations, NumberOfLinenumbers;
        public uint Characteristics;
    }

    public static string[] ReadPEHeader(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        using (BinaryReader reader = new BinaryReader(fs))
        {
            //DOS Header 읽기
            IMAGE_DOS_HEADER dosHeader = ReadStruct<IMAGE_DOS_HEADER>(reader);

            if (dosHeader.e_magic != 0x5A4D)
            {

                Console.WriteLine("올바른 PE 파일이 아닙니다.");
                return null;
            }

            //PE Header 위치로 이동
            fs.Seek(dosHeader.e_lfanew, SeekOrigin.Begin);
            uint peSignature = reader.ReadUInt32();

            if (peSignature != 0x00004550)
            {

                Console.WriteLine("PE 헤더가 없습니다.");
                return null;
            }

            //PE File Header 읽기
            IMAGE_FILE_HEADER fileHeader = ReadStruct<IMAGE_FILE_HEADER>(reader);

            //Optional Header 확인 (32비트 vs 64비트)
            bool is64Bit = reader.ReadUInt16() == 0x20B;
            fs.Seek(-2, SeekOrigin.Current);

            IMAGE_DATA_DIRECTORY[] dataDirectories;
            List<string> optionalData64 = new List<string>();
            List<string> optionalData32 = new List<string>();

            if (is64Bit)
            {
                //Optional Header 읽기 (64 비트)
                IMAGE_OPTIONAL_HEADER64 optionalHeader = ReadStruct<IMAGE_OPTIONAL_HEADER64>(reader);

                //csv 저장
                optionalData64.AddRange(new string[] {
                    $"{optionalHeader.Magic}", $"{optionalHeader.MajorLinkerVersion}", $"{optionalHeader.MinorLinkerVersion}", $"{optionalHeader.SizeOfCode}", $"{optionalHeader.SizeOfInitializedData}", $"{optionalHeader.SizeOfUninitializedData}",
                    $"{optionalHeader.AddressOfEntryPoint}", $"{optionalHeader.BaseOfCode}", $"{null}" , $"{optionalHeader.ImageBase}", $"{optionalHeader.SectionAlignment}", $"{optionalHeader.FileAlignment}", $"{optionalHeader.MajorOperatingSystemVersion}",
                    $"{optionalHeader.MinorOperatingSystemVersion}", $"{optionalHeader.MajorImageVersion}", $"{optionalHeader.MinorImageVersion}", $"{optionalHeader.MajorSubsystemVersion}", $"{optionalHeader.MinorSubsystemVersion}",
                    $"{optionalHeader.Win32VersionValue}", $"{optionalHeader.SizeOfImage}", $"{optionalHeader.SizeOfHeaders}", $"{optionalHeader.CheckSum}", $"{optionalHeader.Subsystem}", $"{optionalHeader.DllCharacteristics}", $"{optionalHeader.SizeOfStackReserve}",
                    $"{optionalHeader.SizeOfStackCommit}", $"{optionalHeader.SizeOfHeapReserve}", $"{optionalHeader.SizeOfHeapCommit}", $"{optionalHeader.LoaderFlags}", $"{optionalHeader.NumberOfRvaAndSizes}"
                });
                dataDirectories = optionalHeader.DataDirectory;
            }
            else
            {
                //Optional Header 읽기 (32 비트)
                IMAGE_OPTIONAL_HEADER32 optionalHeader = ReadStruct<IMAGE_OPTIONAL_HEADER32>(reader);

                //csv 저장
                optionalData32.AddRange(new string[] {
                    $"{optionalHeader.Magic}", $"{optionalHeader.MajorLinkerVersion}", $"{optionalHeader.MinorLinkerVersion}", $"{optionalHeader.SizeOfCode}", $"{optionalHeader.SizeOfInitializedData}", $"{optionalHeader.SizeOfUninitializedData}",
                    $"{optionalHeader.AddressOfEntryPoint}", $"{optionalHeader.BaseOfCode}", $"{optionalHeader.BaseOfData}", $"{optionalHeader.ImageBase}", $"{optionalHeader.SectionAlignment}", $"{optionalHeader.FileAlignment}", $"{optionalHeader.MajorOperatingSystemVersion}",
                    $"{optionalHeader.MinorOperatingSystemVersion}", $"{optionalHeader.MajorImageVersion}", $"{optionalHeader.MinorImageVersion}", $"{optionalHeader.MajorSubsystemVersion}", $"{optionalHeader.MinorSubsystemVersion}",
                    $"{optionalHeader.Win32VersionValue}", $"{optionalHeader.SizeOfImage}", $"{optionalHeader.SizeOfHeaders}", $"{optionalHeader.CheckSum}", $"{optionalHeader.Subsystem}", $"{optionalHeader.DllCharacteristics}", $"{optionalHeader.SizeOfStackReserve}",
                    $"{optionalHeader.SizeOfStackCommit}", $"{optionalHeader.SizeOfHeapReserve}", $"{optionalHeader.SizeOfHeapCommit}", $"{optionalHeader.LoaderFlags}", $"{optionalHeader.NumberOfRvaAndSizes}"
                });

                dataDirectories = optionalHeader.DataDirectory;
            }

            // Section Headers 읽기
            List<string> sectionData = new List<string>();
            int sectionCount = Math.Min((int)fileHeader.NumberOfSections, 10); // ushort → int 변환

            for (int i = 0; i < sectionCount; i++)
            {
                IMAGE_SECTION_HEADER section = ReadStruct<IMAGE_SECTION_HEADER>(reader);
                string sectionName = Encoding.ASCII.GetString(section.Name).TrimEnd('\0');

                //csv 저장
                sectionData.AddRange(new string[] {
                    sectionName, $"{section.VirtualSize}", $"{section.VirtualAddress}",
                    $"{section.SizeOfRawData}", $"{section.PointerToRawData}",
                    $"{section.PointerToRelocations}", $"{section.PointerToLinenumbers}",
                    $"{section.NumberOfRelocations}", $"{section.NumberOfLinenumbers}",
                    $"{section.Characteristics}"
                });
            }
            while (sectionData.Count < 11) sectionData.Add("");

            if (is64Bit)
            {
                //csv 총합 저장 (64 비트)
                return new string[] {
                filePath,
                $"{dosHeader.e_magic}", $"{dosHeader.e_cblp}", $"{dosHeader.e_cp}", $"{dosHeader.e_crlc}", $"{dosHeader.e_cparhdr}", $"{dosHeader.e_minalloc}", $"{dosHeader.e_maxalloc}", $"{dosHeader.e_ss}",
                $"{dosHeader.e_sp}", $"{dosHeader.e_csum}", $"{dosHeader.e_ip}", $"{dosHeader.e_cs}", $"{dosHeader.e_lfarlc}", $"{dosHeader.e_ovno}", $"{dosHeader.e_res[0]}", $"{dosHeader.e_res[1]}", $"{dosHeader.e_res[2]}", $"{dosHeader.e_res[3]}",
                $"{dosHeader.e_oemid}", $"{dosHeader.e_oeminfo}", $"{dosHeader.e_res2[0]}", $"{dosHeader.e_res2[1]}", $"{dosHeader.e_res2[2]}", $"{dosHeader.e_res2[3]}", $"{dosHeader.e_res2[4]}", $"{dosHeader.e_res2[5]}", $"{dosHeader.e_res2[6]}",
                $"{dosHeader.e_res2[7]}", $"{dosHeader.e_res2[8]}", $"{dosHeader.e_res2[9]}", $"{dosHeader.e_lfanew}",
                $"{fileHeader.Machine}", $"{fileHeader.NumberOfSections}", $"{fileHeader.TimeDateStamp}", $"{fileHeader.PointerToSymbolTable}", $"{fileHeader.NumberOfSymbols}", $"{fileHeader.SizeOfOptionalHeader}", $"{fileHeader.Characteristics}"
                }
                .Concat(optionalData64)
                .Concat(dataDirectories.SelectMany(d => new[] { $"{d.VirtualAddress}", $"{d.Size}" }))
                .Concat(sectionData)
                .ToArray();

            }
            else
            {
                //csv 총합 저장 (32 비트)
                return new string[] {
                filePath,
                $"{dosHeader.e_magic}", $"{dosHeader.e_cblp}", $"{dosHeader.e_cp}", $"{dosHeader.e_crlc}", $"{dosHeader.e_cparhdr}", $"{dosHeader.e_minalloc}", $"{dosHeader.e_maxalloc}", $"{dosHeader.e_ss}",
                $"{dosHeader.e_sp}", $"{dosHeader.e_csum}", $"{dosHeader.e_ip}", $"{dosHeader.e_cs}", $"{dosHeader.e_lfarlc}", $"{dosHeader.e_ovno}", $"{dosHeader.e_res[0]}", $"{dosHeader.e_res[1]}", $"{dosHeader.e_res[2]}", $"{dosHeader.e_res[3]}",
                $"{dosHeader.e_oemid}", $"{dosHeader.e_oeminfo}", $"{dosHeader.e_res2[0]}", $"{dosHeader.e_res2[1]}", $"{dosHeader.e_res2[2]}", $"{dosHeader.e_res2[3]}", $"{dosHeader.e_res2[4]}", $"{dosHeader.e_res2[5]}", $"{dosHeader.e_res2[6]}",
                $"{dosHeader.e_res2[7]}", $"{dosHeader.e_res2[8]}", $"{dosHeader.e_res2[9]}", $"{dosHeader.e_lfanew}",
                $"{fileHeader.Machine}", $"{fileHeader.NumberOfSections}", $"{fileHeader.TimeDateStamp}", $"{fileHeader.PointerToSymbolTable}", $"{fileHeader.NumberOfSymbols}", $"{fileHeader.SizeOfOptionalHeader}", $"{fileHeader.Characteristics}"
                }
                .Concat(optionalData32)
                .Concat(dataDirectories.SelectMany(d => new[] { $"{d.VirtualAddress}", $"{d.Size}" }))
                .Concat(sectionData)
                .ToArray();
            }

        }
    }

    static T ReadStruct<T>(BinaryReader reader) where T : struct
    {
        byte[] bytes = reader.ReadBytes(Marshal.SizeOf<T>());
        GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        T structure = Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
        handle.Free();
        return structure;
    }
}