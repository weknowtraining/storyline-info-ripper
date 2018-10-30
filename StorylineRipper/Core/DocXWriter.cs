using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StorylineRipper.Common.Extensions;
using StorylineRipper.Core.Content;
using Xceed.Words.NET;

namespace StorylineRipper.Core
{
    class DocXWriter
    {
        private DocX output;

        public DocXWriter() { }

        public DocXWriter(string filePath)
        {
            File.Create(filePath).Dispose();
            output = DocX.Create(filePath);
        }

        public void GenerateNotesReport(StoryContent story)
        {
            MainForm.AddToLog("Beginning translation to notes report.");
            MainForm.UpdateMicroProgress(0, 1);

            int totalSlides = story.GetSlideCount();
            int currSlide = 0;

            // for every slide within every scene...
            for (int x = 0; x < story.Scenes.Length; x++)
            {
                MainForm.AddToLog($"Translating [{story.Scenes[x].Name}]");
                output.InsertParagraph($"{(x + 1).ToString("D2")} - {story.Scenes[x].Name}", false).Heading(HeadingType.Heading1);

                for (int y = 0; y < story.Scenes[x].Slides.Length; y++)
                {
                    if (story.Scenes[x].Slides[y].Notes.IsNullOrEmpty())
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

        public void GenerateNarrationReport(StoryContent story, string filePath, bool addContextLines = false)
        {
            int totalSlides = story.GetSlideCount();

            Dictionary<string, List<Line>> scripts = new Dictionary<string, List<Line>>();
            List<Line> allLines = new List<Line>();

            foreach (string name in story.Characters)
                scripts.Add(name, new List<Line>());

            // Grabbing all the lines and adding them to my own lists for reference
            for (int x = 0, currSlide = 0; x < story.Scenes.Length; x++)
                for (int y = 0; y < story.Scenes[x].Slides.Length; y++, currSlide++)
                {
                    if (story.Scenes[x].Slides[y].Lines.IsNullOrEmpty())
                        continue; // Just skip writing the notes if there aren't any.

                    foreach (var line in story.Scenes[x].Slides[y].Lines)
                    {
                        scripts[line.Character].Add(line);
                        allLines.Add(line);
                    }
                }

            // For each character...
            foreach (KeyValuePair<string, List<Line>> lines in scripts)
            {
                string reportPath = filePath + $"-NarrationReport[{lines.Key}].docx";
                File.Create(reportPath).Dispose();
                output = DocX.Create(reportPath);

                output.InsertParagraph($"{lines.Key}").Heading(HeadingType.Heading1);
                for (int i = 0; i < lines.Value.Count; i++)
                {
                    int lineIndex = allLines.FindIndex(l => l.Id == lines.Value[i].Id);
                    Line prevLine;
                    Line nextLine;

                    string displayID = $"{lines.Value[i].Id.Split('.')[0]}.{lines.Value[i].Id.Split('.')[1]}";
                    output.InsertParagraph($"{lines.Key}-{displayID}").Heading(HeadingType.Heading2);

                    // Previous line
                    if (addContextLines && lineIndex > 0 && allLines[lineIndex - 1].DisplayId == displayID)
                    {
                        prevLine = allLines[lineIndex - 1];
                        output.InsertParagraph($"{prevLine.Character}: {prevLine.Text}").Color(Color.Gray).SpacingAfter(14).IndentationBefore = 1.0f;
                    }

                    output.InsertParagraph($"{lines.Value[i].Character}: ").Bold().Append($"{lines.Value[i].Text}").SpacingAfter(14).IndentationBefore = 1.0f;

                    // Next line
                    if (addContextLines && lineIndex < allLines.Count - 1 && allLines[lineIndex + 1].DisplayId == displayID)
                    {
                        nextLine = allLines[lineIndex + 1];
                        output.InsertParagraph($"{nextLine.Character}: {nextLine.Text}").Color(Color.Gray).SpacingAfter(14).IndentationBefore = 1.0f;
                    }
                }

                output.Save();
                output.Dispose();

                MainForm.AddToLog("Translation Complete");
                MainForm.UpdateMicroProgress(totalSlides, totalSlides);
            }
        }
    }
}
