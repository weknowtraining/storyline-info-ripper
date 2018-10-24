using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StorylineRipper.Core.Content;
using Xceed.Words.NET;

namespace StorylineRipper.Core
{
    class DocXWriter
    {
        private DocX output;

        public DocXWriter(string filePath)
        {
            File.Create(filePath).Dispose();
            output = DocX.Create(filePath);
        }

        public void GenerateNarrationReport(StoryContent story)
        {
            //StringBuilder stringBuilder = new StringBuilder();
            MainForm.AddToLog("Beginning translation to narration report.");
            MainForm.UpdateMicroProgress(0, 1);

            int totalSlides = 0;
            int currSlide = 0;

            for (int x = 0; x < story.Scenes.Length; x++)
                for (int y = 0; y < story.Scenes[x].Slides.Length; y++)
                    totalSlides++;

            // for every slide within every scene...
            for (int x = 0; x < story.Scenes.Length; x++)
            {
                MainForm.AddToLog($"Translating [{story.Scenes[x].Name}]");
                output.InsertParagraph($"{(x + 1).ToString("D2")} - {story.Scenes[x].Name}", false).Heading(HeadingType.Heading1);

                for (int y = 0; y < story.Scenes[x].Slides.Length; y++)
                {
                    if (story.Scenes[x].Slides[y].Notes == null || story.Scenes[x].Slides[y].Notes.Trim() == "")
                        continue; // Just skip writing the notes if there aren't any.

                    MainForm.AddToLog($"Translating -{story.Scenes[x].Slides[y].Index}-");

                    output.InsertParagraph($"{story.Scenes[x].Slides[y].Index}", false).Heading(HeadingType.Heading2);
                    output.InsertParagraph(story.Scenes[x].Slides[y].Notes, false).IndentationBefore = 1.0f;

                    MainForm.UpdateMicroProgress(++currSlide, totalSlides);
                }
            }
            output.Save();
            output.Dispose();
            MainForm.AddToLog("Translation Complete");
            MainForm.UpdateMicroProgress(totalSlides, totalSlides);
        }
    }
}
