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
        this.components = new System.ComponentModel.Container();
        this.progressBar1 = new System.Windows.Forms.ProgressBar();
        this.timer1 = new System.Windows.Forms.Timer(this.components);
        this.button1 = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.richTextBox1 = new System.Windows.Forms.RichTextBox();
        this.SuspendLayout();
        // 
        // progressBar1 
        // 진행바
        // 
        this.progressBar1.Location = new System.Drawing.Point(10, 21);
        this.progressBar1.Name = "progressBar1";
        this.progressBar1.Size = new System.Drawing.Size(780, 23);
        this.progressBar1.TabIndex = 0;
        // 
        // timer1 
        // progressBar 테스트용 타이머
        // 
        this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        // 
        // button1 
        // progressBar테스트용 버튼튼
        // 
        this.button1.Location = new System.Drawing.Point(436, 425);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 1;
        this.button1.Text = "시작";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // label1 
        // 임시 라벨
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(25, 430);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(52, 15);
        this.label1.TabIndex = 2;
        this.label1.Text = "상태 : ";
        // 
        // label2 
        // 버전 표시
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(600, 430);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(52, 15);
        this.label2.TabIndex = 3;
        this.label2.Text = "버전 : 2025.03.18";
        //
        // Textbox1 
        // 읽은 파일 목록 출력
        //
        this.richTextBox1.Location = new System.Drawing.Point(10,44);
        this.richTextBox1.Name = "textbox1";
        this.richTextBox1.Size = new System.Drawing.Size(780, 370);
        this.richTextBox1.SelectionStart = richTextBox1.TextLength;
        this.richTextBox1.HideSelection = false;
        this.richTextBox1.ReadOnly = true;
        this.richTextBox1.TabIndex = 4;
        //
        // Form1
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.progressBar1);
        this.Controls.Add(this.richTextBox1);
        this.Text = "Form1";
        this.ResumeLayout(false);
        this.PerformLayout();
    }   

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
}