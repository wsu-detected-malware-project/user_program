namespace user_program;

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
        label1 = new Label();
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
        tableLayoutPanel1.SuspendLayout();
        panel1.SuspendLayout();
        panel2.SuspendLayout();
        SuspendLayout();
        // 
        // progressBar1
        // 
        progressBar1.Dock = DockStyle.Fill;
        progressBar1.Location = new Point(1, 1);
        progressBar1.Margin = new Padding(0);
        progressBar1.Name = "progressBar1";
        progressBar1.Size = new Size(778, 28);
        progressBar1.TabIndex = 5;
        // 
        // timer1
        // 
        timer1.Tick += timer1_Tick;
        // 
        // button1
        // 
        button1.Location = new Point(456, 465);
        button1.Margin = new Padding(3, 4, 3, 4);
        button1.Name = "button1";
        button1.Size = new Size(75, 29);
        button1.TabIndex = 6;
        button1.Text = "시작";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(27, 472);
        label1.Name = "label1";
        label1.Size = new Size(42, 15);
        label1.TabIndex = 7;
        label1.Text = "상태 : ";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(672, 472);
        label2.Name = "label2";
        label2.Size = new Size(104, 15);
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
        btn_close.Location = new Point(768, 4);
        btn_close.Margin = new Padding(3, 4, 3, 4);
        btn_close.Name = "btn_close";
        btn_close.Size = new Size(29, 42);
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
        btn_max.Location = new Point(733, 4);
        btn_max.Margin = new Padding(3, 4, 3, 4);
        btn_max.Name = "btn_max";
        btn_max.Size = new Size(29, 42);
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
        btn_min.Location = new Point(698, 4);
        btn_min.Margin = new Padding(3, 4, 3, 4);
        btn_min.Name = "btn_min";
        btn_min.Size = new Size(29, 42);
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
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
        tableLayoutPanel1.Controls.Add(btn_min, 1, 0);
        tableLayoutPanel1.Controls.Add(btn_max, 2, 0);
        tableLayoutPanel1.Controls.Add(btn_close, 3, 0);
        tableLayoutPanel1.Controls.Add(lbl_title, 0, 0);
        tableLayoutPanel1.Dock = DockStyle.Top;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(800, 50);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // lbl_title
        // 
        lbl_title.AutoSize = true;
        lbl_title.BackColor = Color.Transparent;
        lbl_title.Dock = DockStyle.Bottom;
        lbl_title.Font = new Font("굴림", 18F, FontStyle.Bold, GraphicsUnit.Point, 129);
        lbl_title.ForeColor = SystemColors.Control;
        lbl_title.Location = new Point(3, 26);
        lbl_title.Name = "lbl_title";
        lbl_title.Size = new Size(689, 24);
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
        flowLayoutPanel1.Location = new Point(0, 50);
        flowLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Size = new Size(800, 4);
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
        richTextBox1.Size = new Size(778, 318);
        richTextBox1.TabIndex = 9;
        richTextBox1.Text = "";
        // 
        // panel1
        // 
        panel1.BackColor = Color.Black;
        panel1.Controls.Add(richTextBox1);
        panel1.Location = new Point(8, 138);
        panel1.Margin = new Padding(0);
        panel1.Name = "panel1";
        panel1.Padding = new Padding(1);
        panel1.Size = new Size(780, 320);
        panel1.TabIndex = 10;
        // 
        // panel2
        // 
        panel2.BackColor = Color.Black;
        panel2.Controls.Add(progressBar1);
        panel2.Location = new Point(8, 102);
        panel2.Margin = new Padding(0);
        panel2.Name = "panel2";
        panel2.Padding = new Padding(1);
        panel2.Size = new Size(780, 30);
        panel2.TabIndex = 11;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Azure;
        ClientSize = new Size(800, 500);
        Controls.Add(panel2);
        Controls.Add(panel1);
        Controls.Add(label1);
        Controls.Add(label2);
        Controls.Add(button1);
        Controls.Add(flowLayoutPanel1);
        Controls.Add(tableLayoutPanel1);
        FormBorderStyle = FormBorderStyle.None;
        Margin = new Padding(3, 4, 3, 4);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Form1";
        TransparencyKey = Color.Transparent;
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        panel1.ResumeLayout(false);
        panel2.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    private RichTextBox richTextBox1;
    private Panel panel1;
    private Panel panel2;
}