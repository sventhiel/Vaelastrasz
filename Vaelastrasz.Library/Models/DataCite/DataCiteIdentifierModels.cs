using Newtonsoft.Json;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;
using System.ComponentModel.DataAnnotations;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteIdentifier
    {
        [Required]
        [JsonProperty("identifier")]
        [XmlElement("identifier")]
        public string Identifier { get; set; }

        [Required]
        [JsonProperty("identifierType")]
        [XmlElement("identifierType")]
        public DataCiteIdentifierType IdentifierType { get; set; }

        public DataCiteIdentifier()
        { }
    }
}