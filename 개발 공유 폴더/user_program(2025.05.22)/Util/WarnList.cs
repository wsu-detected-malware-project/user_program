using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace user_program.Util
{
    public static class WarnList
    {
        public static void Generate()
        {
            string resultPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.csv");
            string warnlistPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "warnlist.csv");

            if (!File.Exists(resultPath)) return;

            var lines = File.ReadAllLines(resultPath, Encoding.UTF8);
            if (lines.Length < 2) return;

            var existingEntries = new HashSet<string>();
            List<string> finalLines = new List<string>();

            // 기존 warnlist.csv 읽기
            if (File.Exists(warnlistPath))
            {
                var existingLines = File.ReadAllLines(warnlistPath, Encoding.UTF8);
                foreach (var line in existingLines.Skip(1)) // 헤더 제외
                {
                    existingEntries.Add(line.Trim());
                    finalLines.Add(line);
                }
                finalLines.Insert(0, existingLines[0]); // 헤더는 제일 앞에 넣음
            }
            else
            {
                finalLines.Add("Name,Path"); // 새 파일이면 헤더 추가
            }

            // result.csv에서 새 항목 찾아서 누적 추가
            for (int i = 1; i < lines.Length; i++)
            {
                var columns = lines[i].Split(',');
                if (columns.Length < 4) continue;

                if (double.TryParse(columns[1], out double score))
                {
                    if (score >= 0.5 && score < 0.7)
                    {
                        string name = columns[0].Trim('"');
                        string path = columns[3].Trim('"');
                        string newLine = $"\"{name}\",\"{path}\"";

                        if (!existingEntries.Contains(newLine))
                        {
                            existingEntries.Add(newLine);   
                            finalLines.Add(newLine);   
                        }
                    }
                }
            }

            File.WriteAllLines(warnlistPath, finalLines, Encoding.UTF8);
        }

    }
}
