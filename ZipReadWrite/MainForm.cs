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

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StoryReader reader = new StoryReader(progressBar1);
            reader.SetFilePath();
            reader.LoadFile();
            reader.ReadFile();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            StoryReader reader = new StoryReader(progressBar1);
            if (reader.SetFilePath() && reader.LoadFile())
                reader.ReadFile();
        }
    }
}
