using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteNameIdentifierModel
    {
        [JsonPropertyName("nameIdentifier")]
        public string NameIdentifier { get; set; }

        [JsonPropertyName("nameIdentifierScheme")]
        public string NameIdentifierScheme { get; set; }

        [JsonPropertyName("schemeUri")]
        public string SchemeUri { get; set; }
    }
}