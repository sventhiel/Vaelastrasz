using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteDescription
    {
        [JsonPropertyName("lang")]
        public string Language { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("descriptionType")]
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

    [JsonConverter(typeof(JsonStringEnumConverter))]
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