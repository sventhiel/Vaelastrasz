using Newtonsoft.Json;
using Vaelastrasz.Library.Converters;

namespace Vaelastrasz.Library.Models.ORCID
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ORCIDPerson
    {
        [JsonProperty("name")]
        public ORCIDName Name { get; set; }
    }
}