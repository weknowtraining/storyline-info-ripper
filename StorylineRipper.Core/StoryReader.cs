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
            SlideManifest manifest = new SlideManifest(this, GetXmlTextAtPath("story/story.xml"), GetXmlTextAtPath("story/_rels/story.xml.rels"));

        }

        //public void ReadFile()
        //{
        //    string noteText = ""; // Full note text

        //    // Progress bar stuff
        //    progressBar.Maximum = loadedFile.Count;
        //    progressBar.Value = 0;
        //    progressBar.Step = 1;

        //    // Get each file in the zip
        //    foreach (ZipEntry entry in loadedFile)
        //    {
        //        if (Path.GetExtension(entry.FileName) != ".xml") //If it's not xml, skip to next file
        //            continue;

        //        string text;
        //        // Read all the text in the file
        //        using (StreamReader reader = new StreamReader(entry.OpenReader(), Encoding.UTF8))
        //        {
        //            text = reader.ReadToEnd();
        //        }

        //        // Load text as xml
        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.LoadXml(text);

        //        XmlNodeList noteList = xmlDoc.GetElementsByTagName("note");
        //        foreach (XmlNode node in noteList)
        //        {
        //            if (node.InnerText == "")
        //                continue;

        //            // The inner xml is all... I don't even know wtf they're doing here
        //            // but this makes it work.
        //            // It's some sort of xml markup I think.
        //            string notesXML = node.InnerText;
        //            notesXML.Replace("&lt;", "<");
        //            notesXML.Replace("&gt;", ">");

        //            XmlDocument innerDoc = new XmlDocument();
        //            innerDoc.LoadXml(notesXML);

        //            XmlNodeList spanList = innerDoc.SelectNodes("//Span");

        //            StringBuilder stringBuilder = new StringBuilder();
        //            stringBuilder.AppendLine(entry.FileName);

        //            for (int i = 0; i < spanList.Count; i++)
        //            {
        //                stringBuilder.AppendLine(spanList[i].Attributes["Text"].Value);
        //            }

        //            stringBuilder.AppendLine("-");
        //            noteText += stringBuilder.ToString();

        //            progressBar.PerformStep();
        //        }
        //    }

        //    // Finish up
        //    progressBar.Value = progressBar.Maximum;

        //    WriteFile(noteText);
        //}

        public void WriteFile(string contents)
        {
            string newPath = Path.GetDirectoryName(PathToFile) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(PathToFile) + "-output.txt";
            File.Create(newPath).Close();

            using (var writer = new StreamWriter(newPath))
            {
                writer.Write(contents);
            }
        }

    }
}
