using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteRight
    {
        public DataCiteRight()
        { }

        [JsonProperty("lang")]
        public string Language { get; set; }

        [Required]
        [JsonProperty("rights")]
        public string Rights { get; set; }

        [JsonProperty("rightsIdentifier")]
        public string RightsIdentifier { get; set; }

        [JsonProperty("rightsIdentifierScheme")]
        public string RightsIdentifierScheme { get; set; }

        [JsonProperty("rightsUri")]
        public string RightsUri { get; set; }

        [JsonProperty("schemeUri")]
        public string SchemeUri { get; set; }
    }
}