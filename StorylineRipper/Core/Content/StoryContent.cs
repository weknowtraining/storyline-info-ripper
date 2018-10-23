using System;
using System.Xml.Serialization;

namespace StorylineRipper.Core.Content
{
    /// <remarks/>
    [Serializable]
    [XmlRoot("story")]
    public partial class StoryContent
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
}
