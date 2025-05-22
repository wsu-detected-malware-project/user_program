using System.Runtime.InteropServices;
using System.Text;

namespace user_program.Invest {
    public class ReadPE {
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

        static IEnumerable<string> GetFilesSafely(string rootDirectory, string[] searchPatterns) {
            Stack<string> directories = new Stack<string>();
            directories.Push(rootDirectory);

            while (directories.Count > 0) {
                string currentDir = directories.Pop();

                try {
                    foreach (string subDir in Directory.GetDirectories(currentDir)) {
                        directories.Push(subDir);
                    }
                }
                catch { continue; }

                foreach (string pattern in searchPatterns) {
                    string[] files = Array.Empty<string>();

                    try { files = Directory.GetFiles(currentDir, pattern); }
                    catch { continue; }

                    foreach (string file in files) {
                        yield return file;
                    }
                }
            }
        }

        public static string[] ReadPEHeader(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    IMAGE_DOS_HEADER dosHeader = ReadStruct<IMAGE_DOS_HEADER>(reader);
                    if (dosHeader.e_magic != 0x5A4D)
                    {
                        //MessageBox.Show($"[PE 아님] {filePath}", "파일 건너뜀", MessageBoxButtons.OK, MessageBoxIcon.Information); 메시지 창으로 인해 검사 지연되는 현상 방지
                        return null;
                    }

                    fs.Seek(dosHeader.e_lfanew, SeekOrigin.Begin);
                    uint peSignature = reader.ReadUInt32();
                    if (peSignature != 0x00004550)
                    {
                        //MessageBox.Show($"[PE Signature 없음] {filePath}", "파일 건너뜀", MessageBoxButtons.OK, MessageBoxIcon.Information); 메시지 창으로 인해 검사 지연되는 현상 방지
                        return null;
                    }

                    IMAGE_FILE_HEADER fileHeader = ReadStruct<IMAGE_FILE_HEADER>(reader);

                    bool is64Bit = reader.ReadUInt16() == 0x20B;
                    fs.Seek(-2, SeekOrigin.Current);

                    IMAGE_DATA_DIRECTORY[] dataDirectories;
                    List<string> optionalData64 = new List<string>();
                    List<string> optionalData32 = new List<string>();

                    if (is64Bit)
                    {
                        IMAGE_OPTIONAL_HEADER64 optionalHeader = ReadStruct<IMAGE_OPTIONAL_HEADER64>(reader);
                        optionalData64.AddRange(new string[] {
                        $"{optionalHeader.Magic}", $"{optionalHeader.MajorLinkerVersion}", $"{optionalHeader.MinorLinkerVersion}", $"{optionalHeader.SizeOfCode}", $"{optionalHeader.SizeOfInitializedData}", $"{optionalHeader.SizeOfUninitializedData}",
                        $"{optionalHeader.AddressOfEntryPoint}", $"{optionalHeader.BaseOfCode}", $"{optionalHeader.ImageBase}", $"{optionalHeader.SectionAlignment}", $"{optionalHeader.FileAlignment}", $"{optionalHeader.MajorOperatingSystemVersion}",
                        $"{optionalHeader.MinorOperatingSystemVersion}", $"{optionalHeader.MajorImageVersion}", $"{optionalHeader.MinorImageVersion}", $"{optionalHeader.MajorSubsystemVersion}", $"{optionalHeader.MinorSubsystemVersion}",
                        $"{optionalHeader.SizeOfHeaders}", $"{optionalHeader.CheckSum}", $"{optionalHeader.SizeOfImage}", $"{optionalHeader.Subsystem}", $"{optionalHeader.DllCharacteristics}", $"{optionalHeader.SizeOfStackReserve}",
                        $"{optionalHeader.SizeOfStackCommit}", $"{optionalHeader.SizeOfHeapReserve}", $"{optionalHeader.SizeOfHeapCommit}", $"{optionalHeader.LoaderFlags}", $"{optionalHeader.NumberOfRvaAndSizes}"
                    });
                        dataDirectories = optionalHeader.DataDirectory;
                    }
                    else
                    {
                        IMAGE_OPTIONAL_HEADER32 optionalHeader = ReadStruct<IMAGE_OPTIONAL_HEADER32>(reader);
                        optionalData32.AddRange(new string[] {
                        $"{optionalHeader.Magic}", $"{optionalHeader.MajorLinkerVersion}", $"{optionalHeader.MinorLinkerVersion}", $"{optionalHeader.SizeOfCode}", $"{optionalHeader.SizeOfInitializedData}", $"{optionalHeader.SizeOfUninitializedData}",
                        $"{optionalHeader.AddressOfEntryPoint}", $"{optionalHeader.BaseOfCode}", $"{optionalHeader.ImageBase}", $"{optionalHeader.SectionAlignment}", $"{optionalHeader.FileAlignment}", $"{optionalHeader.MajorOperatingSystemVersion}",
                        $"{optionalHeader.MinorOperatingSystemVersion}", $"{optionalHeader.MajorImageVersion}", $"{optionalHeader.MinorImageVersion}", $"{optionalHeader.MajorSubsystemVersion}", $"{optionalHeader.MinorSubsystemVersion}",
                        $"{optionalHeader.SizeOfHeaders}", $"{optionalHeader.CheckSum}", $"{optionalHeader.SizeOfImage}", $"{optionalHeader.Subsystem}", $"{optionalHeader.DllCharacteristics}", $"{optionalHeader.SizeOfStackReserve}",
                        $"{optionalHeader.SizeOfStackCommit}", $"{optionalHeader.SizeOfHeapReserve}", $"{optionalHeader.SizeOfHeapCommit}", $"{optionalHeader.LoaderFlags}", $"{optionalHeader.NumberOfRvaAndSizes}"
                    });
                        dataDirectories = optionalHeader.DataDirectory;
                    }



                    // Section Headers 및 분석 통합
                    List<string> sectionData = new List<string>();
                    int sectionCount = Math.Min((int)fileHeader.NumberOfSections, 10);

                    List<float> entropies = new List<float>();
                    List<uint> rawSizes = new List<uint>();
                    List<uint> virtualSizes = new List<uint>();
                    List<uint> rawPointers = new List<uint>();
                    List<uint> virtualAddresses = new List<uint>();
                    List<uint> characteristics = new List<uint>();
                    bool suspiciousName = false;

                    for (int i = 0; i < sectionCount; i++)
                    {
                        IMAGE_SECTION_HEADER section = ReadStruct<IMAGE_SECTION_HEADER>(reader);
                        string sectionName = Encoding.ASCII.GetString(section.Name).TrimEnd('\0');

                        sectionData.AddRange(new string[] {
                        sectionName, $"{section.VirtualSize}", $"{section.VirtualAddress}",
                        $"{section.SizeOfRawData}", $"{section.PointerToRawData}",
                        $"{section.PointerToRelocations}", $"{section.PointerToLinenumbers}",
                        $"{section.NumberOfRelocations}", $"{section.NumberOfLinenumbers}",
                        $"{section.Characteristics}"
                    });

                        if (!sectionName.Contains(".text") && (sectionName.Contains("UPX") || sectionName.Trim().Length == 0))
                        {
                            suspiciousName = true;
                        }

                        rawSizes.Add(section.SizeOfRawData);
                        virtualSizes.Add(section.VirtualSize);
                        rawPointers.Add(section.PointerToRawData);
                        virtualAddresses.Add(section.VirtualAddress);
                        characteristics.Add(section.Characteristics);

                        byte[] sectionDataBytes = new byte[section.SizeOfRawData];
                        try
                        {
                            long cur = fs.Position;
                            fs.Seek(section.PointerToRawData, SeekOrigin.Begin);
                            reader.Read(sectionDataBytes, 0, sectionDataBytes.Length);
                            fs.Seek(cur, SeekOrigin.Begin);
                            entropies.Add(CalculateEntropy(sectionDataBytes));
                        }
                        catch
                        {
                            entropies.Add(0f);
                        }
                    }
                    while (sectionData.Count < 11) sectionData.Add("");

                    var mainChar = characteristics
                        .GroupBy(c => c)
                        .OrderByDescending(g => g.Count())
                        .Select(g => g.Key)
                        .FirstOrDefault();

                    bool suspiciousImport = false;
                    if (dataDirectories.Length > 1 && dataDirectories[1].VirtualAddress != 0)
                    {
                        suspiciousImport = true;
                    }

                    List<string> calculated = new List<string>() {
                    suspiciousImport ? "1" : "0",
                    suspiciousName ? "1" : "0",
                    $"{sectionCount}",
                    $"{(entropies.Count > 0 ? entropies.Min() : 0f)}",
                    $"{(entropies.Count > 0 ? entropies.Max() : 0f)}",
                    $"{(rawSizes.Count > 0 ? rawSizes.Min() : 0)}",
                    $"{(rawSizes.Count > 0 ? rawSizes.Max() : 0)}",
                    $"{(virtualSizes.Count > 0 ? virtualSizes.Min() : 0)}",
                    $"{(virtualSizes.Count > 0 ? virtualSizes.Max() : 0)}",
                    $"{(rawPointers.Count > 0 ? rawPointers.Max() : 0)}",
                    $"{(rawPointers.Count > 0 ? rawPointers.Min() : 0)}",
                    $"{(virtualAddresses.Count > 0 ? virtualAddresses.Max() : 0)}",
                    $"{(virtualAddresses.Count > 0 ? virtualAddresses.Min() : 0)}",
                    $"{(rawPointers.Count > 0 ? rawPointers.Max() : 0)}",
                    $"{(rawPointers.Count > 0 ? rawPointers.Min() : 0)}",
                    $"{(characteristics.Count > 0 ? characteristics.Max() : 0)}",
                    $"{mainChar}",
                    $"{(dataDirectories.Length > 1 ? dataDirectories[1].VirtualAddress : 0)}",
                    $"{(dataDirectories.Length > 1 ? dataDirectories[1].Size : 0)}",
                    $"{(dataDirectories.Length > 0 ? dataDirectories[0].VirtualAddress : 0)}",
                    $"{(dataDirectories.Length > 0 ? dataDirectories[0].VirtualAddress : 0)}",
                    $"{(dataDirectories.Length > 1 ? dataDirectories[1].VirtualAddress : 0)}",
                    $"{(dataDirectories.Length > 2 ? dataDirectories[2].VirtualAddress : 0)}",
                    $"{(dataDirectories.Length > 3 ? dataDirectories[3].VirtualAddress : 0)}",
                    $"{(dataDirectories.Length > 4 ? dataDirectories[4].VirtualAddress : 0)}"
                };

                    if (is64Bit)
                    {
                        //csv 총합 저장 (64 비트)
                        return new string[] {
                    Path.GetFileName(filePath),
                    $"{dosHeader.e_magic}", $"{dosHeader.e_cblp}", $"{dosHeader.e_cp}", $"{dosHeader.e_crlc}", $"{dosHeader.e_cparhdr}", $"{dosHeader.e_minalloc}", $"{dosHeader.e_maxalloc}", $"{dosHeader.e_ss}",
                    $"{dosHeader.e_sp}", $"{dosHeader.e_csum}", $"{dosHeader.e_ip}", $"{dosHeader.e_cs}", $"{dosHeader.e_lfarlc}", $"{dosHeader.e_ovno}",
                    $"{dosHeader.e_oemid}", $"{dosHeader.e_oeminfo}", $"{dosHeader.e_lfanew}",
                    $"{fileHeader.Machine}", $"{fileHeader.NumberOfSections}", $"{fileHeader.TimeDateStamp}", $"{fileHeader.PointerToSymbolTable}", $"{fileHeader.NumberOfSymbols}", $"{fileHeader.SizeOfOptionalHeader}", $"{fileHeader.Characteristics}"
                    }
                        .Concat(optionalData64)
                        .Concat(calculated)
                        .ToArray();

                    }
                    else
                    {
                        //csv 총합 저장 (32 비트)
                        return new string[] {
                    Path.GetFileName(filePath),
                    $"{dosHeader.e_magic}", $"{dosHeader.e_cblp}", $"{dosHeader.e_cp}", $"{dosHeader.e_crlc}", $"{dosHeader.e_cparhdr}", $"{dosHeader.e_minalloc}", $"{dosHeader.e_maxalloc}", $"{dosHeader.e_ss}",
                    $"{dosHeader.e_sp}", $"{dosHeader.e_csum}", $"{dosHeader.e_ip}", $"{dosHeader.e_cs}", $"{dosHeader.e_lfarlc}", $"{dosHeader.e_ovno}",
                    $"{dosHeader.e_oemid}", $"{dosHeader.e_oeminfo}", $"{dosHeader.e_lfanew}",
                    $"{fileHeader.Machine}", $"{fileHeader.NumberOfSections}", $"{fileHeader.TimeDateStamp}", $"{fileHeader.PointerToSymbolTable}", $"{fileHeader.NumberOfSymbols}", $"{fileHeader.SizeOfOptionalHeader}", $"{fileHeader.Characteristics}"
                    }
                        .Concat(optionalData32)
                        .Concat(calculated)
                        .ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"[예외 발생]\n{filePath}\n\n{ex.Message}", "ReadPEHeader 오류", MessageBoxButtons.OK, MessageBoxIcon.Error); 메시지 창으로 인해 검사 지연되는 현상 방지
                return null;
            }
        }

                static float CalculateEntropy(byte[] data) {
            if (data == null || data.Length == 0) return 0f;

            int[] counts = new int[256];
            foreach (byte b in data) counts[b]++;

            float entropy = 0f;
            foreach (int count in counts) {
                if (count == 0) continue;
                float p = (float)count / data.Length;
                entropy -= p * (float)Math.Log(p, 2);
            }
            return entropy;
        }

        static T ReadStruct<T>(BinaryReader reader) where T : struct {
            byte[] bytes = reader.ReadBytes(Marshal.SizeOf<T>());
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T structure = Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            handle.Free();
            return structure;
        }
    }
}
