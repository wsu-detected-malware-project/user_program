using user_program.Util;

namespace user_program.Controller {
    public class UtilController {

        #region 싱글톤
        static public UtilController Singleton { get; private set; }
        static UtilController() {
            Singleton = new UtilController();
        }

        #endregion

        #region BackgroundScanner
        public static void GetStart(){
            BackgroundScanner.Start();
        }
        #endregion

        #region EnvManager
        public static DateTime? GetLastScanTime() {
            return EnvManager.LastScanTime();
        }

        public static void GetUpdateLastScanTime() {
            EnvManager.UpdateLastScanTime();
        }

        public static void GetSetMalwareStatus(bool isMalware) {
            EnvManager.SetMalwareStatus(isMalware);
        }
        #endregion

        #region NotificationHelper
        public static void ShowWindowsNotification(string title, string content) {
            NotificationHelper.ShowWindowsNotification(title, content);
        }
        #endregion
    }
}
