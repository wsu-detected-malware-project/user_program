using System.ComponentModel;
using Microsoft.Win32;  // 자동 실행 위해 필요
using DotNetEnv;
using user_program.Controller;

namespace user_program.FormAll {
    public partial class Form1 : Form {
        private FormController F_controller = FormController.Singleton;
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private System.Windows.Forms.Timer autoUpdateTimer;

        private string lastRelativeTime = "";

        public Form1() {
            // 실행 위치 고정
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // .env 로드
            DotNetEnv.Env.Load();

            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
            InitializeTrayIcon();
            StartAutoUpdateTimer();
            F_controller.form1 = this;
            UtilController.GetStart();

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

        private void StartAutoUpdateTimer() {
            autoUpdateTimer = new System.Windows.Forms.Timer();
            autoUpdateTimer.Interval = 5000; // 30초마다 갱신
            autoUpdateTimer.Tick += (s, e) => UpdateScanInfoUI();
            autoUpdateTimer.Start();
        }

        private void InitializeTrayIcon() {
            trayIcon = new NotifyIcon();

            // .ico 경로 가져오기 (.env에서 ICON_PATH 지정, 없으면 기본값 사용)
            string iconPath = Env.GetString("ICON_PATH", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pe_checker.ico"));

            // 아이콘 로딩 시도 → 실패하면 기본 아이콘으로 대체
            try {
                trayIcon.Icon = new Icon(iconPath);
            }
            catch (Exception ex) {
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

        private void OnOpen(object sender, EventArgs e) {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void OnExit(object sender, EventArgs e) {
            trayIcon.Visible = false;
            Environment.Exit(0); // 완전 강제 종료
        }

        private void Form1_Resize(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Minimized) {
                this.Hide();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true; // [X] 버튼 눌러도 숨김 처리
            this.Hide();
        }

        private void SetAutoStart(bool enable) {
            string runKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(runKey, true);

            if (enable) {
                key.SetValue("PEAnalyzer", Application.ExecutablePath);
            }
            else {
                key.DeleteValue("PEAnalyzer", false);
            }
        }

        #region 바 이벤트
        bool mouseDown;
        Point lastlocation;
        private void lbl_title_MouseDown(object sender, MouseEventArgs e) {
            mouseDown = true;
            lastlocation = e.Location;
        }
        private void lbl_title_MouseUp(object sender, MouseEventArgs e) {
            mouseDown = false;
        }

        private void lbl_title_MouseMove(object sender, MouseEventArgs e) {
            if (mouseDown) {
                this.Location = new Point(
                    (this.Location.X - lastlocation.X) + e.X,
                    (this.Location.Y - lastlocation.Y) + e.Y);

                this.Update();
            }
        }

        private void btn_min_Click(object sender, MouseEventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btn_max_Click(object sender, MouseEventArgs e) {
            if (this.WindowState == FormWindowState.Normal) {
                this.WindowState = FormWindowState.Maximized;
            }
            else {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btn_close_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btn_close_Click(object sender, MouseEventArgs e) {
            this.Close();
        }

        private void lbl_title_Click(object sender, EventArgs e) {

        }
        #endregion
        #region 라벨 출력

        //시스템 보안 상태가 안전합니다
        
        [NonSerialized]
        private string _securityState;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Security_State {
            get { return _securityState; }
            set {
                _securityState = value.ToString();
                label2.Text = _securityState;
            }
        }

        [NonSerialized]
        private string _recentUpdate;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Recent_Update {
            get { return _recentUpdate; }
            set {
                _recentUpdate = value.ToString();
                label3.Text = _recentUpdate;
            }
        }

        [NonSerialized]
        private string _recentTest;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Recent_Test {
            get { return _recentTest; }
            set {
                _recentTest = value.ToString();
                label4.Text = _recentTest;
            }
        }

        [NonSerialized]
        private string _updateVersion;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Update_Version {
            get { return _updateVersion; }
            set {
                _updateVersion = value.ToString();
                label1.Text = _updateVersion;
            }
        }

        #endregion
        #region 버튼 이벤트
        private void Button_Click1(object sender, EventArgs e) {
            F_controller.Form1_button1(this);
        }

        private void Button_Click2(object sender, EventArgs e) {
            F_controller.Form1_button2(this);
            
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e) {
            UpdateScanInfoUI();

            string EnvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");
            Env.Load(EnvPath);

            string version = Env.GetString("VERSION", "default_version");

            F_controller.Update_Version_1(version);
        }

        // 검사 시간 및 악성 여부 UI 갱신
        public void UpdateScanInfoUI() {
            // 1. .env에서 IS_MALWARE 값을 읽기
            Env.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env"));
            string isMalware = Env.GetString("IS_MALWARE", "false");
            bool hasMalware = isMalware.ToLower() == "true";

            Color backgroundColor = hasMalware ? Color.Red : Color.Blue;
            
            panel1.BackColor = backgroundColor;

            label2.BackColor = backgroundColor;
            label3.BackColor = backgroundColor;
            label4.BackColor = backgroundColor;

            label2.Text = hasMalware ? "시스템의 보안 상태가 위험합니다" : "시스템의 보안 상태가 안전합니다";

            string version = Env.GetString("VERSION", "알 수 없음");
            label3.Text = $"최근 업데이트: {version}";
            
            DateTime? lastScan = UtilController.
            GetLastScanTime();
            if (lastScan.HasValue) {
                string relativeTime = FormatRelativeTime(lastScan.Value);
                if (relativeTime != lastRelativeTime)
                {
                    label4.Text = $"마지막 검사: {relativeTime}";
                    lastRelativeTime = relativeTime;
                }
            }
            else {
                label4.Text = "검사 이력 없음";
                lastRelativeTime = "";
            }

            // 렌더링 호환성 보장
            label2.UseCompatibleTextRendering = true;
            label3.UseCompatibleTextRendering = true;
            label4.UseCompatibleTextRendering = true;
        }

        // 상대 시간 포맷
        private string FormatRelativeTime(DateTime time) {
            TimeSpan diff = DateTime.Now - time;

            if (diff.TotalMinutes < 1)
                return "조금 전";
            if (diff.TotalMinutes < 60)
                return $"{(int)diff.TotalMinutes}분 전";
            if (diff.TotalHours < 24)
                return $"{(int)diff.TotalHours}시간 전";
            else
                return $"{(int)diff.TotalDays}일 전";
        }

        //다시 돌아왔을때 result.csv 파일을 지우는 코드
        private void Form1_Activated(object sender, EventArgs e) {
            string resultPath = Env.GetString("RESULT_PATH", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.csv"));
            if (File.Exists(resultPath)) {
                File.Delete(resultPath);
                Console.WriteLine("result.csv 삭제됨");
            }
        }
    }
}
