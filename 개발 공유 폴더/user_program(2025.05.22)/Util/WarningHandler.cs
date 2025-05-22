using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace user_program.Util
{
    public static class WarningHandler
    {
        public static bool TryHandleWarningMode(string invokedExe)
        {
            if (string.IsNullOrWhiteSpace(invokedExe))
                return false;

            string warnlistPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "warnlist.csv");
            if (!File.Exists(warnlistPath))
            {
                MessageBox.Show("warnlist.csv 파일이 존재하지 않습니다.", "경고 모드 실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string targetPath = null;
            string invokedName = Path.GetFileName(invokedExe).Trim().Trim('"').ToLower();

            foreach (var line in File.ReadAllLines(warnlistPath).Skip(1))
            {
                var parts = line.Split(',');
                if (parts.Length < 2)
                    continue;

                string nameInCsv = parts[0].Trim().Trim('"').ToLower();
                string pathInCsv = parts[1].Trim().Trim('"');

                if (invokedName == nameInCsv || invokedExe.ToLower().EndsWith(nameInCsv))
                {
                    targetPath = pathInCsv;
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(targetPath))
            {
                MessageBox.Show($"{invokedExe} 은(는) warnlist에 등록되어 있지만 경로 정보가 비어 있거나 일치하지 않았습니다.", "경고");
                return true;
            }

            if (!File.Exists(targetPath))
            {
                MessageBox.Show($"실행 대상 경로가 존재하지 않습니다:\n{targetPath}", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            var result = MessageBox.Show(
                $"{invokedExe} 은(는) 의심스러운 프로그램입니다.\n실행하시겠습니까?",
                "실행 경고", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    UnregisterIFEO(Path.GetFileName(invokedExe));
                    Process.Start(targetPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("실행 중 오류 발생: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("실행이 취소되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return true;
        }


        private static void UnregisterIFEO(string exeName)
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options", writable: true))
                {
                    key?.DeleteSubKeyTree(exeName, throwOnMissingSubKey: false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("IFEO 해제 중 오류:\n" + ex.Message, "IFEO 해제 실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

