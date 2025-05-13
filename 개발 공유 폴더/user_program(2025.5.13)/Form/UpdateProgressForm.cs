using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using user_program.Controller;
using user_program.Util;

namespace user_program.FormAll
{
    public partial class UpdateProgressForm : Form
    {
        public List<UpdateManager.UpdateFile> Files { get; set; }
        public string ServerUrl { get; set; }

        public UpdateProgressForm()
        {
            InitializeComponent();
            this.Load += UpdateProgressForm_Load;
        }

        #region 바 이벤트
        bool mouseDown;
        Point lastlocation;
        private void lbl_title_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastlocation = e.Location;
        }
        private void lbl_title_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void lbl_title_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastlocation.X) + e.X,
                    (this.Location.Y - lastlocation.Y) + e.Y);

                this.Update();
            }
        }

        private void btn_min_Click(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btn_max_Click(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_close_Click(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e) { }
        #endregion

        private async void UpdateProgressForm_Load(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = Files.Count;
            progressBar1.Value = 0;

            using (WebClient client = new WebClient())
            {
                for (int i = 0; i < Files.Count; i++)
                {
                    var file = Files[i];
                    string localPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.path);
                    string dir = Path.GetDirectoryName(localPath);
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    string fullUrl = $"{ServerUrl}{file.url}";

                    try
                    {
                        label1.Text = $"[{i + 1}/{Files.Count}] {file.path} 다운로드 중...";
                        await client.DownloadFileTaskAsync(fullUrl, localPath);
                        progressBar1.Value = i + 1;
                    }
                    catch (Exception ex)
                    {
                        label1.Text = $"[X] {file.path} 실패: {ex.Message}";
                    }
                }
            }
            MessageBox.Show("업데이트가 완료되었습니다.\n프로그램을 다시 실행합니다.", "업데이트 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string exePath = Application.ExecutablePath;
            System.Diagnostics.Process.Start(exePath);
            Environment.Exit(0);
        }
    }
}
