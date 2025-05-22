using DotNetEnv;
using user_program.Controller;
using user_program.Util;

namespace user_program.FormAll {
    public partial class Form3 : Form
    {
        private FormController F_controller = FormController.Singleton;

        private string protect_or_malware; // 문자열 인자를 저장할 변수
        private List<int> highlightedColumns = new List<int>();
        private List<string> malwareNames = new List<string>();

        public Form3()
        {
            InitializeComponent();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            F_controller.form3 = this;
        }
        public Form3(string _protect_or_malware, List<string> _malwareNames)
        {
            InitializeComponent();
            F_controller.form3 = this;

            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);

            protect_or_malware = _protect_or_malware;
            malwareNames = _malwareNames;

            // .env 파일에 악성 여부 기록
            bool hasMalware = malwareNames.Count > 0;
            UtilController.GetSetMalwareStatus(hasMalware);

            // Form1이 열려 있다면 UI 실시간 반영
            if (Application.OpenForms["Form1"] is Form1 form1)
            {
                form1.Invoke(() =>
                {
                    form1.UpdateScanInfoUI();
                });
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

        private void lbl_title_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion

        public void LoadImage_Malware()
        {
            string imagePath = Env.GetString("SHIELD_IMAGE2", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "shield2.jpg"));

            pictureBox1.Image = System.Drawing.Image.FromFile(imagePath);

            F_controller.Print_Invest_List2(this, listBox1, malwareNames);

        }

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e) //form3 애매한 값 줄 출력 변경
        {
            if (e.Index < 0) return;

            // 강조 색상 조건
            bool highlight = highlightedColumns.Contains(e.Index);
            Color bgColor = highlight ? Color.Orange : listBox1.BackColor;
            Color fgColor = highlight ? Color.Black : listBox1.ForeColor;

            // 배경 직접 그리기
            using (Brush bgBrush = new SolidBrush(bgColor))
            {
                e.Graphics.FillRectangle(bgBrush, e.Bounds);
            }

            // 텍스트 렌더링 품질 향상
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // 텍스트 그리기
            string text = listBox1.Items[e.Index].ToString();
            using (Brush fgBrush = new SolidBrush(fgColor))
            {
                e.Graphics.DrawString(text, e.Font, fgBrush, e.Bounds.X, e.Bounds.Y);
            }

            // 포커스 사각형
            e.DrawFocusRectangle();
        }

        public void LoadNamesFromColumnWithHighlight() //form3 애매한 값 줄 출력 변경
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.csv");
            if (!File.Exists(path))
            {
                MessageBox.Show("result.csv 파일이 없습니다.");
                return;
            }

            var lines = File.ReadAllLines(path);
            if (lines.Length < 2) return;

            for (int i = 1; i < lines.Length; i++) // 첫 줄은 헤더
            {
                string[] columns = lines[i].Split(',');

                if (columns.Length <= 1) continue;

                string name = columns[0];
                if (double.TryParse(columns[1], out double val))
                {
                    listBox1.Items.Add(name);

                    if (val >= 0.5 && val < 0.7)
                    {
                        highlightedColumns.Add(listBox1.Items.Count - 1);
                    }
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (protect_or_malware == "protect")
            {
                F_controller.Print_Image_Shield1(pictureBox1);
            }
            else if (protect_or_malware == "malware")
            {
                F_controller.Print_Image_Shield2(pictureBox1, this, listBox1, malwareNames);
            }

            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox1.DrawItem += ListBox1_DrawItem;

            LoadNamesFromColumnWithHighlight();

            WarnList.Generate();

            string resultPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.csv");
            MalwareCleaner.DeleteFiles(resultPath);

            string warningExe = Env.GetString("IFEO_PATH", ""); 
            if (!string.IsNullOrEmpty(warningExe))
            {
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, warningExe);
                //RegisterIFEO.RegisterWarnListToIFEO(fullPath);
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {

            F_controller.Form3_Close_After(sender, e);
        }

      
    }

}
