using Newtonsoft.Json;
using Vaelastrasz.Library.Converters;

namespace Vaelastrasz.Library.Models
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ReadORCIDModel
    {
        [JsonProperty("person")]
        public ReadORCIDModel Person { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}