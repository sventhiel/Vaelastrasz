using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteAffiliation
    {
        [JsonPropertyName("affiliationIdentifier")]
        public string AffiliationIdentifier { get; set; }

        [JsonPropertyName("affiliationIdentifierScheme")]
        public string AffiliationIdentifierScheme { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("schemeUri")]
        public string SchemeUri { get; set; }
    }
}