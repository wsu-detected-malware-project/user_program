using System;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Windows.Forms;
using user_program.FormAll;
using DotNetEnv;
using user_program.Controller;

namespace user_program.Util {
    public static class UpdateManager
    {
        private static readonly string EnvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");
        private static string Version = Env.GetString("VERSION", Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
        private static string Url = Env.GetString("URL", Path.Combine(AppDomain.CurrentDomain.BaseDirectory));

        public class UpdateFile
        {
            public string path { get; set; }
            public string url { get; set; }
        }

        public class UpdateInfo
        {

            public bool update_required { get; set; }
            public string latest_version { get; set; }
            public string release_notes { get; set; }
            public List<UpdateFile> files { get; set; }
        }

        public static void CheckAndUpdate()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string url = $"{Url}/check-update?version={Version}";
                    string response = client.DownloadString(url);
                    var info = JsonSerializer.Deserialize<UpdateInfo>(response);

                    if (info != null && info.update_required)
                    {
                        DialogResult result = MessageBox.Show(
                            $"업데이트가 필요합니다.\n최신 버전: {info.latest_version}\n\n{info.release_notes}\n\n업데이트를 진행하시겠습니까?",
                            "업데이트 확인",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information
                        );

                        if (result == DialogResult.Yes)
                        {
                            ShowUpdateProgress(info.files, info.latest_version);
                        }
                    }
                    else
                    {
                        MessageBox.Show("현재 최신 버전입니다.", "업데이트 확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"업데이트 확인 중 오류 발생:\n{ex.Message}", "업데이트 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void ShowUpdateProgress(List<UpdateFile> files, string latestVersion)
        {
            UpdateProgressForm progressForm = new UpdateProgressForm();
            progressForm.Files = files;
            progressForm.ServerUrl = Url;
            progressForm.LatestVersion = latestVersion;

            progressForm.ShowDialog();
        } 
    }
}