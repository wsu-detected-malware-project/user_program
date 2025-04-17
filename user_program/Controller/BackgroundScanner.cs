using System;
using System.Threading;
using System.Threading.Tasks;
using user_program.scan;
using user_program.util;
using user_program.controller;
using DotNetEnv;

namespace user_program.background
{
    public class BackgroundScanner
    {
        private static readonly TimeSpan Interval = TimeSpan.FromHours(6);

        private static System.Threading.Timer _timer;

        public static void Start()
        {
            Console.WriteLine("백그라운드 스캐너 초기화 중...");

            // 마지막 검사 시간 가져오기
            DateTime? lastScanTime = EnvManager.GetLastScanTime();
            DateTime now = DateTime.Now;

            // 조건 검사: 1) 기록 없음, 2) 마지막 스캔 후 주기 초과
            bool shouldRunImmediately = !lastScanTime.HasValue || (now - lastScanTime.Value) >= Interval;

            if (shouldRunImmediately)
            {
                Console.WriteLine("[백그라운드 스캐너] 조건 충족 → 즉시 스캔 시작");
                Task.Run(async () => await RunScanOnce()); // 즉시 검사
            }

            if (_timer == null)
            {
                Console.WriteLine("백그라운드 스캐너 시작됨");
                _timer = new System.Threading.Timer(_ =>
                {
                    Task.Run(async () => await RunScanOnce());
                }, null, TimeSpan.Zero, Interval);
                GC.KeepAlive(_timer);
            }
        }

        private static async Task RunScanOnce()
        {
            List<string> newPEFiles = B_invest.GetNewPEFilesSinceLastScan();

            if (newPEFiles.Count > 0)
            {
                B_invest.AnalyzeAndSaveToCSV(newPEFiles);

                // 서버 전송
                Controller controllerInstance = Controller.Singleton;
                bool success = await controllerInstance.network();
            }
        }

        public static void Stop()
        {
            _timer?.Dispose();
        }
    }
}
