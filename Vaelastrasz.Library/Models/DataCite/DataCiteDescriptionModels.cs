using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{


    public class DataCiteDescription
    {
        public DataCiteDescription()
        { }

        public DataCiteDescription(string description, string lang = null, DataCiteDescriptionType? descriptionType = null)
        {
            Description = description;

            if (lang != null)
                Language = lang;

            if (descriptionType != null)
                DescriptionType = descriptionType;
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