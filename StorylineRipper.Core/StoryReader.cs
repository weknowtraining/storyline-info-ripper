using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

using StorylineRipper.Core;
using StorylineRipper.Common.Extensions;

using Ionic.Zip;

namespace StorylineRipper.Core
{
    public class StoryReader
    {
        public string PathToFile { get; private set; }

        public SlideManifest manifest;

        private ProgressBar progressBar;
        private ZipFile loadedFile;

        public StoryReader (ProgressBar progressBar)
        {
            this.progressBar = progressBar;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // Needed to read zip file
        }

        /// <summary>
        /// Opens a dialog box to allow the user to select the file to open.
        /// </summary>
        /// <returns>True if valid file found, otherwise false</returns>
        public bool SetFilePath()
        {
            PathToFile = null;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Storyline Story|*.story|All Files|*.*";

            if (dialog.ShowDialog() != DialogResult.OK)
                return false;

            PathToFile = dialog.FileName;
            return true;
        }

        /// <summary>
        /// Loads the file set with SetFilePath
        /// </summary>
        /// <returns>True if path is valid, otherwise false</returns>
        public bool LoadFile()
        {
            if (PathToFile != null && PathToFile.Length > 0)
            {
                loadedFile = ZipFile.Read(PathToFile);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Returns the xml content of the file specified with path. Path is relative to story file root.
        /// </summary>
        /// <param name="path">Relative path to file from story root</param>
        /// <returns>Xml content of file as string</returns>
        public string GetXmlTextAtPath(string path)
        {
            string text;

            ZipEntry storyXml = loadedFile.Entries.First(x => x.FileName == path);

            // Read all the text in the file
            using (StreamReader reader = new StreamReader(storyXml.OpenReader(), Encoding.UTF8))
            {
                text = reader.ReadToEnd();
            }

            return text;
        }

        public void ReadFile()
        {
            manifest = new SlideManifest(this, GetXmlTextAtPath("story/story.xml"), GetXmlTextAtPath("story/_rels/story.xml.rels"));

            manifest.ParseData(progressBar);
        }

        public void WriteNarrationReport()
        {
            WriteFile(manifest.GetNarrationReport());
        }

        private void WriteFile(string contents)
        {
            progressBar.Value = progressBar.Minimum;
            progressBar.Maximum = 3;
            progressBar.Step = 1;
            progressBar.PerformStep();

            string newPath = Path.GetDirectoryName(PathToFile) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(PathToFile) + "-NarrationReport.txt";
            File.Create(newPath).Close();

            progressBar.PerformStep();

            using (var writer = new StreamWriter(newPath))
            {
                writer.Write(contents);
            }

            loadedFile.Dispose();

            //Finish up
            progressBar.Value = progressBar.Maximum;
        }

    }
}
