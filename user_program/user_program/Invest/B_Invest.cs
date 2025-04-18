using user_program.Controller;


namespace user_program.Invest {
    public static class B_invest {
        private static readonly string[] peExtensions = new[] { "*.exe", "*.dll", "*.scr", "*.sys", "*.vxd", "*.ocx", "*.cpl", "*.drv", "*.obj" };

        public static List<string> GetNewPEFilesSinceLastScan() {
            DateTime? lastScan = UtilController.GetLastScanTime();
            List<string> peFiles = new List<string>();

            foreach (var drive in DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Fixed || d.DriveType == DriveType.Removable)) {
                try {
                    foreach (var ext in peExtensions) {
                        peFiles.AddRange(GetFilesSafely(drive.RootDirectory.FullName, ext, lastScan));
                    }
                }
                catch {}
            }
            return peFiles;
        }

        private static List<string> GetFilesSafely(string path, string searchPattern, DateTime? lastScan) {
            List<string> result = new List<string>();

            try {
                foreach (var file in Directory.GetFiles(path, searchPattern)) {
                    if (lastScan == null || File.GetCreationTime(file) > lastScan) {
                        if (IsPEFile(file))
                            result.Add(file);
                    }
                }

                foreach (var dir in Directory.GetDirectories(path)) {
                    result.AddRange(GetFilesSafely(dir, searchPattern, lastScan));
                }
            }
            catch {}

            return result;
        }

        private static bool IsPEFile(string path) {
            try {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (var br = new BinaryReader(fs)) {
                    ushort magic = br.ReadUInt16();
                    return magic == 0x5A4D; // 'MZ'
                }
            }
            catch {
                return false;
            }
        }

        public static void AnalyzeAndSaveToCSV(List<string> peFiles) {
            List<string[]> allRows = new List<string[]>();

            foreach (var file in peFiles) {
                try {
                    string[] featureRow = ReadPE.ReadPEHeader(file);
                    if (featureRow != null) {
                        allRows.Add(featureRow);
                    }
                }
                catch{}
            }

            if (allRows.Count > 0) {
                string outputCsv = "pe_info.csv"; // 새롭게 저장할 파일 이름
                SaveToCsv(outputCsv, allRows, Csvheader.csvheader);
            }

            UtilController.GetUpdateLastScanTime(); // 검사 시간 갱신
        }

        // CSV 저장 메서드 (F_invest.cs 기반)
        private static void SaveToCsv(string outputPath, List<string[]> allRows, string[] headers) {
            using (var writer = new StreamWriter(outputPath)) {
                writer.WriteLine(string.Join(",", headers.Select(EscapeCsvField)));
                foreach (var row in allRows) {
                    writer.WriteLine(string.Join(",", row.Select(EscapeCsvField)));
                }
            }
        }

        private static string EscapeCsvField(string field) {
            if (field.Contains(",") || field.Contains("\"")) {
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }
            return field;
        }
    }
}