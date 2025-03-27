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
using DotNetEnv;

namespace user_program
{
    public partial class Form1 : Form
    {
        private Controller _controller = Controller.Singleton;

        public Form1()
        {
            InitializeComponent();
            _controller.form1 = this;
        }

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
                label2.Text = _securityState;
            }
        }

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

        #region 기초 틀

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

        #endregion


        private void Button_Click(object sender, EventArgs e)
        {
            this.Hide();

            // 빠른 검사 (f_invest 실행)
            Form2 form2 = new Form2("f_invest");
            form2.ShowDialog();

            //_controller.get_f_invest();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Hide();

            // 정밀 검사 (p_invest 실행)
            Form2 form2 = new Form2("p_invest");
            form2.ShowDialog();

            //_controller.get_p_invest();

            this.Show();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //임시로 값이 들어오는 지 확인하기 위한 코드 경로처리 잘 할 것.
            Env.Load(@"C:\Users\pth81\Desktop\com\com\.env");

            string version = Env.GetString("VERSION", "default_version");

            _controller.C_Update_Version_1(version);
        }
    }
}