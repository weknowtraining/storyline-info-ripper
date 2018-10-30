using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using StorylineRipper.Core.Content;

namespace StorylineRipper.Core.Narration
{
    class NarrationParser
    {
        private StoryContent content;

        private List<string> characters;

        private const string REGEX_MARKUP = "<(.+)\\|([\\S\\s]*?)>";
        //private const string REGEX_READABLE = "(^[^a-z\\v]+?:)\\s(\\X+?(?=(?:^[^a-z\\v]+?:))|\\X+?\\z)";
        private const string REGEX_READABLE = "(^[^a-z\\v]+?):\\s((?:.|\\v)+?(?=(?:^[^a-z\\v]+?:))|(?:.|\\v)+?$)";

        public NarrationParser(StoryContent content)
        {
            characters = new List<string>();

            this.content = content;
        }

        public void ParseNotes(bool inputIsMarkup = true)
        {
            for (int x = 0; x < content.Scenes.Length; x++)
                for (int y = 0; y < content.Scenes[x].Slides.Length; y++)
                {
                    Slide slide = content.Scenes[x].Slides[y];

                    if (slide.Notes == null || slide.Notes.Length == 0)
                        continue;

                    Regex regex = new Regex(inputIsMarkup?REGEX_MARKUP:REGEX_READABLE, RegexOptions.Multiline);
                    MatchCollection coll = regex.Matches(slide.Notes);

                    int i = 1;
                    foreach (Match match in coll)
                    {
                        slide.Lines.Add(new Line($"{slide.Index}.{i++}", match.Groups[1].Value.TrimEnd(':').Trim(), match.Groups[2].Value.Trim()));
                        if (!characters.Contains(match.Groups[1].Value.Trim()))
                            characters.Add(match.Groups[1].Value.Trim());
                    }
                }

            content.Characters = characters.ToArray();
        }
    }
}
