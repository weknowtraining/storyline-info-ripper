using System;
using System.Linq;
using System.Xml.Serialization;

using StorylineRipper.Common.Extensions;

namespace StorylineRipper.Core
{
    public class SlideManifest
    {
        public Story story;
        private Relations rels;

        public SlideManifest(StoryReader reader, string storyXml, string relsXml)
        {
            story = storyXml.Deserialize<Story>();
            rels = relsXml.Deserialize<Relations>();

            for (int x = 0; x < story.Scenes.Length; x++)
                for (int y = 0; y < story.Scenes[x].Slides.Length; y++)
                {
                    Slide slide = story.Scenes[x].Slides[y];

                    slide.Index = string.Format("{0}.{1}", x + 1, y + 1);
                    slide.Path = rels.Relationships.Single(r => r.Id == slide.Id).Path;

                    SlideContent content = reader.GetXmlTextAtPath(slide.Path.TrimStart('/')).Deserialize<SlideContent>();
                    slide.Notes = content.Notes;
                    slide.Name = content.Name;
                }
        }
    }

    #region story.xml

    /// <remarks/>
    [Serializable]
    [XmlRoot("story")]
    public partial class Story
    {
        /// <remarks/>
        [XmlArray("sceneLst", IsNullable = false)]
        [XmlArrayItem("scene", IsNullable = false)]
        public Scene[] Scenes { get; set; }
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

        public string Name { get; set; }
        public string Notes { get; set; }
        public string Path { get; set; }

        [XmlIgnore]
        public string Index { get; set; }
    }
    #endregion story.xml

    #region _rels.xml

    [Serializable]
    [XmlRoot("Relationships", Namespace = "http://schemas.openxmlformats.org/package/2006/relationships")]
    public partial class Relations
    {
        [XmlElement("Relationship", IsNullable = false)]
        public Relationship[] Relationships { get; set; }
    }

    [Serializable]
    [XmlType("Relationship", AnonymousType = true)]
    public partial class Relationship
    {
        [XmlAttribute("Type")]
        public string Type { get; set; }

        [XmlAttribute("Target")]
        public string Path { get; set; }

        [XmlAttribute("Id")]
        public string Id { get; set; }
    }
    #endregion _rels.xml
}