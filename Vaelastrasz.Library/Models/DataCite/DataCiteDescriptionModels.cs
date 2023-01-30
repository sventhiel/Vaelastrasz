using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteDescriptionModel
    {
        [JsonPropertyName("lang")]
        public string Language { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("descriptionType")]
        public DataCiteDescriptionType? DescriptionType { get; set; }

        [JsonConstructor]
        public DataCiteDescriptionModel() { }

        public DataCiteDescriptionModel(string description, string lang = null, DataCiteDescriptionType? descriptionType = null)
        {
            Description = description;

            if (lang != null)
                Language = lang;

            if (descriptionType != null)
                DescriptionType = descriptionType;
        }
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum DataCiteDescriptionType
    {
        [JsonPropertyName("abstract")]
        Abstract = 1,

        [JsonPropertyName("methods")]
        Methods = 2,

        [JsonPropertyName("seriesInformation")]
        SeriesInformation = 3,

        [JsonPropertyName("tableOfContents")]
        TableOfContents = 4,

        [JsonPropertyName("technicalInfo")]
        TechnicalInfo = 5,

        [JsonPropertyName("other")]
        Other = 6
    }
}