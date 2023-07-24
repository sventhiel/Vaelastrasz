using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteDescription
    {
        [JsonProperty("lang")]
        [XmlElement("lang")]
        public string Language { get; set; }

        [JsonProperty("description")]
        [XmlElement("description")]
        public string Description { get; set; }

        [JsonProperty("descriptionType")]
        [XmlElement("descriptionType")]
        public DataCiteDescriptionType? DescriptionType { get; set; }

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
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteDescriptionType
    {
        [EnumMember(Value = "Abstract")]
        Abstract = 1,

        [EnumMember(Value = "Methods")]
        Methods = 2,

        [EnumMember(Value = "SeriesInformation")]
        SeriesInformation = 3,

        [EnumMember(Value = "TableOfContents")]
        TableOfContents = 4,

        [EnumMember(Value = "TechnicalInfo")]
        TechnicalInfo = 5,

        [EnumMember(Value = "Other")]
        Other = 6
    }
}