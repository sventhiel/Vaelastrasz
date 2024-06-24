using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteDate
    {
        public DataCiteDate()
        { }

        [Required]
        [JsonProperty("date")]
        [XmlElement("date")]
        public string Date { get; set; }

        [Required]
        [JsonProperty("dateType")]
        [XmlElement("dateType")]
        public DataCiteDateType DateType { get; set; }

        [JsonProperty("dateInformation")]
        [XmlElement("dateInformation")]
        public string DateInformation { get; set; }
    }
}