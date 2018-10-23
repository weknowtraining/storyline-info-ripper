using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
//using System.Windows.Forms;
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

        public SlideParser storyParser;

        /// <summary>
        /// int1 is current value, int2 is max value
        /// </summary>
        private Action<int, int> UpdateProgress;

        //private ProgressBar progressBar;
        private ZipFile loadedFile;

        public StoryReader (Action<int, int> OnProgressUpdated)
        {
            this.UpdateProgress = OnProgressUpdated;
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
            storyParser = new SlideParser(this, GetXmlTextAtPath("story/story.xml"), GetXmlTextAtPath("story/_rels/story.xml.rels"));
            storyParser.ParseData(UpdateProgress);
        }

        public void WriteNarrationReport()
        {
            WriteFile(storyParser.GetNarrationReport(), "-NarrationReport.txt");
        }

        private void WriteFile(string contents, string fileSuffix)
        {
            MainForm.AddToLog("");
            MainForm.AddToLog("Writing to file...");
            UpdateProgress.Invoke(1, 3);

            OutputPath = Path.GetDirectoryName(PathToFile) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(PathToFile) + fileSuffix;
            File.Create(OutputPath).Dispose();

            UpdateProgress.Invoke(2, 3);

            using (var writer = new StreamWriter(OutputPath))
            {
                writer.Write(contents);
            }

            loadedFile.Dispose();

            //Finish up
            UpdateProgress.Invoke(3, 3);

            OnGenerationComplete?.Invoke();

            MainForm.AddToLog("Done!");
        }

    }
}
