using user_program.FormAll;

namespace user_program.FormAll {
    partial class Form1 {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btn_close = new Button();
            btn_max = new Button();
            btn_min = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            pictureBox2 = new PictureBox();
            lbl_title = new Label();
            button = new Button();
            button2 = new Button();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            verticalpannel = new Panel();
            pictureBox1 = new PictureBox();
            richText = new RichTextBox();
            horizonpanel = new Panel();
            noDragRichTextBox1 = new NoDragRichTextBox();
            label2 = new Label();
            label5 = new Label();
            label6 = new Label();
            noDragRichTextBox2 = new NoDragRichTextBox();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            btn_close.Location = new Point(765, 5);
            btn_close.Margin = new Padding(4, 5, 4, 5);
            btn_close.Name = "btn_close";
            btn_close.Size = new Size(37, 46);
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
            btn_max.Location = new Point(720, 5);
            btn_max.Margin = new Padding(4, 5, 4, 5);
            btn_max.Name = "btn_max";
            btn_max.Size = new Size(37, 46);
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
            btn_min.Location = new Point(675, 5);
            btn_min.Margin = new Padding(4, 5, 4, 5);
            btn_min.Name = "btn_min";
            btn_min.Size = new Size(37, 46);
            btn_min.TabIndex = 1;
            btn_min.Text = "-";
            btn_min.UseVisualStyleBackColor = false;
            btn_min.MouseClick += btn_min_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(24, 24, 24);
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 51F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.Controls.Add(pictureBox2, 1, 0);
            tableLayoutPanel1.Controls.Add(btn_min, 2, 0);
            tableLayoutPanel1.Controls.Add(btn_max, 3, 0);
            tableLayoutPanel1.Controls.Add(btn_close, 4, 0);
            tableLayoutPanel1.Controls.Add(lbl_title, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(806, 56);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.Location = new Point(624, 4);
            pictureBox2.Margin = new Padding(4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(43, 48);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 17;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // lbl_title
            // 
            lbl_title.AutoSize = true;
            lbl_title.BackColor = Color.Transparent;
            lbl_title.Dock = DockStyle.Bottom;
            lbl_title.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbl_title.ForeColor = SystemColors.Control;
            lbl_title.Location = new Point(4, 17);
            lbl_title.Margin = new Padding(4, 0, 4, 0);
            lbl_title.Name = "lbl_title";
            lbl_title.Size = new Size(612, 39);
            lbl_title.TabIndex = 4;
            lbl_title.Text = "WSU MALWARE";
            lbl_title.MouseDown += lbl_title_MouseDown;
            lbl_title.MouseMove += lbl_title_MouseMove;
            lbl_title.MouseUp += lbl_title_MouseUp;
            // 
            // button
            // 
            button.BackColor = Color.FromArgb(24, 24, 24);
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Microsoft Sans Serif", 14.2499981F, FontStyle.Regular, GraphicsUnit.Point, 129);
            button.ForeColor = Color.Snow;
            button.Location = new Point(67, 372);
            button.Margin = new Padding(4);
            button.Name = "button";
            button.Size = new Size(260, 115);
            button.TabIndex = 2;
            button.Text = "빠른 검사";
            button.UseVisualStyleBackColor = false;
            button.Click += Button_Click1;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(24, 24, 24);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Microsoft Sans Serif", 14.2499981F, FontStyle.Regular, GraphicsUnit.Point, 129);
            button2.ForeColor = Color.Snow;
            button2.Location = new Point(67, 520);
            button2.Margin = new Padding(4);
            button2.Name = "button2";
            button2.Size = new Size(260, 117);
            button2.TabIndex = 3;
            button2.Text = "정밀 검사";
            button2.UseVisualStyleBackColor = false;
            button2.Click += Button_Click2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label1.ForeColor = Color.Snow;
            label1.Location = new Point(675, 628);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(110, 25);
            label1.TabIndex = 7;
            label1.Text = "2025.03.16";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Microsoft Sans Serif", 15.9999981F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label3.ForeColor = Color.Snow;
            label3.Location = new Point(613, 511);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(99, 31);
            label3.TabIndex = 10;
            label3.Text = ": 조금 전";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Microsoft Sans Serif", 15.9999981F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label4.ForeColor = Color.Snow;
            label4.Location = new Point(613, 561);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(99, 31);
            label4.TabIndex = 11;
            label4.Text = ": 조금 전";
            // 
            // verticalpannel
            // 
            verticalpannel.BackColor = Color.Gray;
            verticalpannel.Location = new Point(395, 365);
            verticalpannel.Margin = new Padding(4);
            verticalpannel.Name = "verticalpannel";
            verticalpannel.Size = new Size(1, 293);
            verticalpannel.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(239, 69);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(315, 295);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // richText
            // 
            richText.Location = new Point(0, 0);
            richText.Name = "richText";
            richText.Size = new Size(100, 96);
            richText.TabIndex = 0;
            richText.Text = "";
            // 
            // horizonpanel
            // 
            horizonpanel.BackColor = Color.Snow;
            horizonpanel.Location = new Point(0, 60);
            horizonpanel.Margin = new Padding(4);
            horizonpanel.Name = "horizonpanel";
            horizonpanel.Size = new Size(804, 1);
            horizonpanel.TabIndex = 16;
            // 
            // noDragRichTextBox1
            // 
            noDragRichTextBox1.BackColor = Color.FromArgb(26, 26, 26);
            noDragRichTextBox1.BorderStyle = BorderStyle.None;
            noDragRichTextBox1.Font = new Font("Microsoft Sans Serif", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            noDragRichTextBox1.ForeColor = Color.Snow;
            noDragRichTextBox1.Location = new Point(437, 392);
            noDragRichTextBox1.Margin = new Padding(4);
            noDragRichTextBox1.Name = "noDragRichTextBox1";
            noDragRichTextBox1.ReadOnly = true;
            noDragRichTextBox1.ScrollBars = RichTextBoxScrollBars.None;
            noDragRichTextBox1.Size = new Size(206, 59);
            noDragRichTextBox1.TabIndex = 17;
            noDragRichTextBox1.Text = "실시간 검사";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Microsoft Sans Serif", 15.9999981F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label2.ForeColor = Color.Snow;
            label2.Location = new Point(424, 511);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(147, 31);
            label2.TabIndex = 18;
            label2.Text = "최근 업데이트";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Microsoft Sans Serif", 15.9999981F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label5.ForeColor = Color.Snow;
            label5.Location = new Point(424, 561);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(126, 31);
            label5.TabIndex = 19;
            label5.Text = "마지막 검사";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label6.ForeColor = Color.Snow;
            label6.Location = new Point(566, 628);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(50, 25);
            label6.TabIndex = 20;
            label6.Text = "버전";
            // 
            // noDragRichTextBox2
            // 
            noDragRichTextBox2.BackColor = Color.FromArgb(26, 26, 26);
            noDragRichTextBox2.BorderStyle = BorderStyle.None;
            noDragRichTextBox2.Font = new Font("Microsoft Sans Serif", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            noDragRichTextBox2.ForeColor = Color.Snow;
            noDragRichTextBox2.Location = new Point(661, 392);
            noDragRichTextBox2.Margin = new Padding(4);
            noDragRichTextBox2.Name = "noDragRichTextBox2";
            noDragRichTextBox2.ReadOnly = true;
            noDragRichTextBox2.Size = new Size(86, 59);
            noDragRichTextBox2.TabIndex = 21;
            noDragRichTextBox2.Text = "On";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 26, 26);
            ClientSize = new Size(806, 667);
            Controls.Add(noDragRichTextBox2);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label2);
            Controls.Add(noDragRichTextBox1);
            Controls.Add(horizonpanel);
            Controls.Add(pictureBox1);
            Controls.Add(verticalpannel);
            Controls.Add(label4);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(label3);
            Controls.Add(button);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WSU Malware";
            TransparencyKey = Color.Transparent;
            Load += Form1_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_max;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Button btn_min;
        private Button button;
        private Button button2;
        private Label label1;
        private Label label3;
        private Label label4;
        private Panel verticalpannel;
        private PictureBox pictureBox1;
        private Panel horizonpanel;
        private PictureBox pictureBox2;
        private Label label2;
        private Label label5;
        private Label label6;
        private RichTextBox richText;
        static NoDragRichTextBox noDragRichTextBox1;
        static NoDragRichTextBox noDragRichTextBox2;
        //private Panel verticalDivider;
    }
}
