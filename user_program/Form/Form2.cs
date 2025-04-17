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
using Microsoft.VisualBasic.Devices;

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

    private async Task WaitForFileAsync(string path, int maxWait = 60000) //167번째 코드 실행을 위한 구문, result파일 생길때까지 기다리는 기능
    {
        int waited = 0;
        long previousLength = -1;

        while (waited < maxWait)
        {
            if (File.Exists(path))
            {
                long length = new FileInfo(path).Length;
                if (length > 0 && length == previousLength)
                    return; // 파일 크기가 변하지 않으면 안정적으로 저장 완료된 것으로 간주

                previousLength = length;
            }
            await Task.Delay(500);
        }
        throw new TimeoutException("파일이 안정적으로 저장되지 않았습니다.");
    }

    private async void timer1_Tick(object sender, EventArgs e)
    {
        // 로딩바 값이 최대값보다 작을 때만 증가시킴
        if (progressBar1.Value < progressBar1.Maximum)
        {
            // 로딩바가 1칸 차면 F_Read를 실행
            if (listBox1.Items.Count == 0 && !hasFReadExecuted)
            {
                Task.Run(() =>
                {
                if (investType == "f_invest")
                {
                    _controller.get_f_invest();
                }
                else if (investType == "p_invest")
                {
                    _controller.get_p_invest();  // p_invest 관련 처리
                }
                }).Wait();
                hasFReadExecuted = true;  // F_Read가 실행되었음을 표시
            }
        }
        else
        {           
            // 로딩바가 100에 도달했을 때
            timer1.Stop();  // 타이머를 멈춤
            
            await Task.Delay(500);  // 예: 500ms 대기 (필요에 따라 시간 조정)

            bool success = await _controller.network();

            string filePath = Env.GetString("RESULT_PATH", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.csv"));

            await WaitForFileAsync(filePath, 100000000);

            string[] lines = File.ReadAllLines(filePath);
            string result = "what";

            List<string> malwareNames = new List<string>();

            // 첫 번째 줄은 컬럼명이므로 건너뜁니다
            for (int i = 1; i < lines.Length; i++)
            {
                string[] columns = lines[i].Split(',');
                malwareNames.Add(columns[0]);
            }
            // malwareNames 리스트에 값이 없으면 "fail" 반환
            if (malwareNames.Count == 0)
            {
                result = "protect";
            }
            // malwareNames 리스트에 값이 있으면 "succ" 반환
            else
            {
                result = "malware";
            }

            this.Hide();
            //Form3 form3 = new Form3("protect");
            Form3 form3 = new Form3(result, malwareNames);
            form3.ShowDialog(); // Form3를 띄운다
        }
    }

    private void Form2_Load(object sender, EventArgs e)
    {
        //임시 확인용
        string EnvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");
        Env.Load(EnvPath);

        string version = Env.GetString("VERSION", "default_version");

        _controller.C_Update_Version_2(version);
        //----------------

        progressBar1.Maximum = _controller.testcode_c();
        progressBar1.Value = 1;      // 초기값 설정
        
        // 타이머를 활성화하여 프로세스 시작
        timer1.Interval = 1;
        timer1.Start();

    }

    //Form2 내역들 출력
    public void Print_List(string msg)
    {
        this.Show();
        listBox1.Items.Add(msg);

        // 자동으로 스크롤을 맨 아래로 내리기
        listBox1.SelectedIndex = -1;  // 선택된 항목을 없애기
        listBox1.TopIndex = listBox1.Items.Count - 1;  // 자동으로 스크롤을 맨 아래로 내리기

        // 로딩바 값 증가
        progressBar1.Value = listBox1.Items.Count;  // 리스트박스에 아이템 갯수만큼 증가
    }
}