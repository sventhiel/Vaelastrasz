using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteNameIdentifier
    {
        [JsonProperty("nameIdentifier")]
        [XmlElement("nameIdentifier")]
        public string NameIdentifier { get; set; }

        [JsonProperty("nameIdentifierScheme")]
        [XmlElement("nameIdentifierScheme")]
        public string NameIdentifierScheme { get; set; }

        [JsonProperty("schemeUri")]
        [XmlElement("schemeUri")]
        public string SchemeUri { get; set; }
    }
}