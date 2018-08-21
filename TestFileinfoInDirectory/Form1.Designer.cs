namespace TestFileinfoInDirectory
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.SourceDirLogListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.sourceDirBrowsingButton = new System.Windows.Forms.Button();
            this.DestinationDirBrowsingButton = new System.Windows.Forms.Button();
            this.RealTimeCopyButton = new System.Windows.Forms.Button();
            this.CopyStopButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DestinationDirLogListBox = new System.Windows.Forms.ListBox();
            this.sourceDirLogDelete = new System.Windows.Forms.Button();
            this.DestinationDirLogDelete = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.AutoScanBtn = new System.Windows.Forms.Button();
            this.domainUpDown1 = new System.Windows.Forms.DomainUpDown();
            this.AutoStopBtn = new System.Windows.Forms.Button();
            this.CopyBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SourceDirLogListBox
            // 
            this.SourceDirLogListBox.FormattingEnabled = true;
            this.SourceDirLogListBox.HorizontalScrollbar = true;
            this.SourceDirLogListBox.ItemHeight = 12;
            this.SourceDirLogListBox.Location = new System.Drawing.Point(3, 118);
            this.SourceDirLogListBox.Name = "SourceDirLogListBox";
            this.SourceDirLogListBox.Size = new System.Drawing.Size(808, 160);
            this.SourceDirLogListBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "공유 폴더 경로";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "로컬 저장소 경로";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(115, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(423, 21);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(115, 53);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(423, 21);
            this.textBox2.TabIndex = 4;
            // 
            // sourceDirBrowsingButton
            // 
            this.sourceDirBrowsingButton.Location = new System.Drawing.Point(544, 17);
            this.sourceDirBrowsingButton.Name = "sourceDirBrowsingButton";
            this.sourceDirBrowsingButton.Size = new System.Drawing.Size(37, 23);
            this.sourceDirBrowsingButton.TabIndex = 5;
            this.sourceDirBrowsingButton.Text = "...";
            this.sourceDirBrowsingButton.UseVisualStyleBackColor = true;
            this.sourceDirBrowsingButton.Click += new System.EventHandler(this.SourceDirBrowsingBtn_Click);
            // 
            // DestinationDirBrowsingButton
            // 
            this.DestinationDirBrowsingButton.Location = new System.Drawing.Point(544, 51);
            this.DestinationDirBrowsingButton.Name = "DestinationDirBrowsingButton";
            this.DestinationDirBrowsingButton.Size = new System.Drawing.Size(37, 23);
            this.DestinationDirBrowsingButton.TabIndex = 6;
            this.DestinationDirBrowsingButton.Text = "...";
            this.DestinationDirBrowsingButton.UseVisualStyleBackColor = true;
            this.DestinationDirBrowsingButton.Click += new System.EventHandler(this.DestinationDirBrowsingBtn_Click);
            // 
            // RealTimeCopyButton
            // 
            this.RealTimeCopyButton.Location = new System.Drawing.Point(587, 51);
            this.RealTimeCopyButton.Name = "RealTimeCopyButton";
            this.RealTimeCopyButton.Size = new System.Drawing.Size(138, 23);
            this.RealTimeCopyButton.TabIndex = 7;
            this.RealTimeCopyButton.Text = "Real Time Copy";
            this.RealTimeCopyButton.UseVisualStyleBackColor = true;
            this.RealTimeCopyButton.Click += new System.EventHandler(this.CopyBtn_Click);
            // 
            // CopyStopButton
            // 
            this.CopyStopButton.Location = new System.Drawing.Point(731, 51);
            this.CopyStopButton.Name = "CopyStopButton";
            this.CopyStopButton.Size = new System.Drawing.Size(71, 23);
            this.CopyStopButton.TabIndex = 8;
            this.CopyStopButton.Text = "중지";
            this.CopyStopButton.UseVisualStyleBackColor = true;
            this.CopyStopButton.Click += new System.EventHandler(this.StopButton);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(267, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "sourceDirectory Log Infomation(공유 디렉토리)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 297);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(277, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "destinationDirectory Log Infomation(로컬 저장소)";
            // 
            // DestinationDirLogListBox
            // 
            this.DestinationDirLogListBox.FormattingEnabled = true;
            this.DestinationDirLogListBox.HorizontalScrollbar = true;
            this.DestinationDirLogListBox.ItemHeight = 12;
            this.DestinationDirLogListBox.Location = new System.Drawing.Point(3, 322);
            this.DestinationDirLogListBox.Name = "DestinationDirLogListBox";
            this.DestinationDirLogListBox.Size = new System.Drawing.Size(808, 172);
            this.DestinationDirLogListBox.TabIndex = 11;
            // 
            // sourceDirLogDelete
            // 
            this.sourceDirLogDelete.Location = new System.Drawing.Point(284, 87);
            this.sourceDirLogDelete.Name = "sourceDirLogDelete";
            this.sourceDirLogDelete.Size = new System.Drawing.Size(104, 25);
            this.sourceDirLogDelete.TabIndex = 12;
            this.sourceDirLogDelete.Text = "log delete";
            this.sourceDirLogDelete.UseVisualStyleBackColor = true;
            this.sourceDirLogDelete.Click += new System.EventHandler(this.SourceDirLogDeletebutton_Click);
            // 
            // DestinationDirLogDelete
            // 
            this.DestinationDirLogDelete.Location = new System.Drawing.Point(284, 291);
            this.DestinationDirLogDelete.Name = "DestinationDirLogDelete";
            this.DestinationDirLogDelete.Size = new System.Drawing.Size(104, 25);
            this.DestinationDirLogDelete.TabIndex = 13;
            this.DestinationDirLogDelete.Text = "log delete";
            this.DestinationDirLogDelete.UseVisualStyleBackColor = true;
            this.DestinationDirLogDelete.Click += new System.EventHandler(this.DestinationDirLogDeletebutton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(445, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "Set Sync Time:";
            // 
            // AutoScanBtn
            // 
            this.AutoScanBtn.Location = new System.Drawing.Point(644, 87);
            this.AutoScanBtn.Name = "AutoScanBtn";
            this.AutoScanBtn.Size = new System.Drawing.Size(81, 23);
            this.AutoScanBtn.TabIndex = 16;
            this.AutoScanBtn.Text = "Auto Scan";
            this.AutoScanBtn.UseVisualStyleBackColor = true;
            this.AutoScanBtn.Click += new System.EventHandler(this.AutoBtn_Click);
            // 
            // domainUpDown1
            // 
            this.domainUpDown1.Location = new System.Drawing.Point(544, 88);
            this.domainUpDown1.Name = "domainUpDown1";
            this.domainUpDown1.Size = new System.Drawing.Size(94, 21);
            this.domainUpDown1.TabIndex = 17;
            this.domainUpDown1.Text = "domainUpDown1";
            this.domainUpDown1.SelectedItemChanged += new System.EventHandler(this.domainUpDown1_SelectedItemChanged);
            // 
            // AutoStopBtn
            // 
            this.AutoStopBtn.Location = new System.Drawing.Point(731, 87);
            this.AutoStopBtn.Name = "AutoStopBtn";
            this.AutoStopBtn.Size = new System.Drawing.Size(71, 23);
            this.AutoStopBtn.TabIndex = 18;
            this.AutoStopBtn.Text = "Scan Stop";
            this.AutoStopBtn.UseVisualStyleBackColor = true;
            this.AutoStopBtn.Click += new System.EventHandler(this.AutoStopBtn_Click);
            // 
            // CopyBtn
            // 
            this.CopyBtn.Location = new System.Drawing.Point(587, 17);
            this.CopyBtn.Name = "CopyBtn";
            this.CopyBtn.Size = new System.Drawing.Size(215, 23);
            this.CopyBtn.TabIndex = 19;
            this.CopyBtn.Text = "Copy";
            this.CopyBtn.UseVisualStyleBackColor = true;
            this.CopyBtn.Click += new System.EventHandler(this.justOneCopy);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 500);
            this.Controls.Add(this.CopyBtn);
            this.Controls.Add(this.AutoStopBtn);
            this.Controls.Add(this.domainUpDown1);
            this.Controls.Add(this.AutoScanBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DestinationDirLogDelete);
            this.Controls.Add(this.sourceDirLogDelete);
            this.Controls.Add(this.DestinationDirLogListBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CopyStopButton);
            this.Controls.Add(this.RealTimeCopyButton);
            this.Controls.Add(this.DestinationDirBrowsingButton);
            this.Controls.Add(this.sourceDirBrowsingButton);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SourceDirLogListBox);
            this.Name = "Form1";
            this.Text = "DirectoryCopy";
            this.Load += new System.EventHandler(this.Form1_Load2);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox SourceDirLogListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button sourceDirBrowsingButton;
        private System.Windows.Forms.Button DestinationDirBrowsingButton;
        private System.Windows.Forms.Button RealTimeCopyButton;
        private System.Windows.Forms.Button CopyStopButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox DestinationDirLogListBox;
        private System.Windows.Forms.Button sourceDirLogDelete;
        private System.Windows.Forms.Button DestinationDirLogDelete;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button AutoScanBtn;
        private System.Windows.Forms.DomainUpDown domainUpDown1;
        private System.Windows.Forms.Button AutoStopBtn;
        private System.Windows.Forms.Button CopyBtn;
    }
}

