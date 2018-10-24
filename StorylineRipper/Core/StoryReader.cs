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

        //private ProgressBar progressBar;
        private ZipFile loadedFile;

        public StoryReader ()
        {
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
                try
                {
                    loadedFile = ZipFile.Read(PathToFile);

                    if (!loadedFile.ContainsEntry("story/story.xml"))
                        throw new ZipException("File is not a storyline file");
                }
                catch(ZipException e)
                {
                    MainForm.AddToLog($"\n({e.Message})\n");
                    MainForm.AddErrorToLog("Selected file is not a valid storyline file!");
                    return false;
                }
                
                MainForm.UpdateMicroProgress(1, 1);
                MainForm.UpdateMacroProgress(1, 5);
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
            MainForm.UpdateMacroProgress(2, 5);

            storyParser.ParseData();
            MainForm.UpdateMacroProgress(3, 5);
        }

        public void WriteNarrationReport()
        {
            OutputPath = Path.GetDirectoryName(PathToFile) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(PathToFile) + "-NarrationReport.docx";
            DocXWriter writer = new DocXWriter(OutputPath);

            writer.GenerateNarrationReport(storyParser.story);

            //string report = storyParser.GetNarrationReport();
            //MainForm.UpdateMacroProgress(4, 5);

            //WriteFile(report, "-NarrationReport.txt");
            MainForm.UpdateMacroProgress(5, 5);
            OnGenerationComplete?.Invoke();
        }

        private void WriteFile(string contents)
        {
            MainForm.AddToLog("");
            MainForm.AddToLog("Writing to file...");
            MainForm.UpdateMicroProgress(1, 3);

            File.Create(OutputPath).Dispose();

            MainForm.UpdateMicroProgress(2, 3);

            using (var writer = new StreamWriter(OutputPath))
            {
                writer.Write(contents);
            }

            loadedFile.Dispose();

            MainForm.AddToLog("Done!");
            MainForm.UpdateMicroProgress(3, 3);
        }

    }
}
