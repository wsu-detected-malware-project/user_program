using Microsoft.Toolkit.Uwp.Notifications;
using System.Runtime.InteropServices;

namespace user_program.Util {
    public static class NotificationHelper
    {
        public static void ShowWindowsNotification(string title, string content)
        {
            new ToastContentBuilder()
                .AddText(title)
                .AddText(content)
                .Show(); // 알림 표시
        }
    }
}