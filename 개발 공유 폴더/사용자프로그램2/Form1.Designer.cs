﻿namespace user_program;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        progressBar1 = new ProgressBar();
        timer1 = new System.Windows.Forms.Timer(components);
        button1 = new Button();
        label2 = new Label();
        btn_close = new Button();
        btn_max = new Button();
        btn_min = new Button();
        tableLayoutPanel1 = new TableLayoutPanel();
        lbl_title = new Label();
        flowLayoutPanel1 = new FlowLayoutPanel();
        richTextBox1 = new RichTextBox();
        panel1 = new Panel();
        panel2 = new Panel();
        tableLayoutPanel2 = new TableLayoutPanel();
        tableLayoutPanel3 = new TableLayoutPanel();
        tableLayoutPanel1.SuspendLayout();
        panel1.SuspendLayout();
        panel2.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        SuspendLayout();
        // 
        // progressBar1
        // 
        progressBar1.Dock = DockStyle.Fill;
        progressBar1.Location = new Point(1, 1);
        progressBar1.Margin = new Padding(0);
        progressBar1.Name = "progressBar1";
        progressBar1.Size = new Size(924, 27);
        progressBar1.TabIndex = 5;
        // 
        // timer1
        // 
        timer1.Tick += timer1_Tick;
        // 
        // button1
        // 
        button1.Location = new Point(4, 5);
        button1.Margin = new Padding(4, 5, 4, 5);
        button1.Name = "button1";
        button1.Size = new Size(117, 16);
        button1.TabIndex = 6;
        button1.Text = "시작";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Dock = DockStyle.Right;
        label2.Location = new Point(257, 0);
        label2.Margin = new Padding(4, 0, 4, 0);
        label2.Name = "label2";
        label2.Size = new Size(122, 27);
        label2.TabIndex = 8;
        label2.Text = "버전 : 2025.03.18";
        // 
        // btn_close
        // 
        btn_close.BackColor = Color.Transparent;
        btn_close.Dock = DockStyle.Fill;
        btn_close.FlatAppearance.BorderSize = 0;
        btn_close.FlatStyle = FlatStyle.Flat;
        btn_close.Font = new Font("굴림", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
        btn_close.ForeColor = SystemColors.Control;
        btn_close.Location = new Point(988, 5);
        btn_close.Margin = new Padding(4, 5, 4, 5);
        btn_close.Name = "btn_close";
        btn_close.Size = new Size(37, 57);
        btn_close.TabIndex = 3;
        btn_close.Text = "x";
        btn_close.UseVisualStyleBackColor = false;
        btn_close.MouseClick += btn_close_Click;
        // 
        // btn_max
        // 
        btn_max.BackColor = Color.Transparent;
        btn_max.Dock = DockStyle.Fill;
        btn_max.FlatAppearance.BorderSize = 0;
        btn_max.FlatStyle = FlatStyle.Flat;
        btn_max.Font = new Font("굴림", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
        btn_max.ForeColor = SystemColors.Control;
        btn_max.Location = new Point(943, 5);
        btn_max.Margin = new Padding(4, 5, 4, 5);
        btn_max.Name = "btn_max";
        btn_max.Size = new Size(37, 57);
        btn_max.TabIndex = 2;
        btn_max.Text = "ㅁ";
        btn_max.UseVisualStyleBackColor = false;
        btn_max.MouseClick += btn_max_Click;
        // 
        // btn_min
        // 
        btn_min.BackColor = Color.Transparent;
        btn_min.BackgroundImageLayout = ImageLayout.None;
        btn_min.Dock = DockStyle.Fill;
        btn_min.FlatAppearance.BorderSize = 0;
        btn_min.FlatStyle = FlatStyle.Flat;
        btn_min.Font = new Font("굴림", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
        btn_min.ForeColor = SystemColors.Control;
        btn_min.Location = new Point(898, 5);
        btn_min.Margin = new Padding(4, 5, 4, 5);
        btn_min.Name = "btn_min";
        btn_min.Size = new Size(37, 57);
        btn_min.TabIndex = 1;
        btn_min.Text = "-";
        btn_min.UseVisualStyleBackColor = false;
        btn_min.MouseClick += btn_min_Click;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.BackColor = SystemColors.GrayText;
        tableLayoutPanel1.ColumnCount = 4;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45F));
        tableLayoutPanel1.Controls.Add(btn_min, 1, 0);
        tableLayoutPanel1.Controls.Add(btn_max, 2, 0);
        tableLayoutPanel1.Controls.Add(btn_close, 3, 0);
        tableLayoutPanel1.Controls.Add(lbl_title, 0, 0);
        tableLayoutPanel1.Dock = DockStyle.Top;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(1029, 67);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // lbl_title
        // 
        lbl_title.AutoSize = true;
        lbl_title.BackColor = Color.Transparent;
        lbl_title.Dock = DockStyle.Bottom;
        lbl_title.Font = new Font("굴림", 18F, FontStyle.Bold, GraphicsUnit.Point, 129);
        lbl_title.ForeColor = SystemColors.Control;
        lbl_title.Location = new Point(4, 37);
        lbl_title.Margin = new Padding(4, 0, 4, 0);
        lbl_title.Name = "lbl_title";
        lbl_title.Size = new Size(886, 30);
        lbl_title.TabIndex = 4;
        lbl_title.Text = "WSU malware";
        lbl_title.Click += lbl_title_Click;
        lbl_title.MouseDown += lbl_title_MouseDown;
        lbl_title.MouseMove += lbl_title_MouseMove;
        lbl_title.MouseUp += lbl_title_MouseUp;
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.BackColor = Color.LightGray;
        flowLayoutPanel1.Dock = DockStyle.Top;
        flowLayoutPanel1.Location = new Point(0, 67);
        flowLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Size = new Size(1029, 5);
        flowLayoutPanel1.TabIndex = 1;
        // 
        // richTextBox1
        // 
        richTextBox1.BorderStyle = BorderStyle.None;
        richTextBox1.Dock = DockStyle.Fill;
        richTextBox1.HideSelection = false;
        richTextBox1.Location = new Point(1, 1);
        richTextBox1.Margin = new Padding(0);
        richTextBox1.Name = "richTextBox1";
        richTextBox1.ReadOnly = true;
        richTextBox1.Size = new Size(924, 491);
        richTextBox1.TabIndex = 9;
        richTextBox1.Text = "";
        // 
        // panel1
        // 
        panel1.BackColor = Color.Black;
        panel1.Controls.Add(richTextBox1);
        panel1.Dock = DockStyle.Fill;
        panel1.Location = new Point(51, 69);
        panel1.Margin = new Padding(0);
        panel1.Name = "panel1";
        panel1.Padding = new Padding(1);
        panel1.Size = new Size(926, 493);
        panel1.TabIndex = 10;
        // 
        // panel2
        // 
        panel2.BackColor = Color.Black;
        panel2.Controls.Add(progressBar1);
        panel2.Dock = DockStyle.Fill;
        panel2.Location = new Point(51, 29);
        panel2.Margin = new Padding(0);
        panel2.Name = "panel2";
        panel2.Padding = new Padding(1);
        panel2.Size = new Size(926, 29);
        panel2.TabIndex = 11;
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.ColumnCount = 3;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
        tableLayoutPanel2.Controls.Add(panel2, 1, 1);
        tableLayoutPanel2.Controls.Add(panel1, 1, 3);
        tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 1, 4);
        tableLayoutPanel2.Dock = DockStyle.Fill;
        tableLayoutPanel2.Location = new Point(0, 72);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 5;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 2F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 83F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
        tableLayoutPanel2.Size = new Size(1029, 595);
        tableLayoutPanel2.TabIndex = 12;
        // 
        // tableLayoutPanel3
        // 
        tableLayoutPanel3.ColumnCount = 2;
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.Controls.Add(button1, 0, 0);
        tableLayoutPanel3.Controls.Add(label2, 1, 0);
        tableLayoutPanel3.Dock = DockStyle.Right;
        tableLayoutPanel3.Location = new Point(591, 565);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 1;
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.Size = new Size(383, 27);
        tableLayoutPanel3.TabIndex = 12;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(9F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Azure;
        ClientSize = new Size(1029, 667);
        Controls.Add(tableLayoutPanel2);
        Controls.Add(flowLayoutPanel1);
        Controls.Add(tableLayoutPanel1);
        FormBorderStyle = FormBorderStyle.None;
        Margin = new Padding(4, 5, 4, 5);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Form1";
        TransparencyKey = Color.Transparent;
        Load += Form1_Load_1;
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        panel1.ResumeLayout(false);
        panel2.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel3.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_max;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Button btn_min;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
    private RichTextBox richTextBox1;
    private Panel panel1;
    private Panel panel2;
    private TableLayoutPanel tableLayoutPanel2;
    private TableLayoutPanel tableLayoutPanel3;
}
