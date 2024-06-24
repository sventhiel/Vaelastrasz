using Newtonsoft.Json;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteAffiliation
    {
        [JsonProperty("affiliationIdentifier")]
        [XmlElement("affiliationIdentifier")]
        public string AffiliationIdentifier { get; set; }

        [Required]
        [JsonProperty("affiliationIdentifierScheme")]
        [XmlElement("affiliationIdentifierScheme")]
        public string AffiliationIdentifierScheme { get; set; }

        [JsonProperty("name")]
        [XmlElement("name")]
        public string Name { get; set; }

        [JsonProperty("schemeUri")]
        [XmlElement("schemeUri")]
        public string SchemeUri { get; set; }

        public DataCiteAffiliation()
        { }
    }
}