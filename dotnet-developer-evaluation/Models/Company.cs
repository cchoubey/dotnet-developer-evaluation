using System.Net;
using System.Xml.Serialization;

namespace dotnet_developer_evaluation.Models
{
    [XmlRoot(ElementName = "Data")]
    public class Company
    {
        [XmlElement(ElementName = "id")]
        public int Id { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
    }
}
