namespace StorylineRipper
{
    partial class MainForm
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
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.TestBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Location = new System.Drawing.Point(12, 12);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(75, 23);
            this.OpenFileButton.TabIndex = 0;
            this.OpenFileButton.Text = "Open File";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(93, 12);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(178, 23);
            this.progressBar1.TabIndex = 1;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // TestBtn
            // 
            this.TestBtn.Location = new System.Drawing.Point(12, 41);
            this.TestBtn.Name = "TestBtn";
            this.TestBtn.Size = new System.Drawing.Size(75, 23);
            this.TestBtn.TabIndex = 2;
            this.TestBtn.Text = "Test";
            this.TestBtn.UseVisualStyleBackColor = true;
            this.TestBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 81);
            this.Controls.Add(this.TestBtn);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.OpenFileButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button TestBtn;
    }
}

