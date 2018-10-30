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
using System.Threading;

namespace StorylineRipper
{
    public partial class MainForm : Form
    {
        private bool isPathValid = false;
        private StoryReader reader;

        private Thread generatorThread;

        private static MainForm Instance;

        public MainForm()
        {
            InitializeComponent();
            Instance = this;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            reader = new StoryReader();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Storyline Story|*.story|All Files|*.*";

            isPathValid = (dialog.ShowDialog() == DialogResult.OK);

            reader.PathToFile = dialog.FileName;
            FilePathLabel.Text = Path.GetFileName(reader.PathToFile);
            GenNarrationButton.Enabled = isPathValid;

            AddToLog("File Selected");
        }

        private void GenNarrationButton_Click(object sender, EventArgs e)
        {
            FilePathLabel.Text = "Working...";
            UpdateMicroProgress(0, 1);
            UpdateMacroProgress(0, 5);
            if (reader.LoadFile())
            {
                reader.OnGenerationComplete += GenerationComplete;
                generatorThread = new Thread(
                    new ThreadStart(HandleNarrationGeneration));
                generatorThread.IsBackground = true;
                generatorThread.Start();
                AddToLog("Beginning workload...");
            }
            else
            {
                FilePathLabel.Text = "File couldn't be loaded!";
            }
        }

        private void HandleNarrationGeneration()
        {
            AddToLog("Reading story file...");
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

            FilePathLabel.Invoke((MethodInvoker)(() =>
            {
                FilePathLabel.Text = "Done!";
            }));

            reader = null;
            GenNarrationButton.Invoke((MethodInvoker)(() =>
            {
                GenNarrationButton.Enabled = false;
            }));
        }

        public static void UpdateMacroProgress(int val, int max)
        {
            Instance.progressBar_Macro.Invoke((MethodInvoker)(() => 
                {
                    Instance.progressBar_Macro.Maximum = max;
                    Instance.progressBar_Macro.Value = val;
                }));
        }

        public static void UpdateMicroProgress(int val, int max)
        {
            Instance.progressBar_Micro.Invoke((MethodInvoker)(() =>
            {
                Instance.progressBar_Micro.Maximum = max;
                Instance.progressBar_Micro.Value = val;
            }));
        }

        public static void AddToLog(string log)
        {
            Instance.DebugLog.Invoke((MethodInvoker)(() =>
            {
                Instance.DebugLog.AppendText(log + Environment.NewLine);

                //move the caret to the end of the text
                Instance.DebugLog.SelectionStart = Instance.DebugLog.TextLength;
                //scroll to the caret
                Instance.DebugLog.ScrollToCaret();
            }));
        }

        public static void AddErrorToLog(string log)
        {
            Instance.DebugLog.Invoke((MethodInvoker)(() =>
            {
                Instance.DebugLog.SelectionStart = Instance.DebugLog.TextLength;
                Instance.DebugLog.SelectionLength = 0;

                Instance.DebugLog.SelectionColor = Color.Red;
                Instance.DebugLog.AppendText($"{log}{Environment.NewLine}");
                Instance.DebugLog.SelectionColor = Instance.DebugLog.ForeColor;

                //move the caret to the end of the text
                Instance.DebugLog.SelectionStart = Instance.DebugLog.TextLength;
                //scroll to the caret
                Instance.DebugLog.ScrollToCaret();
            }));
        }
    }
}
