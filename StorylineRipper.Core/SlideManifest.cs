using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using ComLib.Extensions;

namespace ZipReadWrite
{
    public class SlideManifest
    {
        Story story;
        Relations rels;

        public SlideManifest(string storyXml, string relsXml)
        {
            story = storyXml.Deserialize<Story>();
            rels = relsXml.Deserialize<Relations>();

            for (int m = 0; m < story.Scenes.Length; m++)
                for (int s = 0; s < story.Scenes[m].Slides.Length; s++)
                {
                    Slide slide = story.Scenes[m].Slides[s];

                    slide.Index = string.Format("{0}.{1}", m + 1, s + 1);
                    slide.Path = rels.Relationships.Single(x => x.Id == slide.Id).Path;
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