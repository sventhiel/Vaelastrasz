using Newtonsoft.Json;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteAffiliation
    {
        [JsonProperty("affiliationIdentifier")]
        public string AffiliationIdentifier { get; set; }

        [JsonProperty("affiliationIdentifierScheme")]
        public string AffiliationIdentifierScheme { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("schemeUri")]
        public string SchemeUri { get; set; }
    }
}