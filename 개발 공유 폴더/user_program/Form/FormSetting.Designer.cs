using System.Windows.Forms;
using user_program.FormAll;
using Windows.ApplicationModel.Contacts;

namespace user_program.FormAll
{
    partial class FormSetting
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
            labelRealTimeScan = new Label();
            comboBoxScanInterval = new ComboBox();
            toggleRealTimeScan = new ToggleSwitch();
            labelExclusion = new Label();
            listBoxExclusion = new ListBox();
            labelUpdate = new Label();
            buttonUpdate = new Button();
            labelLanguage = new Label();
            comboBoxLanguage = new ComboBox();
            label1 = new Label();
            horizonpanel = new Panel();
            tableLayoutPanel1.SuspendLayout();
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
            btn_close.Location = new Point(368, 4);
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
            btn_max.Location = new Point(333, 4);
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
            btn_min.Location = new Point(298, 4);
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
            tableLayoutPanel1.Controls.Add(lbl_title, 0, 0);
            tableLayoutPanel1.Controls.Add(btn_min, 1, 0);
            tableLayoutPanel1.Controls.Add(btn_max, 2, 0);
            tableLayoutPanel1.Controls.Add(btn_close, 3, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F));
            tableLayoutPanel1.Size = new Size(400, 42);
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
            lbl_title.Size = new Size(289, 31);
            lbl_title.TabIndex = 4;
            lbl_title.Text = "WSU MALWARE";
            lbl_title.MouseDown += lbl_title_MouseDown;
            lbl_title.MouseMove += lbl_title_MouseMove;
            lbl_title.MouseUp += lbl_title_MouseUp;
            // 
            // labelRealTimeScan
            // 
            labelRealTimeScan.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelRealTimeScan.ForeColor = Color.White;
            labelRealTimeScan.Location = new Point(30, 84);
            labelRealTimeScan.Name = "labelRealTimeScan";
            labelRealTimeScan.Size = new Size(175, 30);
            labelRealTimeScan.TabIndex = 1;
            labelRealTimeScan.Text = "실시간 검사 on/off";
            // 
            // comboBoxScanInterval
            // 
            comboBoxScanInterval.BackColor = Color.FromArgb(26, 26, 26);
            comboBoxScanInterval.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxScanInterval.ForeColor = Color.White;
            comboBoxScanInterval.Items.AddRange(new object[] { "6시간" });
            comboBoxScanInterval.Location = new Point(211, 130);
            comboBoxScanInterval.Name = "comboBoxScanInterval";
            comboBoxScanInterval.Size = new Size(139, 23);
            comboBoxScanInterval.TabIndex = 3;
            comboBoxScanInterval.SelectedIndexChanged += comboBoxScanInterval_SelectedIndexChanged;
            // 
            // toggleRealTimeScan
            // 
            toggleRealTimeScan.CircleColor = Color.White;
            toggleRealTimeScan.IsOn = false;
            toggleRealTimeScan.Location = new Point(256, 84);
            toggleRealTimeScan.Name = "toggleRealTimeScan";
            toggleRealTimeScan.OffColor = Color.Gray;
            toggleRealTimeScan.OnColor = Color.Green;
            toggleRealTimeScan.Size = new Size(50, 25);
            toggleRealTimeScan.TabIndex = 0;
            // 
            // labelExclusion
            // 
            labelExclusion.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelExclusion.ForeColor = Color.White;
            labelExclusion.Location = new Point(30, 180);
            labelExclusion.Name = "labelExclusion";
            labelExclusion.Size = new Size(150, 30);
            labelExclusion.TabIndex = 4;
            labelExclusion.Text = "검색 제외 항목";
            // 
            // listBoxExclusion
            // 
            listBoxExclusion.BackColor = Color.FromArgb(26, 26, 26);
            listBoxExclusion.ForeColor = Color.White;
            listBoxExclusion.ItemHeight = 15;
            listBoxExclusion.Location = new Point(30, 213);
            listBoxExclusion.Name = "listBoxExclusion";
            listBoxExclusion.Size = new Size(320, 94);
            listBoxExclusion.TabIndex = 5;
            // 
            // labelUpdate
            // 
            labelUpdate.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelUpdate.ForeColor = Color.White;
            labelUpdate.Location = new Point(30, 330);
            labelUpdate.Name = "labelUpdate";
            labelUpdate.Size = new Size(80, 30);
            labelUpdate.TabIndex = 6;
            labelUpdate.Text = "업데이트";
            // 
            // buttonUpdate
            // 
            buttonUpdate.BackColor = Color.FromArgb(26, 26, 26);
            buttonUpdate.FlatStyle = FlatStyle.Flat;
            buttonUpdate.ForeColor = Color.White;
            buttonUpdate.Location = new Point(170, 330);
            buttonUpdate.Name = "buttonUpdate";
            buttonUpdate.Size = new Size(176, 30);
            buttonUpdate.TabIndex = 7;
            buttonUpdate.Text = "업데이트 확인";
            buttonUpdate.UseVisualStyleBackColor = false;
            // 
            // labelLanguage
            // 
            labelLanguage.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelLanguage.ForeColor = Color.White;
            labelLanguage.Location = new Point(30, 381);
            labelLanguage.Name = "labelLanguage";
            labelLanguage.Size = new Size(80, 30);
            labelLanguage.TabIndex = 8;
            labelLanguage.Text = "언어";
            // 
            // comboBoxLanguage
            // 
            comboBoxLanguage.BackColor = Color.FromArgb(26, 26, 26);
            comboBoxLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLanguage.ForeColor = Color.White;
            comboBoxLanguage.Items.AddRange(new object[] { "한국어", "English" });
            comboBoxLanguage.Location = new Point(170, 381);
            comboBoxLanguage.Name = "comboBoxLanguage";
            comboBoxLanguage.Size = new Size(176, 23);
            comboBoxLanguage.TabIndex = 9;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(30, 128);
            label1.Name = "label1";
            label1.Size = new Size(175, 30);
            label1.TabIndex = 10;
            label1.Text = "실시간 검사 주기 설정";
            // 
            // horizonpanel
            // 
            horizonpanel.BackColor = Color.Snow;
            horizonpanel.Location = new Point(3, 41);
            horizonpanel.Name = "horizonpanel";
            horizonpanel.Size = new Size(400, 1);
            horizonpanel.TabIndex = 17;
            // 
            // FormSetting
            // 
            BackColor = Color.FromArgb(26, 26, 26);
            ClientSize = new Size(400, 420);
            Controls.Add(horizonpanel);
            Controls.Add(toggleRealTimeScan);
            Controls.Add(label1);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(labelRealTimeScan);
            Controls.Add(comboBoxScanInterval);
            Controls.Add(labelExclusion);
            Controls.Add(listBoxExclusion);
            Controls.Add(labelUpdate);
            Controls.Add(buttonUpdate);
            Controls.Add(labelLanguage);
            Controls.Add(comboBoxLanguage);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormSetting";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WSU Malware";
            Load += FormSetting_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        private Button btn_close;
        private Button btn_max;
        private Button btn_min;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lbl_title;
        private Label labelRealTimeScan;
        private ToggleSwitch toggleRealTimeScan;
        private ComboBox comboBoxScanInterval;
        private Label labelExclusion;
        private ListBox listBoxExclusion;
        private Label labelUpdate;
        private Button buttonUpdate;
        private Label labelLanguage;
        private ComboBox comboBoxLanguage;
        private Label label1;

        #endregion

        private Panel horizonpanel;
    }
}