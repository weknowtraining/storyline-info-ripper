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
            this.GenNotesButton = new System.Windows.Forms.Button();
            this.progressBar_Micro = new System.Windows.Forms.ProgressBar();
            this.DebugLog = new System.Windows.Forms.RichTextBox();
            this.GenNarrButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.UseMarkupCheckbox = new System.Windows.Forms.CheckBox();
            this.ShowContextLinesCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
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
            this.progressBar_Macro.Location = new System.Drawing.Point(12, 288);
            this.progressBar_Macro.Name = "progressBar_Macro";
            this.progressBar_Macro.Size = new System.Drawing.Size(415, 23);
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
            // GenNotesButton
            // 
            this.GenNotesButton.Enabled = false;
            this.GenNotesButton.Location = new System.Drawing.Point(246, 49);
            this.GenNotesButton.Name = "GenNotesButton";
            this.GenNotesButton.Size = new System.Drawing.Size(181, 23);
            this.GenNotesButton.TabIndex = 3;
            this.GenNotesButton.Text = "Generate Notes Report";
            this.GenNotesButton.UseVisualStyleBackColor = true;
            this.GenNotesButton.Click += new System.EventHandler(this.GenNotesButton_Click);
            // 
            // progressBar_Micro
            // 
            this.progressBar_Micro.Location = new System.Drawing.Point(12, 269);
            this.progressBar_Micro.Name = "progressBar_Micro";
            this.progressBar_Micro.Size = new System.Drawing.Size(415, 13);
            this.progressBar_Micro.TabIndex = 4;
            // 
            // DebugLog
            // 
            this.DebugLog.Location = new System.Drawing.Point(12, 112);
            this.DebugLog.Name = "DebugLog";
            this.DebugLog.ReadOnly = true;
            this.DebugLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.DebugLog.Size = new System.Drawing.Size(415, 151);
            this.DebugLog.TabIndex = 5;
            this.DebugLog.Text = "";
            // 
            // GenNarrButton
            // 
            this.GenNarrButton.Enabled = false;
            this.GenNarrButton.Location = new System.Drawing.Point(246, 78);
            this.GenNarrButton.Name = "GenNarrButton";
            this.GenNarrButton.Size = new System.Drawing.Size(181, 23);
            this.GenNarrButton.TabIndex = 6;
            this.GenNarrButton.Text = "Generate Narration Report";
            this.GenNarrButton.UseVisualStyleBackColor = true;
            this.GenNarrButton.Click += new System.EventHandler(this.GenNarrButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UseMarkupCheckbox);
            this.groupBox1.Controls.Add(this.ShowContextLinesCheckbox);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 65);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // UseMarkupCheckbox
            // 
            this.UseMarkupCheckbox.AutoSize = true;
            this.UseMarkupCheckbox.Enabled = false;
            this.UseMarkupCheckbox.Location = new System.Drawing.Point(7, 43);
            this.UseMarkupCheckbox.Name = "UseMarkupCheckbox";
            this.UseMarkupCheckbox.Size = new System.Drawing.Size(84, 17);
            this.UseMarkupCheckbox.TabIndex = 1;
            this.UseMarkupCheckbox.Text = "Use Markup";
            this.UseMarkupCheckbox.UseVisualStyleBackColor = true;
            // 
            // ShowContextLinesCheckbox
            // 
            this.ShowContextLinesCheckbox.AutoSize = true;
            this.ShowContextLinesCheckbox.Checked = true;
            this.ShowContextLinesCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowContextLinesCheckbox.Location = new System.Drawing.Point(7, 20);
            this.ShowContextLinesCheckbox.Name = "ShowContextLinesCheckbox";
            this.ShowContextLinesCheckbox.Size = new System.Drawing.Size(120, 17);
            this.ShowContextLinesCheckbox.TabIndex = 0;
            this.ShowContextLinesCheckbox.Text = "Show Context Lines";
            this.ShowContextLinesCheckbox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 325);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GenNarrButton);
            this.Controls.Add(this.DebugLog);
            this.Controls.Add(this.progressBar_Micro);
            this.Controls.Add(this.GenNotesButton);
            this.Controls.Add(this.FilePathLabel);
            this.Controls.Add(this.progressBar_Macro);
            this.Controls.Add(this.OpenFileButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Story Ripper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.ProgressBar progressBar_Macro;
        private System.Windows.Forms.Label FilePathLabel;
        private System.Windows.Forms.Button GenNotesButton;
        private System.Windows.Forms.ProgressBar progressBar_Micro;
        private System.Windows.Forms.RichTextBox DebugLog;
        private System.Windows.Forms.Button GenNarrButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox UseMarkupCheckbox;
        private System.Windows.Forms.CheckBox ShowContextLinesCheckbox;
    }
}

