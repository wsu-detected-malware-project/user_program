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
        private FormController F_controller = FormController.Singleton;

        private static readonly string EnvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");

        private Form1 mainForm;

        public FormSetting()
        {
            InitializeComponent();

            F_controller.formSetting = this;
        }

        public FormSetting(Form1 form1, bool initialState)
        {
            InitializeComponent();
            mainForm = form1; // 생성자에서 Form1 넘겨받기

            F_controller.formSetting = this;

            toggleRealTimeScan.IsOn = initialState;
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
            this.Close();
        }

        //저장 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            string[] whitelistEntries = textBox1.Text
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            string whitelistPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "whitelist.csv");

            File.WriteAllLines(whitelistPath, whitelistEntries);

            if (comboBoxLanguage.SelectedItem == null || F_controller.form1 == null)
                return; // 아직 초기화되지 않은 경우 방지

            // 선택된 언어를 .env에 저장
            F_controller.SaveLanguageToEnv(comboBoxLanguage);

            // 선택된 언어 기준으로 라벨/버튼 텍스트 갱신
            F_controller.UpdateFormLabelsFromCsv(comboBoxLanguage.SelectedItem.ToString());

            // 저장 버튼 클릭 시 콤보박스 값 처리
            if (comboBoxScanInterval.SelectedItem != null)
            {
                string selected = comboBoxScanInterval.SelectedItem.ToString();
                int hours = int.Parse(selected.Replace("Hour", "").Replace("Hour", "").Trim());

                // .env 저장 및 반영
                UtilController.SaveScanInterval(hours);
                UtilController.GetStop();
                UtilController.UpdateInterval(hours);
                UtilController.GetStart();
            }

            // 토글 상태 저장 및 반영
            bool isOn = toggleRealTimeScan.IsOn;
            UtilController.SaveToggleSwitchStatus(isOn);
            if (mainForm != null)
            {
                FormController.UpdateRealTimeScanStatus(isOn);
                if (isOn)
                    UtilController.GetStart();
                else
                    UtilController.GetStop();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 언어 번역
        [NonSerialized]
        private string _real_time_switch;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Real_Time_Switch_Set
        {
            get { return _real_time_switch; }
            set
            {
                _real_time_switch = value.ToString();
                labelRealTimeScan.Text = _real_time_switch;
            }
        }

        [NonSerialized]
        private string _real_time_cycle;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Real_Time_Cycle_Set
        {
            get { return _real_time_cycle; }
            set
            {
                _real_time_cycle = value.ToString();
                label1.Text = _real_time_cycle;
            }
        }

        [NonSerialized]
        private string _whitelist_data;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Whitelist_Data_set
        {
            get { return _whitelist_data; }
            set
            {
                _whitelist_data = value.ToString();
                labelExclusion.Text = _whitelist_data;
            }
        }

        [NonSerialized]
        private string _update_label;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Update_Label_Set
        {
            get { return _update_label; }
            set
            {
                _update_label = value.ToString();
                labelUpdate.Text = _update_label;
            }
        }

        [NonSerialized]
        private string _update_check;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Update_Check_Set
        {
            get { return _update_check; }
            set
            {
                _update_check = value.ToString();
                buttonUpdate.Text = _update_check;
            }
        }

        [NonSerialized]
        private string _language;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Language_Set
        {
            get { return _language; }
            set
            {
                _language = value.ToString();
                labelLanguage.Text = _language;
            }
        }
        //저장 버튼
        [NonSerialized]
        private string _save_button;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Save_Button
        {
            get { return _save_button; }
            set
            {
                _save_button = value.ToString();
                button1.Text = _save_button;
            }
        }
        //닫기 버튼
        [NonSerialized]
        private string _close_button;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Close_Button
        {
            get { return _close_button; }
            set
            {
                _close_button = value.ToString();
                button2.Text = _close_button;
            }
        }

        #endregion

        private void FormSetting_Load(object sender, EventArgs e)
        {
            string languagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "language.csv");
            if (!File.Exists(languagePath))
            {
                MessageBox.Show("language.csv 파일이 존재하지 않습니다.\n언어 설정 기능을 비활성화합니다.", "언어 설정 오류");
                comboBoxLanguage.Enabled = false;
            }

            // 콤보박스 초기화
            comboBoxScanInterval.Items.Clear();
            for (int i = 1; i <= 24; i++)
            {
                comboBoxScanInterval.Items.Add($"{i}Hour");
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

            string selectedItemText = $"{scanIntervalHours}Hour";

            if (comboBoxScanInterval.Items.Contains(selectedItemText))
            {
                comboBoxScanInterval.SelectedItem = selectedItemText; //  무조건 선택해!
            }
            else
            {
                comboBoxScanInterval.SelectedItem = "6Hour"; //  실패하면 6시간 기본
            }

            string filterPath = Environment.GetEnvironmentVariable("FILTERPATH");
            if (!string.IsNullOrEmpty(filterPath))
            {
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filterPath);
                if (File.Exists(fullPath))
                {
                    string[] lines = File.ReadAllLines(fullPath);
                    textBox1.Text = string.Join(Environment.NewLine, lines);
                }
            }

            F_controller.Combobox_print_language(comboBoxLanguage);
            F_controller.UpdateFormLabelsFromCsv(Env.GetString("COMBOBOX_LANGUAGE", "한국어"));
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            UtilController.CheckAndUpdate();
        }
    }
}
