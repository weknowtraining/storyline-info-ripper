using System;
using System.Linq;
using System.Xml.Serialization;

using StorylineRipper.Common.Extensions;

namespace StorylineRipper.Core
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