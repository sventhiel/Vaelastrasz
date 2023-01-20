using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteTitle
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }

        [JsonProperty("titleType")]
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

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteTitleType
    {
        AlternativeTitle = 1,
        Subtitle = 2,
        TranslatedTitle = 3,
        Other = 4
    }
}