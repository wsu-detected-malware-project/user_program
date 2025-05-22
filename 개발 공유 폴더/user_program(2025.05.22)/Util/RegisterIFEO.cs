using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using System.Text;

namespace user_program.Util
{
    public static class RegisterIFEO //warnlist(애매한 값)을 토대로 레지스트리 값 변환
    {
        public static void RegisterWarnListToIFEO(string warningExePath)
        {
            string warnlistPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "warnlist.csv");
            if (!File.Exists(warnlistPath)) return;

            var lines = File.ReadAllLines(warnlistPath, Encoding.UTF8).Skip(1);

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length < 1) continue;

                string exeName = parts[0].Trim().Trim('"');
                try
                {
                    using (RegistryKey key = Registry.LocalMachine.CreateSubKey(
                        $@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\{exeName}",
                        true))
                    {
                        key.SetValue("Debugger", $"\"{warningExePath}\"");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{exeName} 등록 실패: {ex.Message}");
                }
            }

            Console.WriteLine("IFEO 등록 완료");
        }
    }
}
