using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteAffiliation
    {
        public DataCiteAffiliation()
        { }

        [JsonProperty("affiliationIdentifier")]
        public string AffiliationIdentifier { get; set; }

        [Required]
        [JsonProperty("affiliationIdentifierScheme")]
        public string AffiliationIdentifierScheme { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("schemeUri")]
        public string SchemeUri { get; set; }
    }
}