using func.Controller;
using System.Management;

namespace func.Invest {
    public class F_invest {
        public static void F_Read() {
            // CSV 헤더 정의
            string[] csvHeader = Csvheader.csvheader;
            
            string outputCsv = "pe_info.csv";
            List<string[]> csvData = new List<string[]>();

            string[] search_drive = Search_System_Drives();
            //string[] search_drive = { "D:/" };

            foreach (string drive in search_drive)
            { 
                string targetDirectory = $@"{drive}\";

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
            }

            SaveToCsv(outputCsv, csvData, csvHeader);
            //UtilController.GetUpdateLastScanTime();
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
            string[] drivesToSearch = Search_System_Drives();

            int totalFiles = 0;
            foreach (string drive in drivesToSearch)
            {
                string targetDirectory = $@"{drive}\";
                int fileCount = GetFile.GetFilesSafely(targetDirectory, new[] { "*.exe", "*.dll", "*.scr", "*.sys", "*.vxd", "*.ocx", "*.cpl", "*.drv", "*.obj" }).Count();
                totalFiles += fileCount;
            }
            return totalFiles;
        }
    
        //디스크 탐지 후 배열로 저장
        static public string[] Search_System_Drives() {

            var resultList = new System.Collections.Generic.List<string>();

            try
            {
                // 1. 운영체제(Windows) 설치 경로 가져오기 (예: C:\Windows)
                string windowsPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

                // 2. 드라이브 문자만 추출 (예: C:\ → C:/)
                string systemDrive = Path.GetPathRoot(windowsPath).Replace("\\", "/");

                // 3. 리스트에 추가
                resultList.Add(systemDrive);
            }
            catch (Exception ex)
            {
                resultList.Add("오류 발생: " + ex.Message);
            }

            return resultList.ToArray();
        }


    }
}

