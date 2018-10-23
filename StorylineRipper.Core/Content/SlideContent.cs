using System;
using System.Xml.Serialization;

namespace StorylineRipper.Core.Content
{
    /// <remarks/>
    [Serializable]
    [XmlRoot("sld", Namespace = "", IsNullable = false)]
    public partial class SlideContent
    {
        /// <remarks/>
        [XmlElement("note")]
        public string Notes { get; set; }

        /// <remarks/>
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}