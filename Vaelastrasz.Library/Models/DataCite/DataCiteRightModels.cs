using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteRight
    {
        [JsonProperty("lang")]
        [XmlElement("lang")]
        public string Language { get; set; }

        [JsonProperty("rights")]
        [XmlElement("rights")]
        public string Rights { get; set; }

        [JsonProperty("rightsIdentifier")]
        [XmlElement("rightsIdentifier")]
        public string RightsIdentifier { get; set; }

        [JsonProperty("rightsIdentifierScheme")]
        [XmlElement("rightsIdentifierScheme")]
        public string RightsIdentifierScheme { get; set; }

        [JsonProperty("rightsUri")]
        [XmlElement("rightsUri")]
        public string RightsUri { get; set; }

        [JsonProperty("schemeUri")]
        [XmlElement("schemeUri")]
        public string SchemeUri { get; set; }
    }
}