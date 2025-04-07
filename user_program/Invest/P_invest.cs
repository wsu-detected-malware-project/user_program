using System;
using System.IO;
using user_program.controller;

namespace user_program;

public class P_invest
{
    public static void P_Read()
    {     
        string[] csvHeader = new string[] {
            "Name", "e_magic", "e_cblp", "e_cp", "e_crlc", "e_cparhdr", "e_minalloc", "e_maxalloc", "e_ss",
            "e_sp", "e_csum", "e_ip", "e_cs", "e_lfarlc", "e_ovno",
            "e_oemid", "e_oeminfo", "e_lfanew",
            "Machine", "NumberOfSections", "TimeDateStamp", "PointerToSymbolTable", "NumberOfSymbols", "SizeOfOptionalHeader", "Characteristics",
            "Magic", "MajorLinkerVersion", "MinorLinkerVersion", "SizeOfCode", "SizeOfInitializedData", "SizeOfUninitializedData",
            "AddressOfEntryPoint", "BaseOfCode", "ImageBase", "SectionAlignment", "FileAlignment",
            "MajorOperatingSystemVersion", "MinorOperatingSystemVersion", "MajorImageVersion", "MinorImageVersion",
            "MajorSubsystemVersion", "MinorSubsystemVersion", "SizeOfHeaders", "CheckSum", "SizeOfImage",
            "Subsystem", "DllCharacteristics", "SizeOfStackReserve", "SizeOfStackCommit", "SizeOfHeapReserve", "SizeOfHeapCommit",
            "LoaderFlags", "NumberOfRvaAndSizes",
            "SuspiciousImportFunctions", "SuspiciousNameSection", "SectionsLength",
            "SectionMinEntropy", "SectionMaxEntropy",
            "SectionMinRawsize", "SectionMaxRawsize",
            "SectionMinVirtualsize", "SectionMaxVirtualsize",
            "SectionMaxPhysical", "SectionMinPhysical",
            "SectionMaxVirtual", "SectionMinVirtual",
            "SectionMaxPointerData", "SectionMinPointerData",
            "SectionMaxChar", "SectionMainChar",
            "DirectoryEntryImport", "DirectoryEntryImportSize",
            "DirectoryEntryExport",
            "ImageDirectoryEntryExport", "ImageDirectoryEntryImport",
            "ImageDirectoryEntryResource", "ImageDirectoryEntryException",
            "ImageDirectoryEntrySecurity"
        };
        //A-Z드라이브 검색 
        for (char i = 'A'; i <= 'Z'; i++)
        {      
            string targetDirectory = $@"{i}:\";
            string outputCsv = i+"_pe_info_p.csv";
            List<string[]> csvData = new List<string[]>();

            Console.WriteLine("PE 파일 검색 중...");

            foreach (string file in GetFilesSafely(targetDirectory, new[] { "*.exe", "*.dll", "*.scr", "*.sys", "*.vxd", "*.ocx", "*.cpl", "*.drv", "*.obj" }))
            {
                try
                {
                    Console.WriteLine($"분석 중: {file}");
                    var peInfo = ReadPE.ReadPEHeader(file);
                    
                    var _controller = new Controller();
                    _controller.C_Print_List(file);            

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

            SaveToCsv(outputCsv, csvData, csvHeader);            
        }
        
    }
    //CSV 파일 저장
    static void SaveToCsv(string outputPath, List<string[]> allRows, string[] headers)
    {
        using (var writer = new StreamWriter(outputPath))
        {
            writer.WriteLine(string.Join(",", headers.Select(EscapeCsvField)));

            foreach (var row in allRows)
            {
                writer.WriteLine(string.Join(",", row.Select(EscapeCsvField)));
            }
        }
        Console.WriteLine($"CSV 저장 완료: {outputPath}");
    }
    static string EscapeCsvField(string field)
    {
        if (string.IsNullOrEmpty(field)) return "";
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }
        return field;
    }
    //폴더 내 모든 PE 파일을 검색하는 기능 추가
    public static IEnumerable<string> GetFilesSafely(string rootDirectory, string[] searchPatterns)
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

}
