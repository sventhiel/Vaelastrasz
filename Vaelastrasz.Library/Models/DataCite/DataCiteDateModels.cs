using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteDateModel
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("dateType")]
        public DataCiteDateType DateType { get; set; }

        [JsonConstructor]
        public DataCiteDateModel() { }

        public DataCiteDateModel(string date, DataCiteDateType type)
        {
            Date = date;
            DateType = type;
        }
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum DataCiteDateType
    {
        [JsonPropertyName("accepted")]
        Accepted = 1,

        [JsonPropertyName("available")]
        Available = 2,

        [JsonPropertyName("copyrighted")]
        Copyrighted = 3,

        [JsonPropertyName("collected")]
        Collected = 4,

        [JsonPropertyName("created")]
        Created = 5,

        [JsonPropertyName("issued")]
        Issued = 6,

        [JsonPropertyName("submitted")]
        Submitted = 7,

        [JsonPropertyName("updated")]
        Updated = 8,

        [JsonPropertyName("valid")]
        Valid = 9,

        [JsonPropertyName("withdrawn")]
        Withdrawn = 10,

        [JsonPropertyName("other")]
        Other = 11
    }
}