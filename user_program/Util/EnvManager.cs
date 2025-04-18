
namespace user_program.Util {
    public static class EnvManager {
        private static readonly string EnvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");
        private const string ScanTimeKey = "LAST_SCAN_TIME";
        private const string MalwareKey = "IS_MALWARE";

        // .env 파일에서 마지막 검사 시간 불러오기
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

        // .env 파일에 현재 시간 저장하기
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
    }
}