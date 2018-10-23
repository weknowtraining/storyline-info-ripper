using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using StorylineRipper.Core;

namespace StorylineRipper
{
    public partial class MainForm : Form
    {
        private bool isPathValid = false;
        private StoryReader reader;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            reader = new StoryReader(progressBar1);
            reader.OnGenerationComplete += GenerationComplete;

            isPathValid = reader.SetFilePath();

            FilePathLabel.Text = Path.GetFileName(reader.PathToFile);
            GenNarrationButton.Enabled = isPathValid;
        }

        private void GenNarrationButton_Click(object sender, EventArgs e)
        {
            if (reader.LoadFile())
                reader.ReadFile();

            reader.WriteNarrationReport();
        }

        private void GenerationComplete()
        {
                        // Open a folder view of the output
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = Path.GetDirectoryName(reader.OutputPath),
                UseShellExecute = true,
                Verb = "open"
            });

            FilePathLabel.Text = "Done!";
        }
    }
}
