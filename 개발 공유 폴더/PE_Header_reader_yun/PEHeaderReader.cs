using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

class PEHeaderReader
{
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
    struct IMAGE_DATA_DIRECTORY{
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

    static void Main(string[] args)
    {
        string outputCsv32 = "pe_info_32bit.csv";
        string outputCsv64 = "pe_info_64bit.csv";

        var csvData32 = new ConcurrentBag<string[]>();
        var csvData64 = new ConcurrentBag<string[]>();

        Console.WriteLine("PE 파일 검색 중...");

        // 사용 가능한 드라이브만 필터링
        var drives = DriveInfo.GetDrives()
            .Where(d => d.IsReady && Directory.Exists(d.RootDirectory.FullName))
            .Select(d => d.RootDirectory.FullName)
            .ToList();

        // 병렬 처리로 각 드라이브를 탐색
        Parallel.ForEach(drives, drive =>
        {
            Console.WriteLine($"Searching in: {drive}");

            foreach (var file in GetFilesSafely(drive, new[] { "*.exe", "*.dll", "*.sys", "*.ocx", "*.cpl", "*.drv" }))
            {
                // 파일 처리 로직
                Console.WriteLine($"Found file: {file}");
                // PE 파일 헤더를 읽고 CSV에 저장
                if (TryReadPEHeader(file, out var peInfo, out var is64Bit))
                {
                    if (is64Bit)
                    {
                        csvData64.Add(peInfo); // 64비트 파일
                    }
                    else
                    {
                        csvData32.Add(peInfo); // 32비트 파일
                    }
                }
            }
        });

        // CSV 파일로 저장
        SaveCsv32(outputCsv32, csvData32);
        SaveCsv64(outputCsv64, csvData64);
    }

    // 32비트 파일 데이터 저장
    static void SaveCsv32(string filename, ConcurrentBag<string[]> data)
    {
        if (data.Count == 0)
        {
            Console.WriteLine($"[Warning] No data to save for {filename}");
            return;
        }

        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("File Path, DOS Magic, DOS Header, File Header, Optional Header, Sections");

                foreach (var line in data)
                {
                    writer.WriteLine(string.Join(",", line));
                }
            }
            Console.WriteLine($"CSV 저장 완료: {filename}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] Failed to save CSV file: {ex.Message}");
        }
    }

    // 64비트 파일 데이터 저장
    static void SaveCsv64(string filename, ConcurrentBag<string[]> data)
    {
        if (data.Count == 0)
        {
            Console.WriteLine($"[Warning] No data to save for {filename}");
            return;
        }

        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("File Path, DOS Magic, DOS Header, File Header, Optional Header, Sections");

                foreach (var line in data)
                {
                    writer.WriteLine(string.Join(",", line));
                }
            }
            Console.WriteLine($"CSV 저장 완료: {filename}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] Failed to save CSV file: {ex.Message}");
        }
    }

    static IEnumerable<string> GetFilesSafely(string rootDirectory, string[] searchPatterns)
    {
        // 디렉터리가 존재하는지 확인
        if (!Directory.Exists(rootDirectory))
        {
            Console.WriteLine($"[Warning] Directory not found: {rootDirectory}");
            yield break;  // 디렉터리가 없으면 탐색을 중단
        }

        Stack<string> directories = new Stack<string>();
        directories.Push(rootDirectory);

        while (directories.Count > 0)
        {
            string currentDir = directories.Pop();

            try
            {
                foreach (string subDir in Directory.GetDirectories(currentDir))
                    directories.Push(subDir);
            }
            catch (UnauthorizedAccessException) { continue; }

            foreach (string pattern in searchPatterns)
            {
                string[] files = Array.Empty<string>();

                try { files = Directory.GetFiles(currentDir, pattern); }
                catch (UnauthorizedAccessException) { continue; }

                foreach (string file in files)
                    yield return file;
            }
        }
    }

    static bool TryReadPEHeader(string filePath, out string[] peInfo, out bool is64Bit)
    {
        peInfo = Array.Empty<string>();
        is64Bit = false;

        try
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                IMAGE_DOS_HEADER dosHeader = ReadStruct<IMAGE_DOS_HEADER>(reader);
                if (dosHeader.e_magic != 0x5A4D)
                    return false;

                fs.Seek(dosHeader.e_lfanew, SeekOrigin.Begin);
                uint peSignature = reader.ReadUInt32();
                if (peSignature != 0x00004550)
                    return false;

                IMAGE_FILE_HEADER fileHeader = ReadStruct<IMAGE_FILE_HEADER>(reader);

                is64Bit = reader.ReadUInt16() == 0x20B;
                fs.Seek(-2, SeekOrigin.Current);

                IMAGE_DATA_DIRECTORY[] dataDirectories;
                List<string> optionalData;

                if (is64Bit)
                {
                    IMAGE_OPTIONAL_HEADER64 optionalHeader = ReadStruct<IMAGE_OPTIONAL_HEADER64>(reader);
                    optionalData = GetOptionalHeaderData(optionalHeader);
                    dataDirectories = optionalHeader.DataDirectory;
                }
                else
                {
                    IMAGE_OPTIONAL_HEADER32 optionalHeader = ReadStruct<IMAGE_OPTIONAL_HEADER32>(reader);
                    optionalData = GetOptionalHeaderData(optionalHeader);
                    dataDirectories = optionalHeader.DataDirectory;
                }

                List<string> sectionData = GetSectionHeaders(reader, fileHeader.NumberOfSections);

                peInfo = new string[]
                {
                    filePath,
                    $"{dosHeader.e_magic}", $"{dosHeader.e_lfanew}",
                    $"{fileHeader.Machine}", $"{fileHeader.NumberOfSections}", $"{fileHeader.TimeDateStamp}"
                }
                .Concat(optionalData)
                .Concat(dataDirectories.SelectMany(d => new[] { $"{d.VirtualAddress}", $"{d.Size}" }))
                .Concat(sectionData)
                .ToArray();

                return true;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }

    static List<string> GetOptionalHeaderData(dynamic optionalHeader)
    {
        return new List<string>
        {
            $"{optionalHeader.Magic}", $"{optionalHeader.MajorLinkerVersion}",
            $"{optionalHeader.SizeOfCode}", $"{optionalHeader.AddressOfEntryPoint}",
            $"{optionalHeader.ImageBase}", $"{optionalHeader.SectionAlignment}"
        };
    }

    static List<string> GetSectionHeaders(BinaryReader reader, ushort sectionCount)
    {
        List<string> sectionData = new List<string>();

        for (int i = 0; i < Math.Min((int)sectionCount, 10); i++) // 🔹 (int) 캐스팅 추가
        {
            IMAGE_SECTION_HEADER section = ReadStruct<IMAGE_SECTION_HEADER>(reader);
            string sectionName = Encoding.ASCII.GetString(section.Name).TrimEnd('\0');
            sectionData.AddRange(new string[]
            {
                sectionName, $"{section.VirtualSize}", $"{section.VirtualAddress}",
                $"{section.SizeOfRawData}", $"{section.PointerToRawData}",
                $"{section.Characteristics}"
            });
        }
        return sectionData;
    }

    static void SaveCsv(string filename, ConcurrentBag<string[]> data)
    {
        if (data.Count == 0)
            return;

        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var line in data)
                writer.WriteLine(string.Join(",", line));
        }

        Console.WriteLine($"CSV 저장 완료: {filename}");
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