using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace user_program;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
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

    private void button1_Click(object sender, EventArgs e)
    {
        progressBar1.Maximum = 100; ///최대값을 설정한다. 가령 progressBar1의 값이 10 인 경우 1프로에 해당한다.
        this.timer1.Enabled = true;
        button1.Enabled = false;
    }

    private void timer1_Tick(object sender, EventArgs e)
    {

        if (progressBar1.Value == progressBar1.Maximum)
        {
            this.timer1.Enabled = false;
            button1.Enabled = true;
            return;
        }
        this.progressBar1.Value = this.progressBar1.Value + 1;
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void Form1_Load_1(object sender, EventArgs e)
    {

    }
}
