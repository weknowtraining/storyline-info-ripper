using System;
using System.Xml.Serialization;

namespace StorylineRipper.Core.Content
{
    /// <remarks/>
    [Serializable]
    [XmlRoot("Document")]
    public partial class NoteContent
    {
        /// <remarks/>
        [XmlArray("Content", IsNullable = false)]
        [XmlArrayItem("Block", IsNullable = false)]
        public Block[] Blocks { get; set; }
    }

    /// <remarks/>
    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class Block
    {
        /// <remarks/>
        public Style Style { get; set; }

        /// <remarks/>
        [XmlElement("Span")]
        public Span[] Span { get; set; }
    }

    /// <remarks/>
    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class Style
    {
        /// <remarks/>
        public ListStyle ListStyle { get; set; }
    }

    /// <remarks/>
    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class ListStyle
    {
        /// <remarks/>
        [XmlAttribute]
        public string ListType { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string BulletChar { get; set; }
    }

    /// <remarks/>
    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class Span
    {
        /// <remarks/>
        [XmlAttribute]
        public string Text { get; set; }
    }
}