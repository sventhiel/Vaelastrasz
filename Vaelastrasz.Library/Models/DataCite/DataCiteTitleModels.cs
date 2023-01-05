using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteTitle
    {
        [JsonProperty("title")]
        [Required]
        public string Title { get; set; }

        [JsonProperty("lang", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Language { get; set; }

        [JsonProperty("titleType", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DataCiteTitleType? TitleType { get; set; }

        [JsonConstructor]
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

    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
    public enum DataCiteTitleType
    {
        AlternativeTitle = 1,
        Subtitle = 2,
        TranslatedTitle = 3,
        Other = 4
    }
}