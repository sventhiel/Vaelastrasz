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
        Abstract = 1,
        Methods = 2,
        SeriesInformation = 3,
        TableOfContents = 4,
        TechnicalInfo = 5,
        Other = 6
    }
}