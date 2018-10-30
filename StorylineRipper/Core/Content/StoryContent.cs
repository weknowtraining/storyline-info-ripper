using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace StorylineRipper.Core.Content
{
    /// <remarks/>
    [Serializable]
    [XmlRoot("story")]
    public partial class StoryContent
    {
        public string[] Characters { get; set; }

        private int slideCount = -1; // Cache the slide count

        private Scene[] _scenes;

        /// <remarks/>
        [XmlArray("sceneLst", IsNullable = false)]
        [XmlArrayItem("scene", IsNullable = false)]
        public Scene[] Scenes {
            get { return _scenes; }
            set
            {
                _scenes = value;
                slideCount = -1; // Reset slide count
            }
        }

        public int GetSlideCount()
        {
            if (slideCount == -1) // If count has been reset, recalculate, otherwise return the cached result
            {
                slideCount = 0;
                for (int x = 0; x < Scenes.Length; x++)
                    slideCount += Scenes[x].Slides.Length;
            }

            return slideCount;
        }
    }

    /// <remarks/>
    [Serializable]
    [XmlType("scene", AnonymousType = true)]
    public partial class Scene
    {
        /// <remarks/>
        [XmlArray("sldIdLst", IsNullable = false)]
        [XmlArrayItem("sldId", IsNullable = false)]
        public Slide[] Slides { get; set; }

        /// <remarks/>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <remarks/>
        [XmlAttribute("desc")]
        public string Description { get; set; }
    }

    [Serializable]
    [XmlType("sldId", AnonymousType = true)]
    public partial class Slide
    {
        [XmlText]
        public string Id { get; set; }
        /// <summary> Relative path to this slide's xml file </summary>
        public string Path { get; set; }

        public string Name { get; set; }

        /// <summary> Compiled text from the Notes section of this slide </summary>
        public string Notes { get; set; }

        [XmlIgnore]
        /// <summary> Notes converted to narration lines </summary>
        public List<Line> Lines = new List<Line>();

        [XmlIgnore]
        public string Index { get; set; }
    }

    public partial class Line
    {
        public Line(string id, string character, string text)
        {
            Id = id;
            Character = character;
            Text = text;
        }

        public string Id { get; set; }
        public string Character { get; set; }
        public string Text { get; set; }

        public string DisplayId { get { return $"{Id.Split('.')[0]}.{Id.Split('.')[1]}"; } }
    }
}
