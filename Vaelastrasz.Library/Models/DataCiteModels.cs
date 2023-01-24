using System.Text.Json.Serialization;
using Vaelastrasz.Library.Models.DataCite;

namespace Vaelastrasz.Library.Models
{
    public class CreateDataCiteModel
    {
        [JsonPropertyName("data")]
        public CreateDataCiteDataModel Data { get; set; }
    }

    public class UpdateDataCiteModel
    {
        [JsonPropertyName("data")]
        public UpdateDataCiteDataModel Data { get; set; }
    }

    public class ReadDataCiteModel
    {
        [JsonPropertyName("data")]
        public ReadDataCiteDataModel Data { get; set; }
    }
}