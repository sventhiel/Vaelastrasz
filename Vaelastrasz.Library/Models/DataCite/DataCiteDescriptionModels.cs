using Newtonsoft.Json;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteDescription
    {
        public DataCiteDescription()
        { }

        public DataCiteDescription(string description, string lang = null, DataCiteDescriptionType descriptionType = DataCiteDescriptionType.Other)
        {
            Description = description;
            DescriptionType = descriptionType;

            if (lang != null)
                Language = lang;
        }

        [JsonProperty("description")]
        [XmlElement("description")]
        public string Description { get; set; }

        [JsonProperty("descriptionType")]
        [XmlElement("descriptionType")]
        public DataCiteDescriptionType? DescriptionType { get; set; }

        [JsonProperty("lang")]
        [XmlElement("lang")]
        public string Language { get; set; }
    }
}