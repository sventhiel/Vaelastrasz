using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteDataModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        public DataCiteType Type { get; set; }

        public DataCiteAttributesModel Attributes { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum DataCiteType
    {
        [JsonPropertyName("dois")]
        DOIs = 1
    }
}
