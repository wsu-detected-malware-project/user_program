using System.Resources;
using System.Windows.Forms;

namespace user_program.FormAll
{
    partial class UpdateProgressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_close = new Button();
            btn_max = new Button();
            btn_min = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            lbl_title = new Label();
            horizonpanel = new Panel();
            progressBar1 = new ProgressBar();
            label1 = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
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
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(627, 42);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_title
            // 
            lbl_title.AutoSize = true;
            lbl_title.BackColor = Color.Transparent;
            lbl_title.Dock = DockStyle.Bottom;
            lbl_title.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbl_title.ForeColor = SystemColors.Control;
            lbl_title.Location = new Point(3, 11);
            lbl_title.Name = "lbl_title";
            lbl_title.Size = new Size(516, 31);
            lbl_title.TabIndex = 4;
            lbl_title.Text = "WSU MALWARE";
            lbl_title.MouseDown += lbl_title_MouseDown;
            lbl_title.MouseMove += lbl_title_MouseMove;
            lbl_title.MouseUp += lbl_title_MouseUp;
            // 
            // horizonpanel
            // 
            horizonpanel.BackColor = Color.Snow;
            horizonpanel.Location = new Point(0, 42);
            horizonpanel.Name = "horizonpanel";
            horizonpanel.Size = new Size(627, 1);
            horizonpanel.TabIndex = 18;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(34, 24);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(558, 23);
            progressBar1.TabIndex = 19;
            // 
            // label1
            // 
            label1.ForeColor = Color.White;
            label1.BackColor = Color.Transparent;
            label1.Location = new Point(34, 64);
            label1.Name = "label1";
            label1.Size = new Size(558, 25);
            label1.TabIndex = 20;
            label1.Text = "업데이트 진행 중..";
            label1.UseMnemonic = false;
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.Controls.Add(progressBar1, 1, 1);
            tableLayoutPanel2.Controls.Add(label1, 1, 2);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 42);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.Size = new Size(627, 129);
            tableLayoutPanel2.TabIndex = 21;
            // 
            // UpdateProgressForm
            // 
            BackColor = Color.FromArgb(26, 26, 26);
            ClientSize = new Size(627, 171);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(horizonpanel);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "UpdateProgressForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WSU Malware";
            TransparencyKey = Color.Transparent;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_max;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Button btn_min;
        private Panel horizonpanel;
        private ProgressBar progressBar1;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}