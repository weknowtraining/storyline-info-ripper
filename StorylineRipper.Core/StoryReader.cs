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
        public event Action OnGenerationComplete;

        public string PathToFile { get; set; }
        public string OutputPath { get; private set; }

        public SlideParser manifest;

        private ProgressBar progressBar;
        private ZipFile loadedFile;

        public StoryReader (ProgressBar progressBar)
        {
            this.progressBar = progressBar;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // Needed to read zip file
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
            manifest = new SlideParser(this, GetXmlTextAtPath("story/story.xml"), GetXmlTextAtPath("story/_rels/story.xml.rels"));
            manifest.ParseData(progressBar);
        }

        public void WriteNarrationReport()
        {
            WriteFile(manifest.GetNarrationReport(), "-NarrationReport.txt");
        }

        private void WriteFile(string contents, string fileSuffix)
        {
            progressBar.Value = progressBar.Minimum;
            progressBar.Maximum = 3;
            progressBar.Step = 1;
            progressBar.PerformStep();

            OutputPath = Path.GetDirectoryName(PathToFile) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(PathToFile) + fileSuffix;
            File.Create(OutputPath).Dispose();

            progressBar.PerformStep();

            using (var writer = new StreamWriter(OutputPath))
            {
                writer.Write(contents);
            }

            loadedFile.Dispose();

            //Finish up
            progressBar.Value = progressBar.Maximum;

            if (OnGenerationComplete != null)
                OnGenerationComplete.Invoke();
        }

    }
}
