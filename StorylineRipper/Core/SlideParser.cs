using System;
using System.Linq;
using System.Text;

using StorylineRipper.Common.Extensions;
using StorylineRipper.Core.Content;

namespace StorylineRipper.Core
{
    public class SlideParser
    {
        public StoryContent story;
        private RelationsContent rels;
        private StoryReader reader;

        public SlideParser(StoryReader reader, string storyXml, string relsXml)
        {
            this.reader = reader;
            story = storyXml.Deserialize<StoryContent>();
            MainForm.AddToLog("story.xml manifest deserialized.");
            rels = relsXml.Deserialize<RelationsContent>();
            MainForm.AddToLog("_rels.xml manifest deserialized.");
        }

        public void ParseData(Action<int, int> OnProgressUpdated)
        {
            MainForm.AddToLog("Preparing to parse.");
            OnProgressUpdated.Invoke(0, 0);
            int slideCount = 0;
            int currSlide = 0;
            
            // Peek ahead to see how many slides we will be parsing
            for (int x = 0; x < story.Scenes.Length; x++)
                for (int y = 0; y < story.Scenes[x].Slides.Length; y++)
                {
                    slideCount++;
                }

            MainForm.AddToLog("Gaining Insight on workload.");

            // for every slide within every scene...
            for (int x = 0; x < story.Scenes.Length; x++)
                for (int y = 0; y < story.Scenes[x].Slides.Length; y++)
                {
                    Slide slide = story.Scenes[x].Slides[y];

                    slide.Index = string.Format("{0}.{1}", x + 1, y + 1);
                    slide.Path = rels.Relationships.Single(r => r.Id == slide.Id).Path; // Find this slide within the relationships doc and get the path to it

                    // Get relative path to slide
                    SlideContent content = reader.GetXmlTextAtPath(slide.Path.TrimStart('/')).Deserialize<SlideContent>();
                    slide.Name = content.Name;

                    MainForm.AddToLog($"Slide path found: {slide.Path}");

                    if (content.Notes.Trim() != "")
                    {
                        NoteContent noteContent = content.Notes.Deserialize<NoteContent>();
                        Block[] blocks = noteContent.Blocks.Where(b => b.Span != null).ToArray();
                        StringBuilder stringBuilder = new StringBuilder();

                        MainForm.AddToLog("Notes parsed from xml.");

                        int currentListNumber = 1;
                        // For numbered lists
                        // For every numbered list, this number goes up,
                        // but when the numbered list is broken it resets back to one
                        
                        // For every span within every block...
                        for (int u = 0; u < blocks.Length; u++)
                            for (int v = 0; v < blocks[u].Span.Length; v++)
                            {
                                Block block = blocks[u];
                                Span span = blocks[u].Span[v];
                                // If there is a style, it has a list style, and it's list type isn't "None"
                                if (span.Text.Trim().Length > 0 &&
                                    block.Style != null &&
                                    block.Style.ListStyle != null &&
                                    block.Style.ListStyle.ListType != "None")
                                {
                                    if (block.Style.ListStyle.ListType == "Bullet")
                                    {
                                        stringBuilder.Append($"{block.Style.ListStyle.BulletChar} ");
                                        currentListNumber = 1; // Reset
                                    }
                                    else if (block.Style.ListStyle.ListType.Contains("ListNumbered"))
                                    {
                                        stringBuilder.Append($"{currentListNumber++}. ");
                                    }
                                    else
                                    {
                                        // I don't know what other list types there are,
                                        // but it's safe to say they aren't numbered.
                                        currentListNumber = 1;
                                    }
                                }
                                else // No list, reset to 1
                                {
                                    currentListNumber = 1;
                                }

                                // Add a space between spans that aren't line-breaks
                                if (u > 0 && u < blocks.Length - 1 && !span.Text.EndsWith("\n"))
                                    stringBuilder.Append(" ");

                                stringBuilder.Append(span.Text);
                            }
                        MainForm.AddToLog($"{blocks.Select(s => s.Span).Count()} spans parsed.");

                        slide.Notes = stringBuilder.ToString();
                    }
                    
                    OnProgressUpdated.Invoke(currSlide++, slideCount);
                    MainForm.AddToLog($"Slide {slide.Index} parsed.");
                }
            MainForm.AddToLog("All data parsed to codebase!");
        }

        public string GetNarrationReport()
        {
            StringBuilder stringBuilder = new StringBuilder();
            MainForm.AddToLog("Beginning translation to narration report.");
            // for every slide within every scene...
            for (int x = 0; x < story.Scenes.Length; x++)
            {
                stringBuilder.AppendLine($"[{story.Scenes[x].Name}]");
                MainForm.AddToLog($"Translating [{story.Scenes[x].Name}]");

                for (int y = 0; y < story.Scenes[x].Slides.Length; y++)
                {
                    if (story.Scenes[x].Slides[y].Notes == null || story.Scenes[x].Slides[y].Notes.Trim() == "")
                        continue; // Just skip writing the notes if there aren't any.
                    MainForm.AddToLog($"Translating -{story.Scenes[x].Slides[y].Index}-");

                    stringBuilder.AppendLine($"----{story.Scenes[x].Slides[y].Index}----");
                    stringBuilder.AppendLine(story.Scenes[x].Slides[y].Notes + "\n");
                }
            }
            MainForm.AddToLog("Translation Complete");
            return stringBuilder.ToString();
        }
    }
}