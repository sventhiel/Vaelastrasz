using Newtonsoft.Json;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteRight
    {
        [JsonProperty("rights")]
        public string Rights { get; set; }

        [JsonProperty("rightsUri")]
        public string RightsUri { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }
    }
}