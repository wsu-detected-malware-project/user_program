using user_program.Controller;
using System.Management;

namespace user_program.Invest {
    public class D_invest {
        public static void D_Read() {
            // CSV 헤더 정의
            string[] csvHeader = Csvheader.csvheader;
            
            string outputCsv = "pe_info.csv";
            List<string[]> csvData = new List<string[]>();

            string[] search_drive = SearchAllDrives();

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
            string[] drivesToSearch = SearchAllDrives();

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
        static public string[] SearchAllDrives() {

        var resultList = new System.Collections.Generic.List<string>();

        try 
        {
            // 1. USB 디스크 찾기
            ManagementObjectSearcher usbSearcher = new ManagementObjectSearcher(
                "SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'");

            foreach (ManagementObject drive in usbSearcher.Get())
            {

                var driveId = drive["DeviceID"].ToString();  // 예: \\.\PHYSICALDRIVE2
                var partitionQuery = $"ASSOCIATORS OF {{Win32_DiskDrive.DeviceID='{driveId}'}} WHERE AssocClass=Win32_DiskDriveToDiskPartition";
                ManagementObjectSearcher partitionSearcher = new ManagementObjectSearcher(partitionQuery);

                foreach (ManagementObject partition in partitionSearcher.Get())
                {
                    var logicalQuery = $"ASSOCIATORS OF {{Win32_DiskPartition.DeviceID='{partition["DeviceID"]}'}} WHERE AssocClass=Win32_LogicalDiskToPartition";
                    ManagementObjectSearcher logicalSearcher = new ManagementObjectSearcher(logicalQuery);

                    foreach (ManagementObject logical in logicalSearcher.Get())
                    {
                        resultList.Add("" +logical["DeviceID"]);  // E:
                    }
                }
            }

            // 2. CD/DVD 드라이브 찾기
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
            {
                if (drive.DriveType == DriveType.CDRom)
                {
                    resultList.Add(drive.Name);
                }
            }
            }
            catch (Exception ex)
            {
                resultList.Add("검색 중 오류 발생: " + ex.Message);
            }

            return resultList.ToArray();
        }


    }
}

