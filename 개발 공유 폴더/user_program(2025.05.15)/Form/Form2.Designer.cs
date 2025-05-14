using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Xml.Linq;
using Font = System.Drawing.Font; //**
using user_program.FormAll;

namespace user_program.FormAll {
    partial class Form2 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            progressBar1 = new ProgressBar();
            timer1 = new System.Windows.Forms.Timer(components);
            label2 = new Label();
            btn_close = new Button();
            btn_max = new Button();
            btn_min = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            lbl_title = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            panel1 = new Panel();
            listBox1 = new ListBox();
            panel2 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            label1 = new Label();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.BackColor = Color.Black;
            progressBar1.Dock = DockStyle.Fill;
            progressBar1.Location = new Point(1, 1);
            progressBar1.Margin = new Padding(0);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(562, 20);
            progressBar1.TabIndex = 5;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Right;
            label2.Font = new Font("서울한강체 EB", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label2.ForeColor = Color.Snow;
            label2.Location = new Point(202, 0);
            label2.Name = "label2";
            label2.Size = new Size(93, 21);
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
            btn_close.Location = new Point(595, 4);
            btn_close.Margin = new Padding(3, 4, 3, 4);
            btn_close.Name = "btn_close";
            btn_close.Size = new Size(29, 34);
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
            btn_max.Location = new Point(560, 4);
            btn_max.Margin = new Padding(3, 4, 3, 4);
            btn_max.Name = "btn_max";
            btn_max.Size = new Size(29, 34);
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
            btn_min.Location = new Point(525, 4);
            btn_min.Margin = new Padding(3, 4, 3, 4);
            btn_min.Name = "btn_min";
            btn_min.Size = new Size(29, 34);
            btn_min.TabIndex = 1;
            btn_min.Text = "-";
            btn_min.UseVisualStyleBackColor = false;
            btn_min.MouseClick += btn_min_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(24, 24, 24);
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
            tableLayoutPanel1.Size = new Size(627, 42);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_title
            // 
            lbl_title.AutoSize = true;
            lbl_title.BackColor = Color.Transparent;
            lbl_title.Dock = DockStyle.Bottom;
            lbl_title.Font = new Font("서울한강체 EB", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbl_title.ForeColor = SystemColors.Control;
            lbl_title.Location = new Point(3, 15);
            lbl_title.Name = "lbl_title";
            lbl_title.Size = new Size(516, 27);
            lbl_title.TabIndex = 4;
            lbl_title.Text = "WSU MALWARE";
            lbl_title.MouseDown += lbl_title_MouseDown;
            lbl_title.MouseMove += lbl_title_MouseMove;
            lbl_title.MouseUp += lbl_title_MouseUp;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.Snow;
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(0, 42);
            flowLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(627, 1);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Controls.Add(listBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(31, 53);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(1);
            panel1.Size = new Size(564, 379);
            panel1.TabIndex = 10;
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.SlateGray;
            listBox1.BorderStyle = BorderStyle.None;
            listBox1.Font = new Font("서울한강체 EB", 11.249999F, FontStyle.Regular, GraphicsUnit.Point, 129);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(0, 0);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(564, 375);
            listBox1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Black;
            panel2.Controls.Add(progressBar1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(31, 22);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(1);
            panel2.Size = new Size(564, 22);
            panel2.TabIndex = 11;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.FromArgb(26, 26, 26);
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.Controls.Add(panel2, 1, 1);
            tableLayoutPanel2.Controls.Add(panel1, 1, 3);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 1, 4);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 43);
            tableLayoutPanel2.Margin = new Padding(2);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 2F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 83F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.Size = new Size(627, 457);
            tableLayoutPanel2.TabIndex = 12;
            tableLayoutPanel2.Paint += tableLayoutPanel2_Paint;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(label2, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Right;
            tableLayoutPanel3.Location = new Point(295, 434);
            tableLayoutPanel3.Margin = new Padding(2);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(298, 21);
            tableLayoutPanel3.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 26, 26);
            ClientSize = new Size(627, 500);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WSU Malware";
            TransparencyKey = Color.Transparent;
            Load += Form2_Load;
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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Button btn_min;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private Panel panel1;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private ListBox listBox1;
        private Label label1;
    }
}

