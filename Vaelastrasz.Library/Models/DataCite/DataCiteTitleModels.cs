using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteTitle
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("lang")]
        public string Language { get; set; }

        [JsonPropertyName("titleType")]
        public DataCiteTitleType? TitleType { get; set; }

        public DataCiteTitle()
        { }

        public DataCiteTitle(string title, string lang = null, DataCiteTitleType? titleType = null)
        {
            Title = title;

            if (lang != null)
                Language = lang;

            if (titleType != null)
                TitleType = titleType;
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DataCiteTitleType
    {
        AlternativeTitle = 1,
        Subtitle = 2,
        TranslatedTitle = 3,
        Other = 4
    }
}