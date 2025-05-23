using user_program.Controller;


namespace user_program.Invest {
    public class F_invest {
        public static void F_Read() {
            // CSV 헤더 정의
            string[] csvHeader = Csvheader.csvheader;

            string targetDirectory = $@"G:\";

            string outputCsv = "pe_info.csv";
            List<string[]> csvData = new List<string[]>();
            
            foreach (string file in GetFile.GetFilesSafely(targetDirectory, new[] { "*.exe", "*.dll", "*.scr", "*.sys", "*.vxd", "*.ocx", "*.cpl", "*.drv", "*.obj" })) {
                try {
                    var peInfo = ReadPE.ReadPEHeader(file);

                    var F_controller = new FormController();
                    F_controller.Print_Invest_List1(file);
                    
                    if (peInfo != null) {
                        csvData.Add(peInfo);
                    }
                    
                }
                catch {}
            }

            SaveToCsv(outputCsv, csvData, csvHeader);
            UtilController.GetUpdateLastScanTime();
        }

        //CSV 파일 저장
        static void SaveToCsv(string outputPath, List<string[]> allRows, string[] headers) {
            using (var writer = new StreamWriter(outputPath)) {
                writer.WriteLine(string.Join(",", headers.Select(EscapeCsvField)));

                foreach (var row in allRows) {
                    writer.WriteLine(string.Join(",", row.Select(EscapeCsvField)));
                }
            }
        }
        static string EscapeCsvField(string field) {
            if (string.IsNullOrEmpty(field)) return "";
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n")) {
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }
            return field;
        }
        
        public static int TotalFile() {
            string targetDirectory = $@"G:\";

            int totalFiles = GetFile.GetFilesSafely(targetDirectory, new[] { "*.exe", "*.dll", "*.scr", "*.sys", "*.vxd", "*.ocx", "*.cpl", "*.drv", "*.obj" }).Count();

            return totalFiles;
        }
    
    }
};

