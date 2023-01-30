using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteTitleModel
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("lang")]
        public string Language { get; set; }

        [JsonPropertyName("titleType")]
        public DataCiteTitleType? TitleType { get; set; }

        [JsonConstructor]
        public DataCiteTitleModel() { }
        
        public DataCiteTitleModel(string title, string lang = null, DataCiteTitleType? titleType = null)
        {
            Title = title;

            if (lang != null)
                Language = lang;

            if (titleType != null)
                TitleType = titleType;
        }
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum DataCiteTitleType
    {
        [JsonPropertyName("AlternativeTitle")]
        AlternativeTitle = 1,

        [JsonPropertyName("Subtitle")]
        Subtitle = 2,

        [JsonPropertyName("TranslatedTitle")]
        TranslatedTitle = 3,

        [JsonPropertyName("Other")]
        Other = 4
    }
}