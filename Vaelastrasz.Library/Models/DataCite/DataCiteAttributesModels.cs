using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteAttributesModel
    {
        [JsonPropertyName("doi")]
        public string Doi { get; set; }

        [JsonPropertyName("prefix")]
        public string Prefix { get; set; }

        [JsonPropertyName("suffix")]
        public string Suffix { get; set; }

        [JsonPropertyName("event")]

        public DataCiteEventType Event { get; set; }

        [JsonPropertyName("identifiers")]

        public List<DataCiteIdentifier> Identifiers { get; set; }

        [JsonPropertyName("creators")]
        public List<DataCiteCreator> Creators { get; set; }

        [JsonPropertyName("titles")]
        public List<DataCiteTitle> Titles { get; set; }

        [JsonPropertyName("publisher")]
        public string Publisher { get; set; }

        [JsonPropertyName("publicationYear")]
        public int PublicationYear { get; set; }

        [JsonPropertyName("subjects")]
        public List<DataCiteSubject> Subjects { get; set; }

        [JsonPropertyName("contributors")]
        public List<DataCiteCreator> Contributors { get; set; }

        [JsonPropertyName("dates")]
        public List<DataCiteDate> Dates { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("url")]
        public string URL { get; set; }

        [JsonPropertyName("descriptions")]
        public List<DataCiteDescription> Descriptions { get; set; }

        public DataCiteAttributesModel()
        {
            Creators = new List<DataCiteCreator>();
            Contributors = new List<DataCiteCreator>();
            Dates = new List<DataCiteDate>();
            Descriptions = new List<DataCiteDescription>();
            Identifiers = new List<DataCiteIdentifier>();
            Subjects = new List<DataCiteSubject>();
            Titles = new List<DataCiteTitle>();
        }
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum DataCiteEventType
    {
        [JsonPropertyName("publish")]
        Publish = 1,

        [JsonPropertyName("register")]
        Register = 2,

        [JsonPropertyName("hide")]
        Hide = 3
    }
}
