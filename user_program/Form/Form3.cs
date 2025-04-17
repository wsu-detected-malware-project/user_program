using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using user_program.controller;
using user_program.util;
using DotNetEnv;


namespace user_program
{
    public partial class Form3 : Form
    {
        private Controller _controller = Controller.Singleton;

        private string protect_or_malware; // 문자열 인자를 저장할 변수
        private List<string> malwareNames = new List<string>();

        public Form3()
        {
            InitializeComponent();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            _controller.form3 = this;
        }
        public Form3(string _protect_or_malware, List<string> _malwareNames)
        {
            InitializeComponent();
            _controller.form3 = this;
            
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);

            protect_or_malware = _protect_or_malware;
            malwareNames = _malwareNames;

            // .env 파일에 악성 여부 기록
            bool hasMalware = malwareNames.Count > 0;
            EnvManager.SetMalwareStatus(hasMalware);

            // Form1이 열려 있다면 UI 실시간 반영
            if (Application.OpenForms["Form1"] is Form1 form1)
            {
                form1.Invoke(() =>
                {
                    form1.UpdateScanInfoUI();
                });
            }
        }

        // 이미지 로드 함수
        public void LoadImage_Protect()
        {
            string imagePath = Env.GetString("SHIELD_IMAGE1", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "shield1.jpg"));

            pictureBox1.Image = System.Drawing.Image.FromFile(imagePath);

        }

        public void LoadImage_Malware()
        {
            string imagePath = Env.GetString("SHIELD_IMAGE2", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "shield2.jpg"));

            pictureBox1.Image = System.Drawing.Image.FromFile(imagePath);

            _controller.C_Test_Print_List(malwareNames);

        }

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
        private void Form3_Load(object sender, EventArgs e)
        {
            if (protect_or_malware == "protect")
            {
                _controller.C_Print_Image_Protect();
            }
            else if (protect_or_malware == "malware")
            {
                _controller.C_Print_Image_Malware();  // p_invest 관련 처리
                //_controller.C_Test_Print_List();
            }
        }

        //Form3 검사 내역 출력
        public void Test_Print_List(List<string> msg)
        {
            this.Show();

            foreach (var name in msg)
            {
                listBox1.Items.Add(name);
            }    
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Controller.Singleton.form1.UpdateScanInfoUI();
            Controller.Singleton.form1.Show();
        }
    }
}
