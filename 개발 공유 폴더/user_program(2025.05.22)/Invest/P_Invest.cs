using user_program.Controller;

namespace user_program.Invest {
    public class P_invest {
        public static void P_Read() {     
            
            string[] csvHeader = Csvheader.csvheader;


            
            string outputCsv = "pe_info.csv";
            string pathMapCsv = "path_map.csv";
            List<string[]> csvData = new List<string[]>();
            var pathMap = new List<string[]>();

            //A-Z드라이브 검색 
            for (char i = 'F'; i <= 'G'; i++) {      
                string targetDirectory = $@"{i}:\";
                

                foreach (string file in GetFile.GetFilesSafely(targetDirectory, new[] { "*.exe", "*.dll", "*.scr", "*.sys", "*.vxd", "*.ocx", "*.cpl", "*.drv", "*.obj" })) {
                    try {
                        var peInfo = ReadPE.ReadPEHeader(file);        

                        var F_controller = new FormController();
                        F_controller.Print_Invest_List1(file); 

                        if (peInfo != null) {
                            csvData.Add(peInfo);
                            pathMap.Add(new string[] { Path.GetFileName(file), file });

                        } 
                    }
                    catch {
                        
                    }
                }

                
            }

            SaveToCsv(outputCsv, csvData, csvHeader);
            SaveToCsv(pathMapCsv, pathMap, new string[] { "Name", "Path" });
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
    }
}
