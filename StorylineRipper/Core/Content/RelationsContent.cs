using System;
using System.Xml.Serialization;

namespace StorylineRipper.Core.Content
{
    [Serializable]
    [XmlRoot("Relationships", Namespace = "http://schemas.openxmlformats.org/package/2006/relationships")]
    public partial class RelationsContent
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
}
