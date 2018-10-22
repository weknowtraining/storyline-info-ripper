using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml;

using Ionic.Zip;

namespace ZipReadWrite
{
    public class StoryReader : IDisposable
    {
        public string PathToFile { get; private set; }

        private ProgressBar progressBar;

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

        public string GetXmlTextAtPath(string path)
        {
            string text;
            var entries = ZipFile.Read(PathToFile);

            ZipEntry storyXml = entries.Entries.First(x => x.FileName == path);

            // Read all the text in the file
            using (StreamReader reader = new StreamReader(storyXml.OpenReader(), Encoding.UTF8))
            {
                text = reader.ReadToEnd();
            }

            return text;
        }

        public void ReadFile()
        {
            string noteText = ""; // Full note text

            var entries = ZipFile.Read(PathToFile);

            // Progress bar stuff
            progressBar.Maximum = entries.Count;
            progressBar.Value = 0;
            progressBar.Step = 1;

            // Get each file in the zip
            foreach (ZipEntry entry in entries)
            {
                if (Path.GetExtension(entry.FileName) != ".xml") //If it's not xml, skip to next file
                    continue;

                string text;
                // Read all the text in the file
                using (StreamReader reader = new StreamReader(entry.OpenReader(), Encoding.UTF8))
                {
                    text = reader.ReadToEnd();
                }

                // Load text as xml
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(text);

                XmlNodeList noteList = xmlDoc.GetElementsByTagName("note");
                foreach (XmlNode node in noteList)
                {
                    if (node.InnerText == "")
                        continue;

                    // The inner xml is all... I don't even know wtf they're doing here
                    // but this makes it work.
                    // It's some sort of xml markup I think.
                    string notesXML = node.InnerText;
                    notesXML.Replace("&lt;", "<");
                    notesXML.Replace("&gt;", ">");

                    XmlDocument innerDoc = new XmlDocument();
                    innerDoc.LoadXml(notesXML);

                    XmlNodeList spanList = innerDoc.SelectNodes("//Span");

                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine(entry.FileName);

                    for (int i = 0; i < spanList.Count; i++)
                    {
                        stringBuilder.AppendLine(spanList[i].Attributes["Text"].Value);
                    }

                    stringBuilder.AppendLine("-");
                    noteText += stringBuilder.ToString();

                    progressBar.PerformStep();
                }
            }

            // Finish up
            progressBar.Value = progressBar.Maximum;

            WriteFile(noteText);
        }

        public void WriteFile(string contents)
        {
            string newPath = Path.GetDirectoryName(PathToFile) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(PathToFile) + "-output.txt";
            File.Create(newPath).Close();

            using (var writer = new StreamWriter(newPath))
            {
                writer.Write(contents);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~StoryReader() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
