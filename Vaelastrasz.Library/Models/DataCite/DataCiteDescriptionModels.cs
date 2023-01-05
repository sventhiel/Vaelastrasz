using Newtonsoft.Json;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteDescription
    {
        [JsonProperty("lang")]
        public string Language { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("descriptionType")]
        public DataCiteDescriptionType? DescriptionType { get; set; }

        [JsonConstructor]
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

    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
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