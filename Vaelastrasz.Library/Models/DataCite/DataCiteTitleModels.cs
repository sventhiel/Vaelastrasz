using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteTitle
    {
        [JsonProperty("title")]
        [XmlElement("title")]
        public string Title { get; set; }

        [JsonProperty("lang")]
        [XmlElement("lang")]
        public string Language { get; set; }

        [JsonProperty("titleType")]
        [XmlElement("titleType")]
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

    [JsonConverter(typeof(StringEnumConverter))]
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