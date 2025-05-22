using user_program.Controller;


namespace user_program.Invest {
    public class F_invest {
        public static void F_Read() {
            // CSV 헤더 정의
            string[] csvHeader = Csvheader.csvheader;
            var pathMap = new List<string[]>(); 
            string[] search_drive = Search_System_Drives();

            //string targetDirectory = $@"F:\";

            if (search_drive.Length == 0 || !Directory.Exists(search_drive[0]))// 드라이브 존재 여부 확인
            {
                MessageBox.Show("지정한 드라이브 경로가 존재하지 않습니다.\n검사를 중단합니다.", "드라이브 오류");
                return;
            }

            string outputCsv = "pe_info.csv";
            string pathMapCsv = "path_map.csv"; 
            List<string[]> csvData = new List<string[]>();

            foreach (string drive in search_drive)
            {
                //string targetDirectory = Path.Combine(drive, "");

                string targetDirectory = $@"{drive}";
                foreach (string file in GetFile.GetFilesSafely(targetDirectory, new[] { "*.exe", "*.dll", "*.scr", "*.sys", "*.vxd", "*.ocx", "*.cpl", "*.drv", "*.obj" }))
                {
                    try
                    {
                        var peInfo = ReadPE.ReadPEHeader(file);

                        var F_controller = new FormController();
                        F_controller.Print_Invest_List1(file);

                        if (peInfo != null)
                        {
                            csvData.Add(peInfo);
                            pathMap.Add(new string[] { Path.GetFileName(file), file });
                        }
                    }
                    catch { }
                }
            }
            // 이 시점에서 form2와 progressBar를 다시 가져와서 보정
            var F_controller1 = new FormController();

            F_controller1.Form2_Prograss_Last();


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

        private static int CountFiles(string directory, HashSet<string> extensions)
        {
            try
            {
                var options = new System.IO.EnumerationOptions
                {
                    RecurseSubdirectories = true,
                    IgnoreInaccessible = true,
                    AttributesToSkip = 0,
                    ReturnSpecialDirectories = false,
                };

                return Directory.EnumerateFiles(directory, "*", options)
                    .Count(file => extensions.Contains(Path.GetExtension(file).ToLowerInvariant()));
            }
            catch
            {
                return 0;
            }
        }

        public static int TotalFile() {
            string[] drivesToSearch = Search_System_Drives();
            //string[] drivesToSearch = { "D:" };

            int totalFiles = 0;

            var extensions = new HashSet<string> { ".exe", ".dll", ".scr", ".sys", ".vxd", ".ocx", ".cpl", ".drv", ".obj" };

            foreach (string drive in drivesToSearch)
            {
                //string targetDirectory = Path.Combine(drive, "");
                string targetDirectory = $@"{drive}";
                totalFiles += CountFiles(targetDirectory, extensions);
            }

            return totalFiles;
        }

        static public string[] Search_System_Drives()
        {

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
};

