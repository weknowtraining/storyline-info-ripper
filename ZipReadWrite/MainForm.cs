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

using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml;

namespace ZipReadWrite
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
            using (StoryReader reader = new StoryReader(progressBar1))
            {
                reader.SetFilePath();
                reader.ReadFile();
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            using (StoryReader reader = new StoryReader(progressBar1))
            {
                reader.SetFilePath();

                SlideManifest manifest = new SlideManifest(reader.GetXmlTextAtPath("story/story.xml"), reader.GetXmlTextAtPath("story/_rels/story.xml.rels"));
            }
        }
    }
}
