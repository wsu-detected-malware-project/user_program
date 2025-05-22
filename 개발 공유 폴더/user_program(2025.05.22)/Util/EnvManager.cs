
namespace user_program.Util {
    public static class EnvManager {
        private static readonly string EnvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");
        private const string ScanTimeKey = "LAST_SCAN_TIME";
        private const string realtimescan = "REALTIME_SCAN";

        // 마지막 검사 시간 불러오기
        public static DateTime? LastScanTime() {
            if (!File.Exists(EnvPath))
                return null;

            string[] lines = File.ReadAllLines(EnvPath);
            foreach (string line in lines) {
                if (line.StartsWith(ScanTimeKey + "=")) {
                    string value = line.Substring(ScanTimeKey.Length + 1);
                    if (DateTime.TryParse(value, out DateTime time)) {
                        return time;
                    }
                }
            }
            return null;
        }

        // 현재 시간 저장
        public static void UpdateLastScanTime() {
            var lines = new List<string>();
            bool found = false;

            if (File.Exists(EnvPath)) {
                foreach (string line in File.ReadAllLines(EnvPath)) {
                    if (line.StartsWith(ScanTimeKey + "=")) {
                        lines.Add($"{ScanTimeKey}={DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                        found = true;
                    }
                    else {
                        lines.Add(line);
                    }
                }
            }
            if (!found) {
                lines.Add($"{ScanTimeKey}={DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            }
            File.WriteAllLines(EnvPath, lines);
        }

        public static void SetMalwareStatus(bool isMalware) {
            var lines = File.Exists(EnvPath) ? File.ReadAllLines(EnvPath).ToList() : new List<string>();
            bool found = false;

            for (int i = 0; i < lines.Count; i++) {
                if (lines[i].StartsWith("IS_MALWARE=")) {
                    lines[i] = $"IS_MALWARE={isMalware.ToString().ToLower()}";
                    found = true;
                    break;
                }
            }

            if (!found) {
                lines.Add($"IS_MALWARE={isMalware.ToString().ToLower()}");
            }

            File.WriteAllLines(EnvPath, lines);
        }

        // 토글 상태 저장
        public static void SaveToggleSwitchStatus(bool isOn)
        {
            var lines = new List<string>();
            bool found = false;

            // 기존 .env 파일 읽어오기
            if (File.Exists(EnvPath))
            {
                foreach (string line in File.ReadAllLines(EnvPath))
                {
                    if (line.StartsWith(realtimescan + "="))
                    {
                        lines.Add($"{realtimescan}={(isOn ? "ON" : "OFF")}");
                        found = true;
                    }
                    else
                    {
                        lines.Add(line);
                    }
                }
            }
            if (!found)
            {
                lines.Add($"{realtimescan}={(isOn ? "ON" : "OFF")}");
            }
            File.WriteAllLines(EnvPath, lines);
        }

        // 검사 시간 저장
        public static void SaveScanInterval(int hours)
        {
            var lines = new List<string>();

            if (File.Exists(EnvPath))
            {
                lines.AddRange(File.ReadAllLines(EnvPath));
            }

            bool found = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("SCAN_INTERVAL="))
                {
                    lines[i] = $"SCAN_INTERVAL={hours}";
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                lines.Add($"SCAN_INTERVAL={hours}");
            }
            File.WriteAllLines(EnvPath, lines);
        }

        //버전정보 업데이트
        public static void UpdateVersionInEnv(string envPath, string newVersion)
        {

            if (string.IsNullOrWhiteSpace(newVersion))
                return;

            if (!File.Exists(envPath)) return;

            var lines = File.ReadAllLines(envPath).ToList();
            bool versionLineFound = false;

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("VERSION="))
                {
                    lines[i] = $"VERSION={newVersion}";
                    versionLineFound = true;
                    break;
                }
            }

            if (!versionLineFound)
            {
                // VERSION 줄이 없으면 새로 추가
                lines.Add($"VERSION={newVersion}");
            }

            File.WriteAllLines(envPath, lines);
        }
    }
}