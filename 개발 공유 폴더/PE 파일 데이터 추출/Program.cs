    using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Linq;

    class PEHeaderReader
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

        static void Main(string[] args)
        {
            string targetDirectory = @"C:\"; // 전체 드라이브 검색
            string outputCsv = "pe_info.csv";
            List<string[]> csvData = new List<string[]>();

            // CSV 헤더 정의 (모든 필드 추가)
            csvData.Add(new string[]
            {
                "File Path",
                // DOS Header
                "e_magic", "e_cblp", "e_cp", "e_crlc", "e_cparhdr", "e_minalloc", "e_maxalloc",
                "e_ss", "e_sp", "e_csum", "e_ip", "e_cs", "e_lfarlc", "e_ovno", "e_oemid", "e_oeminfo", "e_lfanew",

                // PE File Header
                "Machine", "NumberOfSections", "TimeDateStamp", "PointerToSymbolTable",
                "NumberOfSymbols", "SizeOfOptionalHeader", "Characteristics",

                // Optional Header (32bit / 64bit 자동 선택)
                "Magic", "MajorLinkerVersion", "MinorLinkerVersion", "SizeOfCode",
                "SizeOfInitializedData", "SizeOfUninitializedData", "AddressOfEntryPoint",
                "BaseOfCode", "BaseOfData", "ImageBase", "SectionAlignment", "FileAlignment",
                "MajorOperatingSystemVersion", "MinorOperatingSystemVersion",
                "MajorImageVersion", "MinorImageVersion", "MajorSubsystemVersion", "MinorSubsystemVersion",
                "SizeOfImage", "SizeOfHeaders", "CheckSum", "Subsystem", "DllCharacteristics",

                // Data Directory (16개 모든 항목 저장)
                "Export Table VA", "Export Table Size", "Import Table VA", "Import Table Size",
                "Resource Table VA", "Resource Table Size", "Exception Table VA", "Exception Table Size",
                "Certificate Table VA", "Certificate Table Size", "Base Relocation Table VA", "Base Relocation Table Size",
                "Debug VA", "Debug Size", "Architecture VA", "Architecture Size",
                "Global Ptr VA", "Global Ptr Size", "TLS Table VA", "TLS Table Size",
                "Load Config Table VA", "Load Config Table Size", "Bound Import VA", "Bound Import Size",
                "IAT VA", "IAT Size", "Delay Import Descriptor VA", "Delay Import Descriptor Size",
                "CLR Runtime Header VA", "CLR Runtime Header Size",

                // Section Headers (최대 5개 저장)
                "Section1 Name", "Section1 Virtual Address", "Section1 Virtual Size", "Section1 Raw Size", "Section1 Raw Offset",
                "Section2 Name", "Section2 Virtual Address", "Section2 Virtual Size", "Section2 Raw Size", "Section2 Raw Offset",
                "Section3 Name", "Section3 Virtual Address", "Section3 Virtual Size", "Section3 Raw Size", "Section3 Raw Offset",
                "Section4 Name", "Section4 Virtual Address", "Section4 Virtual Size", "Section4 Raw Size", "Section4 Raw Offset",
                "Section5 Name", "Section5 Virtual Address", "Section5 Virtual Size", "Section5 Raw Size", "Section5 Raw Offset"
            });

            Console.WriteLine("PE 파일 검색 중...");

            foreach (string file in GetFilesSafely(targetDirectory, new[] { "*.exe", "*.dll" }))
            {
                try
                {
                    Console.WriteLine($"분석 중: {file}");
                    var peInfo = ReadPEHeader(file);
                    if (peInfo != null)
                    {
                        csvData.Add(peInfo);
                    }
                }
                catch
                {
                    Console.WriteLine($"[오류] 파일 접근 실패: {file}");
                }
            }

            File.WriteAllLines(outputCsv, csvData.ConvertAll(line => string.Join(",", line)));
            Console.WriteLine($"CSV 저장 완료: {outputCsv}");
        }

        // 🛠 폴더 내 모든 PE 파일을 검색하는 기능 추가
        static IEnumerable<string> GetFilesSafely(string rootDirectory, string[] searchPatterns)
        {
            Stack<string> directories = new Stack<string>();
            directories.Push(rootDirectory);

            while (directories.Count > 0)
            {
                string currentDir = directories.Pop();

                try
                {
                    foreach (string subDir in Directory.GetDirectories(currentDir))
                    {
                        directories.Push(subDir);
                    }
                }
                catch { continue; }

                foreach (string pattern in searchPatterns)
                {
                    string[] files = Array.Empty<string>();

                    try { files = Directory.GetFiles(currentDir, pattern); }
                    catch { continue; }

                    foreach (string file in files)
                    {
                        yield return file;
                    }
                }
            }
        }

        static string[] ReadPEHeader(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                IMAGE_DOS_HEADER dosHeader = ReadStruct<IMAGE_DOS_HEADER>(reader);
                if (dosHeader.e_magic != 0x5A4D) return null;

                fs.Seek(dosHeader.e_lfanew, SeekOrigin.Begin);
                uint peSignature = reader.ReadUInt32();
                if (peSignature != 0x00004550) return null;

                IMAGE_FILE_HEADER fileHeader = ReadStruct<IMAGE_FILE_HEADER>(reader);
                bool is64Bit = reader.ReadUInt16() == 0x20B;
                fs.Seek(-2, SeekOrigin.Current);

                IMAGE_DATA_DIRECTORY[] dataDirectories;
                uint entryPoint, imageBase, sizeOfImage;
                ushort subsystem, dllCharacteristics;

                if (is64Bit)
                {
                    IMAGE_OPTIONAL_HEADER64 optHeader = ReadStruct<IMAGE_OPTIONAL_HEADER64>(reader);
                    entryPoint = optHeader.AddressOfEntryPoint;
                    imageBase = (uint)optHeader.ImageBase;
                    sizeOfImage = optHeader.SizeOfImage;
                    subsystem = optHeader.Subsystem;
                    dllCharacteristics = optHeader.DllCharacteristics;
                    dataDirectories = optHeader.DataDirectory;
                }
                else
                {
                    IMAGE_OPTIONAL_HEADER32 optHeader = ReadStruct<IMAGE_OPTIONAL_HEADER32>(reader);
                    entryPoint = optHeader.AddressOfEntryPoint;
                    imageBase = optHeader.ImageBase;
                    sizeOfImage = optHeader.SizeOfImage;
                    subsystem = optHeader.Subsystem;
                    dllCharacteristics = optHeader.DllCharacteristics;
                    dataDirectories = optHeader.DataDirectory;
                }

                // Section Headers (최대 2개만 예제용으로 저장)
                List<string> sectionData = new List<string>();
                int sectionCount = Math.Min((int)fileHeader.NumberOfSections, 2); // ushort → int 변환

                for (int i = 0; i < sectionCount; i++)
                {
                    IMAGE_SECTION_HEADER section = ReadStruct<IMAGE_SECTION_HEADER>(reader);
                    string sectionName = Encoding.ASCII.GetString(section.Name).TrimEnd('\0');
                    sectionData.AddRange(new string[]
                    {
                        sectionName, $"{section.VirtualAddress}", $"{section.VirtualSize}",
                        $"{section.SizeOfRawData}", $"{section.PointerToRawData}"
                    });
                }
                while (sectionData.Count < 10) sectionData.Add("");

                return new string[]
                {
                    filePath, $"{dosHeader.e_magic}", $"{dosHeader.e_lfanew}",
                    $"{fileHeader.Machine}", $"{fileHeader.NumberOfSections}", $"{fileHeader.TimeDateStamp}",
                    $"{fileHeader.PointerToSymbolTable}", $"{fileHeader.NumberOfSymbols}",
                    $"{fileHeader.SizeOfOptionalHeader}", $"{fileHeader.Characteristics}", $"{entryPoint}",
                    $"{imageBase}", $"{sizeOfImage}", $"{subsystem}", $"{dllCharacteristics}"
                }
                .Concat(dataDirectories.SelectMany(d => new[] { $"{d.VirtualAddress}", $"{d.Size}" }))
                .Concat(sectionData)
                .ToArray();
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