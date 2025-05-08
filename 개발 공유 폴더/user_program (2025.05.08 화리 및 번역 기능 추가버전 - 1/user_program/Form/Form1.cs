using System.ComponentModel;
using Microsoft.Win32;  // 자동 실행 위해 필요
using DotNetEnv;
using user_program.Controller;
using System.Drawing.Drawing2D;
using System.Drawing.Text; //추가

namespace user_program.FormAll {
    public partial class Form1 : Form
    {
        private static readonly string EnvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");

        private FormController F_controller = FormController.Singleton;
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private System.Windows.Forms.Timer autoUpdateTimer;

        private FormSetting formsetting = null;
        private static bool isRealTimeScanOn = true; // 현재 실시간 검사 상태

        private string lastRelativeTime = "";

        public Form1()
        {
            // 실행 위치 고정
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // .env 로드
            DotNetEnv.Env.Load();

            InitializeComponent();
            InitializeTrayIcon();
            LoadRealTimeScanStatus();
            StartAutoUpdateTimer();
            F_controller.form1 = this;
            this.Load += new EventHandler(Form1_Load);
            FormController.SetMainForm(this);

            // 트레이 아이콘 초기화
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("열기", null, OnOpen);
            trayMenu.Items.Add("종료", null, OnExit);

            trayIcon = new NotifyIcon();
            trayIcon.Text = "PE 분석기";
            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.Visible = true;
            trayIcon.DoubleClick += OnOpen;

            this.Resize += new EventHandler(Form1_Resize);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);

            // 윈도우 부팅 시 자동 실행 등록
            SetAutoStart(true);

            this.Show();
            this.WindowState = FormWindowState.Minimized;

            this.Activated += new EventHandler(Form1_Activated);
        
        }


        //버튼 테두리 둥글게
        private void round_button(Button btn)
        {
            int radius = 30;

            btn.Paint += (s, e) =>
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(0, 0, radius, radius, 180, 90);
                    path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
                    path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
                    path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
                    path.CloseFigure();
                    btn.Region = new Region(path);

                    using (Pen pen = new Pen(Color.Snow, 2))
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };
        }

        private void StartAutoUpdateTimer()
        {
            autoUpdateTimer = new System.Windows.Forms.Timer();
            autoUpdateTimer.Interval = 5000; // 30초마다 갱신
            autoUpdateTimer.Tick += (s, e) => UpdateScanInfoUI();
            autoUpdateTimer.Start();
        }

        private void InitializeTrayIcon()
        {
            trayIcon = new NotifyIcon();

            // .ico 경로 가져오기 (.env에서 ICON_PATH 지정, 없으면 기본값 사용)
            string iconPath = Env.GetString("ICON_PATH", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pe_checker.ico"));

            // 아이콘 로딩 시도 → 실패하면 기본 아이콘으로 대체
            try
            {
                trayIcon.Icon = new Icon(iconPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("아이콘 로딩 실패!\n경로: " + iconPath + "\n오류: " + ex.Message);
                trayIcon.Icon = SystemIcons.Application;
            }

            trayIcon.Visible = true;

            // 트레이 메뉴 구성
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("열기", null, OnOpen);
            trayMenu.Items.Add("종료", null, OnExit);

            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.DoubleClick += OnOpen;
        }

        private void OnOpen(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void OnExit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Environment.Exit(0); // 완전 강제 종료
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // [X] 버튼 눌러도 숨김 처리
            this.Hide();
        }

        private void SetAutoStart(bool enable)
        {
            string runKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(runKey, true);

            if (enable)
            {
                key.SetValue("WSU_Malware", Application.ExecutablePath);
            }
            else
            {
                key.DeleteValue("WSU_Malware", false);
            }
        }

        public class NoDragRichTextBox : RichTextBox // 실시간 검사 드래그 방지
        {
            protected override void WndProc(ref Message m)
            {
                const int WM_LBUTTONDOWN = 0x0201;
                const int WM_LBUTTONDBLCLK = 0x0203;
                const int WM_MOUSEMOVE = 0x0200;

                if (m.Msg == WM_LBUTTONDOWN || m.Msg == WM_LBUTTONDBLCLK || m.Msg == WM_MOUSEMOVE)
                    return;

                base.WndProc(ref m);
            }
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


        #endregion

        #region 라벨 출력
        //시스템 보안 상태가 안전합니다

        [NonSerialized]
        private string _securityState;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Security_State
        {
            get { return _securityState; }
            set
            {
                _securityState = value.ToString();
                //label2.Text = _securityState;
            }
        }

        //번역할 때 사용
        [NonSerialized]
        private string _recentUpdate;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Recent_Update
        {
            get { return _recentUpdate; }
            set
            {
                _recentUpdate = value.ToString();
                label3.Text = _recentUpdate;
            }
        }

        [NonSerialized]
        private string _recentUpdate2;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Recent_Update2
        {
            get { return _recentUpdate2; }
            set
            {
                _recentUpdate2 = value.ToString();
                label2.Text = _recentUpdate2;
            }
        }

        [NonSerialized]
        private string _recentTest;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Recent_Test
        {
            get { return _recentTest; }
            set
            {
                _recentTest = value.ToString();
                label4.Text = _recentTest;
            }
        }

        [NonSerialized]
        private string _recentTest2;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Recent_Test2
        {
            get { return _recentTest2; }
            set
            {
                _recentTest2 = value.ToString();
                label5.Text = _recentTest2;
            }
        }

        [NonSerialized]
        private string _updateVersion;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Update_Version
        {
            get { return _updateVersion; }
            set
            {
                _updateVersion = value.ToString();
                label1.Text = _updateVersion;
            }
        }

        #endregion

        #region 버튼 이벤트
        private void Button_Click1(object sender, EventArgs e)
        {
            UtilController.GetStop();
            F_controller.Form1_button1(this);
        }

        private void Button_Click2(object sender, EventArgs e)
        {
            UtilController.GetStop();
            F_controller.Form1_button2(this);

        }

        #endregion

        #region 언어 번역
        [NonSerialized]
        private string _button_text;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Button_Text
        {
            get { return _button_text; }
            set
            {
                _button_text = value.ToString();
                button.Text = _button_text;
            }
        }
        [NonSerialized]
        private string _button_text2;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Button_Text2
        {
            get { return _button_text2; }
            set
            {
                _button_text2 = value.ToString();
                button2.Text = _button_text2;
            }
        }
        [NonSerialized]
        private string real_time_switch;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Real_Time_Switch
        {
            get { return real_time_switch; }
            set
            {
                real_time_switch = value;
                noDragRichTextBox1.Text = real_time_switch;
            }
        }
        [NonSerialized]
        private string version_check;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Version_Check
        {
            get { return version_check; }
            set
            {
                version_check = value.ToString();
                label6.Text = version_check;
            }
        }


        #endregion


        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateScanInfoUI();

            string EnvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");
            Env.Load(EnvPath);

            string version = Env.GetString("VERSION", "default_version");

            F_controller.Update_Version_1(version);

            round_button(button);
            round_button(button2);

            F_controller.UpdateFormLabelsFromCsv(Env.GetString("COMBOBOX_LANGUAGE", "한국어"));
       
        }

        // 검사 시간 및 악성 여부 UI 갱신
        public void UpdateScanInfoUI()
        {
            // 1. .env에서 IS_MALWARE 값을 읽기
            Env.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env"));
            string isMalware = Env.GetString("IS_MALWARE", "false");
            bool hasMalware = isMalware.ToLower() == "true";

            string shieldPath1 = Env.GetString("FORM1_IMAGE1", "img/form1_shield1.png");
            string shieldPath2 = Env.GetString("FORM1_IMAGE2", "img/form1_shield2.png");
            string settingPath = Env.GetString("settingPath", "img/setting.png");

            string imagepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, hasMalware ? shieldPath2 : shieldPath1);

            pictureBox2.Image = Image.FromFile(settingPath);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            if (File.Exists(imagepath))
            {
                pictureBox1.Image = Image.FromFile(imagepath);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // 필요 시 조정
            }
            else
            {
                Console.WriteLine($"이미지 파일이 존재하지 않습니다: {imagepath}");
                pictureBox1.Image = null;
            }


            string version = Env.GetString("VERSION", "Unverified");
            label3.Text = $": {version}";

            DateTime? lastScan = UtilController.
            GetLastScanTime();
            if (lastScan.HasValue)
            {
                string relativeTime = FormatRelativeTime(lastScan.Value);
                if (relativeTime != lastRelativeTime)
                {
                    label4.Text = $": {relativeTime}";
                    lastRelativeTime = relativeTime;
                }
            }
            else
            {
                label4.Text = "No scan history";
                lastRelativeTime = "";
            }

            // 렌더링 호환성 보장
            label3.UseCompatibleTextRendering = true;
            label4.UseCompatibleTextRendering = true;

        }

        // 상대 시간 포맷
        private string FormatRelativeTime(DateTime time)
        {
            TimeSpan diff = DateTime.Now - time;

            if (diff.TotalMinutes < 1)
                return "Just now";
            if (diff.TotalMinutes < 60)
                return $"{(int)diff.TotalMinutes} min ago";
            if (diff.TotalHours < 24)
                return $"{(int)diff.TotalHours} hours ago";
            else
                return $"{(int)diff.TotalDays} days ago";
        }

        //다시 돌아왔을때 result.csv 파일을 지우는 코드
        private void Form1_Activated(object sender, EventArgs e)
        {
            string resultPath = Env.GetString("RESULT_PATH", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.csv"));
            if (File.Exists(resultPath))
            {
                File.Delete(resultPath);
                Console.WriteLine("result.csv 삭제됨");
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (formsetting == null || formsetting.IsDisposed)
            {
                formsetting = new FormSetting(this, isRealTimeScanOn); // 넘길 때 현재 상태 넘기기
                formsetting.Show();
            }
            else
            {
                formsetting.BringToFront();
            }
        }

        public static void UpdateRealTimeScanStatus(bool isOn)
        {
            isRealTimeScanOn = isOn;

            if (isOn)
            {
                noDragRichTextBox2.Text = "ON";
                noDragRichTextBox2.Select(noDragRichTextBox2.Text.IndexOf("ON"), 2);
                noDragRichTextBox2.SelectionColor = Color.LimeGreen;
            }
            else
            {
                noDragRichTextBox2.Text = "OFF";
                noDragRichTextBox2.Select(noDragRichTextBox2.Text.IndexOf("OFF"), 3);
                noDragRichTextBox2.SelectionColor = Color.Red;
            }
        }

        private void LoadRealTimeScanStatus()
        {
            try
            {
                Env.Load(EnvPath);
                string value = Env.GetString("REALTIME_SCAN");
                string scanIntervalStr = Env.GetString("SCAN_INTERVAL");

                if (!string.IsNullOrEmpty(scanIntervalStr) && int.TryParse(scanIntervalStr, out int hours))
                {
                    UtilController.UpdateInterval(hours);
                }

                if (string.IsNullOrEmpty(value) || value == "ON")
                {
                    isRealTimeScanOn = true;
                    UtilController.GetStart();
                }
                else
                {
                    isRealTimeScanOn = false;
                }
            }
            catch
            {
                isRealTimeScanOn = true; // 오류 시 기본 ON
            }

            UpdateRealTimeScanStatus(isRealTimeScanOn);
        }

        // 설정 버튼 클릭
        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if (formsetting == null || formsetting.IsDisposed)
            {
                formsetting = new FormSetting(this, isRealTimeScanOn);
                formsetting.Show();
            }
            else
            {
                formsetting.BringToFront();
            }
        }

    }
}