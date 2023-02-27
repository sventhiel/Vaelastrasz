using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteRight
    {
        [JsonPropertyName("rights")]
        public string Rights { get; set; }

        [JsonPropertyName("rightsUri")]
        public string RightsUri { get; set; }

        [JsonPropertyName("lang")]
        public string Language { get; set; }
    }
}