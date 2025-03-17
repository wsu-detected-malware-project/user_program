namespace detected_malware
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
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
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_max = new System.Windows.Forms.Button();
            this.btn_min = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_title = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_close.FlatAppearance.BorderSize = 0;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_close.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_close.Location = new System.Drawing.Point(968, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(29, 34);
            this.btn_close.TabIndex = 3;
            this.btn_close.Text = "x";
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_close_Click);
            // 
            // btn_max
            // 
            this.btn_max.BackColor = System.Drawing.Color.Transparent;
            this.btn_max.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_max.FlatAppearance.BorderSize = 0;
            this.btn_max.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_max.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_max.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_max.Location = new System.Drawing.Point(933, 3);
            this.btn_max.Name = "btn_max";
            this.btn_max.Size = new System.Drawing.Size(29, 34);
            this.btn_max.TabIndex = 2;
            this.btn_max.Text = "ㅁ";
            this.btn_max.UseVisualStyleBackColor = false;
            this.btn_max.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_max_Click);
            // 
            // btn_min
            // 
            this.btn_min.BackColor = System.Drawing.Color.Transparent;
            this.btn_min.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_min.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_min.FlatAppearance.BorderSize = 0;
            this.btn_min.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_min.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_min.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_min.Location = new System.Drawing.Point(898, 3);
            this.btn_min.Name = "btn_min";
            this.btn_min.Size = new System.Drawing.Size(29, 34);
            this.btn_min.TabIndex = 1;
            this.btn_min.Text = "-";
            this.btn_min.UseVisualStyleBackColor = false;
            this.btn_min.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_min_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.GrayText;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Controls.Add(this.btn_min, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_max, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_close, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_title, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1000, 40);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_title.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_title.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_title.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_title.Location = new System.Drawing.Point(3, 16);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(889, 24);
            this.lbl_title.TabIndex = 4;
            this.lbl_title.Text = "WSU malware";
            this.lbl_title.Click += new System.EventHandler(this.lbl_title_Click);
            this.lbl_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_title_MouseDown);
            this.lbl_title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_title_MouseMove);
            this.lbl_title.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_title_MouseUp);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.LightGray;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 40);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1000, 3);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_max;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Button btn_min;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

