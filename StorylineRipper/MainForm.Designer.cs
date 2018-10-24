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
            this.progressBar_Macro = new System.Windows.Forms.ProgressBar();
            this.FilePathLabel = new System.Windows.Forms.Label();
            this.GenNarrationButton = new System.Windows.Forms.Button();
            this.progressBar_Micro = new System.Windows.Forms.ProgressBar();
            this.DebugLog = new System.Windows.Forms.RichTextBox();
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
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // progressBar_Macro
            // 
            this.progressBar_Macro.Location = new System.Drawing.Point(93, 46);
            this.progressBar_Macro.Name = "progressBar_Macro";
            this.progressBar_Macro.Size = new System.Drawing.Size(180, 23);
            this.progressBar_Macro.TabIndex = 1;
            // 
            // FilePathLabel
            // 
            this.FilePathLabel.AutoSize = true;
            this.FilePathLabel.Location = new System.Drawing.Point(93, 17);
            this.FilePathLabel.Name = "FilePathLabel";
            this.FilePathLabel.Size = new System.Drawing.Size(77, 13);
            this.FilePathLabel.TabIndex = 2;
            this.FilePathLabel.Text = "Choose a file...\r\n";
            // 
            // GenNarrationButton
            // 
            this.GenNarrationButton.Enabled = false;
            this.GenNarrationButton.Location = new System.Drawing.Point(12, 46);
            this.GenNarrationButton.Name = "GenNarrationButton";
            this.GenNarrationButton.Size = new System.Drawing.Size(75, 23);
            this.GenNarrationButton.TabIndex = 3;
            this.GenNarrationButton.Text = "Generate!";
            this.GenNarrationButton.UseVisualStyleBackColor = true;
            this.GenNarrationButton.Click += new System.EventHandler(this.GenNarrationButton_Click);
            // 
            // progressBar_Micro
            // 
            this.progressBar_Micro.Location = new System.Drawing.Point(12, 294);
            this.progressBar_Micro.Name = "progressBar_Micro";
            this.progressBar_Micro.Size = new System.Drawing.Size(261, 13);
            this.progressBar_Micro.TabIndex = 4;
            // 
            // DebugLog
            // 
            this.DebugLog.Location = new System.Drawing.Point(12, 75);
            this.DebugLog.Multiline = true;
            this.DebugLog.Name = "DebugLog";
            this.DebugLog.ReadOnly = true;
            this.DebugLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.DebugLog.Size = new System.Drawing.Size(261, 213);
            this.DebugLog.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 317);
            this.Controls.Add(this.DebugLog);
            this.Controls.Add(this.progressBar_Micro);
            this.Controls.Add(this.GenNarrationButton);
            this.Controls.Add(this.FilePathLabel);
            this.Controls.Add(this.progressBar_Macro);
            this.Controls.Add(this.OpenFileButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Story Ripper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.ProgressBar progressBar_Macro;
        private System.Windows.Forms.Label FilePathLabel;
        private System.Windows.Forms.Button GenNarrationButton;
        private System.Windows.Forms.ProgressBar progressBar_Micro;
        private System.Windows.Forms.RichTextBox DebugLog;
    }
}

