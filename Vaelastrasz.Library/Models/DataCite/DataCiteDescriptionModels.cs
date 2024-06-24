using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteDescription
    {
        public DataCiteDescription()
        { }

        [Required]
        [JsonProperty("description")]
        [XmlElement("description")]
        public string Description { get; set; }

        [Required]
        [JsonProperty("descriptionType")]
        [XmlElement("descriptionType")]
        public DataCiteDescriptionType DescriptionType { get; set; }

        [JsonProperty("lang")]
        [XmlElement("lang")]
        public string Language { get; set; }
    }
}