using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DotNetEnv;
using System.Threading.Tasks;
using user_program.FormAll;
using user_program.Controller;
using static System.Net.Mime.MediaTypeNames;
using user_program.Util;
using System.Runtime.InteropServices;

namespace user_program.FormAll
{
    public partial class FormSetting : Form
    {
        private static readonly string EnvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");

        private Form1 mainForm;

        public FormSetting()
        {
            InitializeComponent();
        }

        public FormSetting(Form1 form1, bool initialState)
        {
            InitializeComponent();
            mainForm = form1; // 생성자에서 Form1 넘겨받기

            toggleRealTimeScan.IsOn = initialState;
            // 토글 스위치 이벤트 연결
            toggleRealTimeScan.ToggleChanged += ToggleRealTimeScan_ToggleChanged;
        }

        #region 디자인 이벤트
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
            UtilController.SaveToggleSwitchStatus(toggleRealTimeScan.IsOn);
            if (mainForm != null)
            {
                FormController.UpdateRealTimeScanStatus(toggleRealTimeScan.IsOn); // 저장하고 반영
            }
            this.Close();
        }

        private void ToggleRealTimeScan_ToggleChanged(object sender, EventArgs e)
        {
            UtilController.SaveToggleSwitchStatus(toggleRealTimeScan.IsOn);
            if (mainForm != null)
            {
                FormController.UpdateRealTimeScanStatus(toggleRealTimeScan.IsOn);

                if (toggleRealTimeScan.IsOn)
                {
                    UtilController.GetStart(); // 토글 켜지면 다시 BackgroundScanner 시작
                }
                else
                {
                    UtilController.GetStop();  // 토글 끄면 검사 멈춤
                }
            }
        }

        private void comboBoxScanInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxScanInterval.SelectedItem != null)
            {
                string selected = comboBoxScanInterval.SelectedItem.ToString();

                // "1시간", "2시간" 같은 문자열을 숫자만 뽑아내기
                int hours = int.Parse(selected.Replace("시간", "").Trim());

                // .env 파일에 저장
                UtilController.SaveScanInterval(hours);

                // BackgroundScanner에 즉시 반영
                BackgroundScanner.Stop();
                BackgroundScanner.UpdateInterval(hours);
                BackgroundScanner.Start();
            }
        }
        #endregion

        private void FormSetting_Load(object sender, EventArgs e)
        {
            // 콤보박스 초기화
            comboBoxScanInterval.Items.Clear();
            for (int i = 1; i <= 24; i++)
            {
                comboBoxScanInterval.Items.Add($"{i}시간");
            }

            Env.Load(EnvPath);
            string scanIntervalStr = Environment.GetEnvironmentVariable("SCAN_INTERVAL");
            int scanIntervalHours = 6; // 기본 6시간

            if (!string.IsNullOrEmpty(scanIntervalStr) && int.TryParse(scanIntervalStr, out int hours))
            {
                if (hours >= 1 && hours <= 24)
                {
                    scanIntervalHours = hours;
                }
            }

            string selectedItemText = $"{scanIntervalHours}시간";

            if (comboBoxScanInterval.Items.Contains(selectedItemText))
            {
                comboBoxScanInterval.SelectedItem = selectedItemText; //  무조건 선택해!
            }
            else
            {
                comboBoxScanInterval.SelectedItem = "6시간"; //  실패하면 6시간 기본
            }
        }
    }
}
