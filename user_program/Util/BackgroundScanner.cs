using user_program.Controller;

namespace user_program.Util {
    public class BackgroundScanner {
        private static readonly TimeSpan Interval = TimeSpan.FromHours(6);

        private static System.Threading.Timer _timer;

        public static void Start() {

            // 마지막 검사 시간 가져오기
            DateTime? lastScanTime = EnvManager.LastScanTime();
            DateTime now = DateTime.Now;

            // 조건 검사: 1) 기록 없음, 2) 마지막 스캔 후 주기 초과
            bool shouldRunImmediately = !lastScanTime.HasValue || (now - lastScanTime.Value) >= Interval;

            if (shouldRunImmediately) {
                Task.Run(async () => await RunScanOnce()); // 즉시 검사
            }

            if (_timer == null) {
                _timer = new System.Threading.Timer(_ => {
                    Task.Run(async () => await RunScanOnce());
                }, null, TimeSpan.Zero, Interval);
                GC.KeepAlive(_timer);
            }
        }

        private static async Task RunScanOnce() {
            List<string> newPEFiles = PEController.Get_B_Invest_LastScan();

            if (newPEFiles.Count > 0) {
                UtilController.ShowWindowsNotification("검사 시작", "PE 파일 분석을 시작합니다.");
                PEController.Get_Analyze(newPEFiles);

                // 서버 전송
                NetController controllerInstance = NetController.Singleton;
                bool success = await controllerInstance.network();

                // 검사 결과 확인 후 알림
                DotNetEnv.Env.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env"));
                string isMalware = DotNetEnv.Env.GetString("IS_MALWARE", "false");
                bool hasMalware = isMalware.ToLower() == "true";

                UtilController.ShowWindowsNotification(
                    hasMalware ? "악성코드 발견" : "악성코드 없음",
                    hasMalware ? "악성코드가 탐지되었습니다." : "모든 파일이 안전합니다.");

            }
            UtilController.ShowWindowsNotification("검사 종료", "검사가 정상적으로 완료되었습니다.");
        }

        public static void Stop() {
            _timer?.Dispose();
        }
    }
}
