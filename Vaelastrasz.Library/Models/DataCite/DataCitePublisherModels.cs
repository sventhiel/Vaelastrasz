using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCitePublisher
    {
        public DataCitePublisher()
        { }

        [JsonProperty("lang")]
        public string Language { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("publisherIdentifier")]
        public string PublisherIdentifier { get; set; }

        [Required]
        [JsonProperty("publisherIdentifierScheme")]
        public string PublisherIdentifierScheme { get; set; }

        [JsonProperty("schemeUri")]
        public string SchemeUri { get; set; }
    }
}