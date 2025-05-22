using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace user_program.Util
{
    //검사한 파일 경로 추출해서 result.csv와 결합
    public static class FilePath
    {
        public static void AppendPathsToResult(string resultPath)
        {
            string mapPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "path_map.csv");
            if (!File.Exists(mapPath) || !File.Exists(resultPath)) return;

            var pathDict = new Dictionary<string, string>();

            foreach (var line in File.ReadAllLines(mapPath, Encoding.UTF8).Skip(1))
            {
                var cols = line.Split(',');
                if (cols.Length < 2) continue;

                string name = cols[0].Trim();
                string path = cols[1].Trim();

                if (!pathDict.ContainsKey(name))
                    pathDict[name] = path;
            }

            var resultLines = File.ReadAllLines(resultPath, Encoding.UTF8);
            var newLines = new List<string>();

            newLines.Add(resultLines[0] + ",Path");

            for (int i = 1; i < resultLines.Length; i++)
            {
                var cols = resultLines[i].Split(',');
                string name = cols[0].Trim('"');
                string path = pathDict.ContainsKey(name) ? pathDict[name] : "경로 없음";
                newLines.Add(resultLines[i] + $",\"{path}\"");
            }

            File.WriteAllLines(resultPath, newLines, Encoding.UTF8);
        }
    }
}