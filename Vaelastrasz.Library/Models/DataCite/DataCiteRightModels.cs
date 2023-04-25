using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteRight
    {
        [JsonProperty("rights")]
        [XmlElement("rights")]
        public string Rights { get; set; }

        [JsonProperty("rightsUri")]
        [XmlElement("rightsUri")]
        public string RightsUri { get; set; }

        [JsonProperty("lang")]
        [XmlElement("lang")]
        public string Language { get; set; }
    }
}