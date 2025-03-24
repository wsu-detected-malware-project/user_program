using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

class PEHeaderReader {
    // ========== DOS Header ==========
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct IMAGE_DOS_HEADER {
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
    struct IMAGE_FILE_HEADER {
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
    struct IMAGE_DATA_DIRECTORY {
        public uint VirtualAddress;
        public uint Size;
    }

    // ========== Optional Header (32비트) ==========
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct IMAGE_OPTIONAL_HEADER32 {
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
    struct IMAGE_OPTIONAL_HEADER64 {
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
    struct IMAGE_SECTION_HEADER {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Name;
        public uint VirtualSize, VirtualAddress, SizeOfRawData, PointerToRawData;
        public uint PointerToRelocations, PointerToLinenumbers;
        public ushort NumberOfRelocations, NumberOfLinenumbers;
        public uint Characteristics;
    }

    static void Main(string[] args) {

        if (args.Length < 1) {
            Console.WriteLine("사용법: PEHeaderReader.exe <파일 경로>");
            return;
        }

        string filePath = args[0];
        List<string> CsvFile = new List<string>();

        try {
            
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs)) {
                //DOS Header 읽기
                IMAGE_DOS_HEADER dosHeader = ReadStruct<IMAGE_DOS_HEADER>(reader);
                if (dosHeader.e_magic != 0x5A4D) {

                    Console.WriteLine("올바른 PE 파일이 아닙니다.");
                    return;
                }

                // DOS Header csv 저장
                CsvFile.Add($"{dosHeader.e_magic}");
                CsvFile.Add($"{dosHeader.e_cblp}");
                CsvFile.Add($"{dosHeader.e_cp}");
                CsvFile.Add($"{dosHeader.e_crlc}");
                CsvFile.Add($"{dosHeader.e_cparhdr}");
                CsvFile.Add($"{dosHeader.e_minalloc}");
                CsvFile.Add($"{dosHeader.e_maxalloc}");
                CsvFile.Add($"{dosHeader.e_ss}");
                CsvFile.Add($"{dosHeader.e_sp}");
                CsvFile.Add($"{dosHeader.e_csum}");
                CsvFile.Add($"{dosHeader.e_ip}");
                CsvFile.Add($"{dosHeader.e_cs}");
                CsvFile.Add($"{dosHeader.e_lfarlc}");
                CsvFile.Add($"{dosHeader.e_ovno}");
                for (int i = 0; i< dosHeader.e_res.Length; i++) {
                    CsvFile.Add($"0x{dosHeader.e_res[i]}");
                }
                CsvFile.Add($"0x{dosHeader.e_oemid}");
                CsvFile.Add($"0x{dosHeader.e_oeminfo}");
                for (int i = 0; i< dosHeader.e_res2.Length; i++) {
                    CsvFile.Add($"0x{dosHeader.e_res2[i]}");
                }
                CsvFile.Add($"0x{dosHeader.e_lfanew}");

                //PE Header 위치로 이동
                fs.Seek(dosHeader.e_lfanew, SeekOrigin.Begin);
                uint peSignature = reader.ReadUInt32();

                if (peSignature != 0x00004550) {// 'PE\0\0' 체크
                
                    Console.WriteLine("PE 헤더가 없습니다.");
                    return;
                }

                //PE File Header 읽기
                IMAGE_FILE_HEADER fileHeader = ReadStruct<IMAGE_FILE_HEADER>(reader);

                //PE 헤더 출력
                Console.WriteLine("=== PE 헤더 정보 ===");
                Console.WriteLine($"Machine: 0x{fileHeader.Machine:X4}");
                Console.WriteLine($"Number of Sections: {fileHeader.NumberOfSections}");
                Console.WriteLine($"Timestamp: {fileHeader.TimeDateStamp}");
                Console.WriteLine($"Size of Optional Header: {fileHeader.SizeOfOptionalHeader}");
                Console.WriteLine($"Characteristics: 0x{fileHeader.Characteristics:X4}");

                // PE File Header csv 저장
                CsvFile.Add($"{fileHeader.Machine}");
                CsvFile.Add($"{fileHeader.NumberOfSections}");
                CsvFile.Add($"{fileHeader.TimeDateStamp}");
                CsvFile.Add($"{fileHeader.PointerToSymbolTable}");
                CsvFile.Add($"{fileHeader.NumberOfSymbols}");
                CsvFile.Add($"{fileHeader.SizeOfOptionalHeader}");
                CsvFile.Add($"{fileHeader.Characteristics}");

                //Optional Header 확인 (32비트 vs 64비트)
                bool is64Bit = false;
                ushort optionalHeaderMagic = reader.ReadUInt16();
                fs.Seek(-2, SeekOrigin.Current);
                if (optionalHeaderMagic == 0x20B) is64Bit = true;
                
                IMAGE_DATA_DIRECTORY[] dataDirectories;

                if(is64Bit) {
                     //Optional Header 읽기 (64비트 기준)
                    IMAGE_OPTIONAL_HEADER64 optionalHeader = ReadStruct<IMAGE_OPTIONAL_HEADER64>(reader);

                    //Optional Header 출력
                    Console.WriteLine("\n=== Optional Header ===");
                    Console.WriteLine($"Magic: 0x{optionalHeader.Magic:X4}");
                    Console.WriteLine($"Entry Point: 0x{optionalHeader.AddressOfEntryPoint:X8}");
                    Console.WriteLine($"Image Base: 0x{optionalHeader.ImageBase:X8}");
                    Console.WriteLine($"Section Alignment: {optionalHeader.SectionAlignment}");
                    Console.WriteLine($"File Alignment: {optionalHeader.FileAlignment}");
                    Console.WriteLine($"Size of Image: {optionalHeader.SizeOfImage}");
                    Console.WriteLine($"Subsystem: {optionalHeader.Subsystem}");

                    //Option Header (64비트 기준) csv 저장
                    CsvFile.Add($"{optionalHeader.Magic}");
                    CsvFile.Add($"{optionalHeader.MajorLinkerVersion}");
                    CsvFile.Add($"{optionalHeader.MinorLinkerVersion}");
                    CsvFile.Add($"{optionalHeader.SizeOfCode}");
                    CsvFile.Add($"{optionalHeader.SizeOfInitializedData}");
                    CsvFile.Add($"{optionalHeader.SizeOfUninitializedData}");
                    CsvFile.Add($"{optionalHeader.AddressOfEntryPoint}");
                    CsvFile.Add($"{optionalHeader.BaseOfCode}");
                    CsvFile.Add($"{optionalHeader.ImageBase}");
                    CsvFile.Add($"{optionalHeader.SectionAlignment}");
                    CsvFile.Add($"{optionalHeader.FileAlignment}");
                    CsvFile.Add($"{optionalHeader.MajorOperatingSystemVersion}");
                    CsvFile.Add($"{optionalHeader.MinorOperatingSystemVersion}");
                    CsvFile.Add($"{optionalHeader.MajorImageVersion}");
                    CsvFile.Add($"{optionalHeader.MinorImageVersion}");
                    CsvFile.Add($"{optionalHeader.MajorSubsystemVersion}");
                    CsvFile.Add($"{optionalHeader.MinorSubsystemVersion}");
                    CsvFile.Add($"{optionalHeader.Win32VersionValue}");
                    CsvFile.Add($"{optionalHeader.SizeOfImage}");
                    CsvFile.Add($"{optionalHeader.SizeOfHeaders}");
                    CsvFile.Add($"{optionalHeader.CheckSum}");
                    CsvFile.Add($"{optionalHeader.Subsystem}");
                    CsvFile.Add($"{optionalHeader.DllCharacteristics}");
                    CsvFile.Add($"{optionalHeader.SizeOfStackReserve}");
                    CsvFile.Add($"{optionalHeader.SizeOfStackCommit}");
                    CsvFile.Add($"{optionalHeader.SizeOfHeapReserve}");
                    CsvFile.Add($"{optionalHeader.SizeOfHeapCommit}");
                    CsvFile.Add($"{optionalHeader.LoaderFlags}");
                    CsvFile.Add($"{optionalHeader.NumberOfRvaAndSizes}");

                    dataDirectories = optionalHeader.DataDirectory;
                    
                }
                else {
                    //Optional Header 읽기 (32비트 기준)
                    IMAGE_OPTIONAL_HEADER32 optionalHeader = ReadStruct<IMAGE_OPTIONAL_HEADER32>(reader);

                    //Optional Header 출력
                    Console.WriteLine("\n=== Optional Header ===");
                    Console.WriteLine($"Magic: 0x{optionalHeader.Magic:X4}");
                    Console.WriteLine($"Entry Point: 0x{optionalHeader.AddressOfEntryPoint:X8}");
                    Console.WriteLine($"Image Base: 0x{optionalHeader.ImageBase:X8}");
                    Console.WriteLine($"Section Alignment: {optionalHeader.SectionAlignment}");
                    Console.WriteLine($"File Alignment: {optionalHeader.FileAlignment}");
                    Console.WriteLine($"Size of Image: {optionalHeader.SizeOfImage}");
                    Console.WriteLine($"Subsystem: {optionalHeader.Subsystem}");

                    //Option Header (32비트 기준) csv 저장
                    CsvFile.Add($"{optionalHeader.Magic}");
                    CsvFile.Add($"{optionalHeader.MajorLinkerVersion}");
                    CsvFile.Add($"{optionalHeader.MinorLinkerVersion}");
                    CsvFile.Add($"{optionalHeader.SizeOfCode}");
                    CsvFile.Add($"{optionalHeader.SizeOfInitializedData}");
                    CsvFile.Add($"{optionalHeader.SizeOfUninitializedData}");
                    CsvFile.Add($"{optionalHeader.AddressOfEntryPoint}");
                    CsvFile.Add($"{optionalHeader.BaseOfCode}");
                    CsvFile.Add($"{optionalHeader.BaseOfData}");
                    CsvFile.Add($"{optionalHeader.ImageBase}");
                    CsvFile.Add($"{optionalHeader.SectionAlignment}");
                    CsvFile.Add($"{optionalHeader.FileAlignment}");
                    CsvFile.Add($"{optionalHeader.MajorOperatingSystemVersion}");
                    CsvFile.Add($"{optionalHeader.MinorOperatingSystemVersion}");
                    CsvFile.Add($"{optionalHeader.MajorImageVersion}");
                    CsvFile.Add($"{optionalHeader.MinorImageVersion}");
                    CsvFile.Add($"{optionalHeader.MajorSubsystemVersion}");
                    CsvFile.Add($"{optionalHeader.MinorSubsystemVersion}");
                    CsvFile.Add($"{optionalHeader.Win32VersionValue}");
                    CsvFile.Add($"{optionalHeader.SizeOfImage}");
                    CsvFile.Add($"{optionalHeader.SizeOfHeaders}");
                    CsvFile.Add($"{optionalHeader.CheckSum}");
                    CsvFile.Add($"{optionalHeader.Subsystem}");
                    CsvFile.Add($"{optionalHeader.DllCharacteristics}");
                    CsvFile.Add($"{optionalHeader.SizeOfStackReserve}");
                    CsvFile.Add($"{optionalHeader.SizeOfStackCommit}");
                    CsvFile.Add($"{optionalHeader.SizeOfHeapReserve}");
                    CsvFile.Add($"{optionalHeader.SizeOfHeapCommit}");
                    CsvFile.Add($"{optionalHeader.LoaderFlags}");
                    CsvFile.Add($"{optionalHeader.NumberOfRvaAndSizes}");

                    dataDirectories = optionalHeader.DataDirectory;
                }

                //Data Directory 출력
                string[] directoryNames = {
                    "Export Table", "Import Table", "Resource Table", "Exception Table",
                    "Certificate Table", "Base Relocation Table", "Debug", "Architecture",
                    "Global Ptr", "TLS Table", "Load Config Table", "Bound Import",
                    "IAT", "Delay Import Descriptor", "CLR Runtime Header", "Reserved"
                };

                Console.WriteLine("\n=== Data Directory ===");
                for (int i = 0; i < dataDirectories.Length; i++) {
                    Console.WriteLine($"{directoryNames[i]} -> VA: 0x{dataDirectories[i].VirtualAddress:X8}, Size: {dataDirectories[i].Size}");
                    
                    //Data Directory cvs 저장
                    CsvFile.Add($"{dataDirectories[i].VirtualAddress}");
                    CsvFile.Add($"{dataDirectories[i].Size}");
                }  

                //Section Headers 읽기
                IMAGE_SECTION_HEADER[] sections = new IMAGE_SECTION_HEADER[fileHeader.NumberOfSections];

                for (int i = 0; i < fileHeader.NumberOfSections; i++) {

                    sections[i] = ReadStruct<IMAGE_SECTION_HEADER>(reader);

                }

                //섹션 정보 출력
                Console.WriteLine("\n=== Section Headers ===");
                foreach (var section in sections) {

                    string sectionName = System.Text.Encoding.ASCII.GetString(section.Name).TrimEnd('\0');
                    Console.WriteLine($"[{sectionName}]");
                    Console.WriteLine($"  Virtual Address: 0x{section.VirtualAddress:X8}");
                    Console.WriteLine($"  Virtual Size: {section.VirtualSize}");
                    Console.WriteLine($"  Raw Data Size: {section.SizeOfRawData}");
                    Console.WriteLine($"  Raw Data Offset: 0x{section.PointerToRawData:X8}");
                    Console.WriteLine($"  Characteristics: 0x{section.Characteristics:X8}\n");

                    //Section Header csv 저장
                    CsvFile.Add($"{sectionName}");
                    CsvFile.Add($"{section.VirtualSize}");
                    CsvFile.Add($"{section.VirtualAddress}");
                    CsvFile.Add($"{section.SizeOfRawData}");
                    CsvFile.Add($"{section.PointerToRawData}");
                    CsvFile.Add($"{section.PointerToRelocations}");
                    CsvFile.Add($"{section.PointerToLinenumbers}");
                    CsvFile.Add($"{section.NumberOfRelocations}");
                    CsvFile.Add($"{section.NumberOfLinenumbers}");
                    CsvFile.Add($"{section.Characteristics}");
                }

                 File.WriteAllText("pe_header.csv", string.Join(",", CsvFile));
                 Console.WriteLine("PE 정보 저장 완료");
            }
        }

        catch (Exception ex) {
            Console.WriteLine($"오류 발생: {ex.Message}");
        }
    }

    static T ReadStruct<T>(BinaryReader reader) where T : struct {

        int size = Marshal.SizeOf<T>();
        byte[] bytes = reader.ReadBytes(size);
        GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        T structure = Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
        handle.Free();
        return structure;
    }
}
