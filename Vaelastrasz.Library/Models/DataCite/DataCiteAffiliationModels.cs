using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteAffiliation
    {
        [JsonProperty("affiliationIdentifier")]
        [XmlElement("affiliationIdentifier")]
        public string AffiliationIdentifier { get; set; }

        [JsonProperty("affiliationIdentifierScheme")]
        [XmlElement("affiliationIdentifierScheme")]
        public string AffiliationIdentifierScheme { get; set; }

        [JsonProperty("name")]
        [XmlElement("name")]
        public string Name { get; set; }

        [JsonProperty("schemeUri")]
        [XmlElement("schemeUri")]
        public string SchemeUri { get; set; }
    }
}