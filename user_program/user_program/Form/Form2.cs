using System.ComponentModel;
using DotNetEnv;
using user_program.Controller;

namespace user_program.FormAll {
    public partial class Form2 : Form {
        private FormController F_controller = FormController.Singleton;
        private PEController PE_controller = PEController.Singleton;
        private NetController Net_controller = NetController.Singleton;
        private bool hasFReadExecuted = false;  // F_Read가 한 번 실행되었는지 확인하는 변수
        private string investType; // 문자열 인자를 저장할 변수
        
        public Form2(string _investType) {
            InitializeComponent();
            F_controller.form2 = this;

            investType = _investType;
        }

        // 기본 생성자 추가
        public Form2() {
            InitializeComponent();
        }

        #region 라벨 출력
        [NonSerialized]
        private string _updateVersion;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

        public string Update_Version {
            get { return _updateVersion; }
            set {
                _updateVersion = value.ToString();
                label2.Text = _updateVersion;
            }
        }
        #endregion

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
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
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
        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e) {}
        #endregion

        private async Task WaitForFileAsync(string path, int maxWait = 60000) {
            int waited = 0;
            long previousLength = -1;

            while (waited < maxWait) {
                if (File.Exists(path)) {
                    long length = new FileInfo(path).Length;
                    if (length > 0 && length == previousLength)
                        return;

                    previousLength = length;
                }
                await Task.Delay(500);
            }
            throw new TimeoutException("파일이 안정적으로 저장되지 않았습니다.");
        }

        private async void timer1_Tick(object sender, EventArgs e) {
            // 로딩바 값이 최대값보다 작을 때만 증가시킴
            if (progressBar1.Value < progressBar1.Maximum) {
                // 로딩바가 1칸 차면 F_Read를 실행
                if (listBox1.Items.Count == 0 && !hasFReadExecuted) {
                    Task.Run(() => {
                    if (investType == "f_invest") {
                        PE_controller.get_f_invest();
                    }
                    else if (investType == "p_invest") {
                        PE_controller.get_p_invest();
                    }
                    }).Wait();
                    hasFReadExecuted = true;
                }
            }
            else {           
                // 로딩바가 100에 도달했을 때
                timer1.Stop();  // 타이머를 멈춤
                
                await Task.Delay(500);  // 예: 500ms 대기 (필요에 따라 시간 조정)

                bool success = await Net_controller.network();

                string filePath = Env.GetString("RESULT_PATH", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.csv"));

                await WaitForFileAsync(filePath, 100000000);

                string[] lines = File.ReadAllLines(filePath);
                string result = "";

                List<string> malwareNames = new List<string>();

                for (int i = 1; i < lines.Length; i++) {
                    string[] columns = lines[i].Split(',');
                    malwareNames.Add(columns[0]);
                }
                if (malwareNames.Count == 0) {
                    result = "protect";
                }
                else {
                    result = "malware";
                }

                this.Hide();
                Form3 form3 = new Form3(result, malwareNames);
                form3.ShowDialog();
            }
        }


        private void Form2_Load(object sender, EventArgs e) {
            string EnvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");
            Env.Load(EnvPath);

            string version = Env.GetString("VERSION", "default_version");

            F_controller.Update_Version_2(version);

            progressBar1.Maximum = PE_controller.get_totalfile();
            progressBar1.Value = 1;
            
            // 타이머를 활성화하여 프로세스 시작
            timer1.Interval = 1;
            timer1.Start();

        }

        public ListBox Give_Listbox() {
            return listBox1;
        }

        public System.Windows.Forms.ProgressBar Give_progressBar() { 
            return progressBar1;
        }
    }
}