using System.Runtime.Serialization;
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
        [EnumMember(Value = "AlternativeTitle")]
        AlternativeTitle = 1,
        [EnumMember(Value = "Subtitle")]
        Subtitle = 2,
        [EnumMember(Value = "TranslatedTitle")]
        TranslatedTitle = 3,
        [EnumMember(Value = "Other")]
        Other = 4
    }
}