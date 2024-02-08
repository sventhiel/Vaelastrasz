using Newtonsoft.Json;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteTitle
    {
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

        [JsonProperty("lang")]
        [XmlElement("lang")]
        public string Language { get; set; }

        [JsonProperty("title")]
        [XmlElement("title")]
        public string Title { get; set; }

        [JsonProperty("titleType")]
        [XmlElement("titleType")]
        public DataCiteTitleType? TitleType { get; set; }
    }
}