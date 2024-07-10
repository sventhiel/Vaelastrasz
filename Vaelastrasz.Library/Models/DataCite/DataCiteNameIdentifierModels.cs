using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteNameIdentifier
    {
        public DataCiteNameIdentifier()
        { }

        [Required]
        [JsonProperty("nameIdentifier")]
        public string NameIdentifier { get; set; }

        [Required]
        [JsonProperty("nameIdentifierScheme")]
        public string NameIdentifierScheme { get; set; }

        [JsonProperty("schemeUri")]
        public string SchemeUri { get; set; }
    }
}