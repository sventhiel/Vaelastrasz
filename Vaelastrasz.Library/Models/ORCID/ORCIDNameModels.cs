using Newtonsoft.Json;
using Vaelastrasz.Library.Converters;

namespace Vaelastrasz.Library.Models.ORCID
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ORCIDName
    {
        [JsonProperty("given-names.value")]
        public string Firstname { get; set; }

        [JsonProperty("family-name.value")]
        public string Lastname { get; set; }
    }
}