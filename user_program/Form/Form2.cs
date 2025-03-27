using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using user_program.controller;
using DotNetEnv;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace user_program;

public partial class Form2 : Form
{
    private Controller _controller = Controller.Singleton;
    private bool hasFReadExecuted = false;  // F_Read가 한 번 실행되었는지 확인하는 변수
    private string investType; // 문자열 인자를 저장할 변수
    
    public Form2(string _investType)
    {
        InitializeComponent();
        _controller.form2 = this;

        investType = _investType;
    }

    // 기본 생성자 추가
    public Form2()
    {
        InitializeComponent();
    }

    #region 라벨 출력
    [NonSerialized]
    private string _updateVersion;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

    public string Update_Version
    {
        get { return _updateVersion; }
        set
        {
            _updateVersion = value.ToString();
            label2.Text = _updateVersion;
        }
    }

    #endregion

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

    private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e) {}

    private void timer1_Tick(object sender, EventArgs e)
    {
        // 로딩바 값이 최대값보다 작을 때만 증가시킴
        if (progressBar1.Value < progressBar1.Maximum)
        {
            // 로딩바 값 증가
            progressBar1.Value = progressBar1.Value + 1;

            // 로딩바가 1칸 차면 F_Read를 실행
            if (progressBar1.Value == 1 && !hasFReadExecuted)
            {
                //_controller.get_f_invest();
                if (investType == "f_invest")
                {
                    _controller.get_f_invest();
                }
                else if (investType == "p_invest")
                {
                    _controller.get_p_invest();  // p_invest 관련 처리
                }
                hasFReadExecuted = true;  // F_Read가 실행되었음을 표시
            }
        }
        else
        {
            // 로딩바가 100에 도달했을 때
            timer1.Stop();  // 타이머를 멈춤

            this.Hide();  // 현재 폼 숨김 form2에 file명이 잘뜨고 form3로 잘 넘어가는지 확인용

            Form3 form3 = new Form3();
            form3.ShowDialog(); // Form3를 띄운다
        }
    }

    private void Form2_Load(object sender, EventArgs e)
    {
        //임시 확인용
        Env.Load(@"C:\Users\pth81\Desktop\com\com\.env");

        string version = Env.GetString("VERSION", "default_version");

        _controller.C_Update_Version_2(version);
        //----------------

        //Task.Run(() => F_invest.F_Read());
        
        progressBar1.Maximum = 100;  // 최대값 설정 //파일 개수 연동.
        progressBar1.Value = 0;      // 초기값 설정

        // 타이머를 활성화하여 프로세스 시작
        timer1.Interval = 1;
        timer1.Start();

    }

    //Form2 내역들 출력
    public void Print_List(string msg)
    {
        this.Show();
        listBox1.Items.Add(msg);
    }
}
