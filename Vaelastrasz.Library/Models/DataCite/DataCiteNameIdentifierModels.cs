using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteNameIdentifier
    {
        [Required]
        [JsonProperty("nameIdentifier")]
        [XmlElement("nameIdentifier")]
        public string NameIdentifier { get; set; }

        [Required]
        [JsonProperty("nameIdentifierScheme")]
        [XmlElement("nameIdentifierScheme")]
        public string NameIdentifierScheme { get; set; }

        [JsonProperty("schemeUri")]
        [XmlElement("schemeUri")]
        public string SchemeUri { get; set; }

        public DataCiteNameIdentifier()
        { }
    }
}