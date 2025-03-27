using System;
using System.IO;
using user_program.controller;

namespace user_program;

public class F_invest
{
    public static void F_Read()
    {
        string targetDirectory = $@"C:\";

        string outputCsv = "f_pe_info.csv";
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

        File.WriteAllLines(outputCsv, csvData.ConvertAll(line => string.Join(",", line)));
        Console.WriteLine($"CSV 저장 완료: {outputCsv}");

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
