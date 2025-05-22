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

        public static void GetStop(){
            BackgroundScanner.Stop();
        }

        public static void UpdateInterval(double hours)
        {
            BackgroundScanner.UpdateInterval(hours);
        }
        #endregion

        #region EnvManagers
        public static DateTime? GetLastScanTime() {
            return EnvManager.LastScanTime();
        }

        public static void GetUpdateLastScanTime() {
            EnvManager.UpdateLastScanTime();
        }

        public static void GetSetMalwareStatus(bool isMalware) {
            EnvManager.SetMalwareStatus(isMalware);
        }

        public static void SaveToggleSwitchStatus(bool isOn)
        {
            EnvManager.SaveToggleSwitchStatus(isOn);
        }

        public static void SaveScanInterval(int hours){
            EnvManager.SaveScanInterval(hours);
        }

        public static void UpdateVersionInEnv(string envPath, string newVersion)
        {
            EnvManager.UpdateVersionInEnv(envPath, newVersion);
        }
        #endregion

        #region NotificationHelper
        public static void ShowWindowsNotification(string title, string content) {
            NotificationHelper.ShowWindowsNotification(title, content);
        }

        public static void ShowWarningMessage(string title, string message){
            NotificationHelper.ShowWarningMessage(title, message);
        }
        #endregion

        #region MalwareChecker
        public static bool CheckMalwareInFiles(string peFilesCsvPath){
            return MalwareChecker.CheckMalwareInFiles(peFilesCsvPath);
        }
        #endregion

        #region UpdateManager
        public static void CheckAndUpdate()
        {
            UpdateManager.CheckAndUpdate();
        }

        
        #endregion
    }
}
