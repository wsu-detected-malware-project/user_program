using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace user_program.Util
{
    public static class WhitelistFilter 
    {
        public static List<string[]> ApplyWhitelistFilter(string peCsvPath, string whitelistPath, out string[] headers)
        {
            headers = null;
            if (!File.Exists(peCsvPath)) return null;

            var lines = File.ReadAllLines(peCsvPath);
            if (lines.Length <= 1) return null;

            headers = lines[0].Split(',');

            int nameIndex = Array.FindIndex(headers, h => h.Trim().Equals("name", StringComparison.OrdinalIgnoreCase));
            if (nameIndex == -1) return null;

            var whitelist = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (File.Exists(whitelistPath))
            {
                foreach (var line in File.ReadAllLines(whitelistPath))
                {
                    string name = line.Trim();
                    if (!string.IsNullOrEmpty(name)) whitelist.Add(name);
                }
            }

            return lines.Skip(1)
                        .Select(line => line.Split(','))
                        .Where(row => !whitelist.Contains(row[nameIndex].Trim()))
                        .ToList();
        }
    }
}
