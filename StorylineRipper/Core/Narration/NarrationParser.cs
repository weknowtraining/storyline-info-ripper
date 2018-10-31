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
        public List<string> Characters { get; private set; }

        private StoryContent content;

        private const string REGEX_MARKUP = "<(.+)\\|([\\S\\s]*?)>";
        private const string REGEX_READABLE = "(^[^a-z\\n\\v]+?):\\s((?:.|\\v)+?(?=(?:^[^a-z\\v]+?:))|(?:.|\\v)+?$)";
        private const string REGEX_SELECTBRACES = "\\s?{(?:.|\\v)+?}\\s?";

        public NarrationParser(StoryContent content)
        {
            Characters = new List<string>();

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

                    Regex regex = new Regex(inputIsMarkup?REGEX_MARKUP:REGEX_READABLE, RegexOptions.Multiline | RegexOptions.Singleline);
                    MatchCollection coll = regex.Matches(slide.Notes);

                    int i = 1;
                    foreach (Match match in coll)
                    {
                        slide.Lines.Add(new Line(
                            $"{slide.Index}.{i++}", // Ex. 03.21.1 - Used for line number within a slide
                            match.Groups[1].Value.TrimEnd(':').Trim(), // The character name
                            Regex.Replace(match.Groups[2].Value.Trim(), REGEX_SELECTBRACES, ReplaceEvaluater) // Remove everything between { } braces.
                            ));

                        if (!Characters.Contains(match.Groups[1].Value.Trim()))
                            Characters.Add(match.Groups[1].Value.Trim());
                    }
                }

            content.Characters = Characters.ToArray();
        }

        /// <summary>
        /// Used for the replacement regex to determine if a blank space should be returned or not
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public string ReplaceEvaluater(Match match)
        {
            bool isBlankBefore = match.Value[0] == ' ';
            bool isBlankAfter = match.Value[match.Value.Length - 1] == ' ';

            if (isBlankBefore && isBlankAfter)
                return " ";
            else if (!isBlankBefore && !isBlankAfter)
                return "";
            else if (isBlankBefore && !isBlankAfter)
                return " ";
            else // !isBlankBefore && isBlankAfter
                return "";
        }
    }
}
