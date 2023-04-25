using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteIdentifier
    {
        [JsonProperty("identifier")]
        [XmlElement("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("identifierType")]
        [XmlElement("identifierType")]
        public string IdentifierType { get; set; }
    }
}