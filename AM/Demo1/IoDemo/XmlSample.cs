using System;

using System.Xml.Serialization;

namespace IoDemo
{
    [Serializable]
    public class XmlSample
    {
        [Obsolete("This constructor is only for XmlSerializer", error: true)]
        public XmlSample()
        {
            
        }

        public XmlSample(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
