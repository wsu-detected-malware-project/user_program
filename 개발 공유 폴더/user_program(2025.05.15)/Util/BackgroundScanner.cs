using user_program.Controller;
using user_program.Invest;

namespace user_program.Util {
    public class BackgroundScanner {
        private static TimeSpan Interval = TimeSpan.FromHours(6);

        private static System.Threading.Timer _timer;
        private static bool isRunning = false;
        private static bool isScanning = false;

        private static System.Threading.Timer _driveCheckTimer;
        private static HashSet<string> _previousDrives = new HashSet<string>();
        private static bool _isProcessingDrive = false;

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
                isRunning = true;
            }

            _driveCheckTimer = new System.Threading.Timer(_ => {
                Task.Run(() => {

                    if (_isProcessingDrive) return;  // 이미 처리 중이면 무시

                    string[] currentDrives = D_invest.SearchAllDrives();

                    var currentDriveSet = new HashSet<string>(currentDrives);

                    // 새 드라이브가 생긴 경우에만 실행
                    if (currentDriveSet.Except(_previousDrives).Any()) {
                        _isProcessingDrive = true;

                        Application.OpenForms[0].Invoke(new Action(() => {
                            FormController.Form2_drive_search();
                            _isProcessingDrive = false;
                        }));

                        _previousDrives = currentDriveSet;
                    }
                });

            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

        }

        private static async Task RunScanOnce()
        {
            if (isScanning)
            {
                Console.WriteLine("[BackgroundScanner] 이미 검사 중이므로 실행 안 함");
                return;
            }

            try
            {
                isScanning = true;
                if (!isRunning) return;

                List<string> newPEFiles = PEController.Get_B_Invest_LastScan();
                if (!isRunning || newPEFiles.Count == 0) return;

                UtilController.ShowWindowsNotification("검사 시작", "PE 파일 분석을 시작합니다.");
                string filePath = PEController.Get_Analyze(newPEFiles);
                if (!isRunning) return;

                bool hasMalware = UtilController.CheckMalwareInFiles(filePath);
                if (!isRunning) return;

                if (hasMalware)
                {
                    FormController.Singleton.form1?.Invoke(new Action(() =>
                    {
                        if (FormController.Singleton.form1.WindowState == FormWindowState.Minimized)
                            FormController.Singleton.form1.WindowState = FormWindowState.Normal;

                        FormController.Singleton.form1.Show();
                        FormController.Singleton.form1.BringToFront();
                        FormController.Singleton.form1.Activate();

                        UtilController.ShowWarningMessage("악성코드 발견", "검사를 진행하세요!");
                    }));
                }
                else
                {
                    UtilController.ShowWindowsNotification("악성코드 없음", "모든 파일이 안전합니다.");
                }

                if (isRunning)
                {
                    UtilController.ShowWindowsNotification("검사 종료", "검사가 정상적으로 완료되었습니다.");
                }
            }
            finally
            {
                isScanning = false;
            }
        }

        public static void Stop() {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
            isRunning = false;
        }

        public static void UpdateInterval(double hours)
        {
            Interval = TimeSpan.FromHours(hours);
        }
    }
}