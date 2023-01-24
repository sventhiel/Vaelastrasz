using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class CreateDataCiteDataModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public DataCiteType Type { get; set; }

        [JsonPropertyName("attributes")]
        public DataCiteAttributesModel Attributes { get; set; }
    }

    public class UpdateDataCiteDataModel
    {

    }

    public class ReadDataCiteDataModel
    {

    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum DataCiteType
    {
        [JsonPropertyName("dois")]
        DOIs = 1
    }
}
