using Newtonsoft.Json;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteNameIdentifier
    {
        [JsonProperty("nameIdentifier")]
        public string NameIdentifier { get; set; }

        [JsonProperty("nameIdentifierScheme")]
        public string NameIdentifierScheme { get; set; }

        [JsonProperty("schemeUri")]
        public string SchemeUri { get; set; }
    }
}