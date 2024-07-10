using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteRelatedItemIdentifier
    {
        public DataCiteRelatedItemIdentifier()
        { }

        [Required]
        [JsonProperty("relatedItemIdentifier")]
        public string RelatedItemIdentifier { get; set; }

        [Required]
        [JsonProperty("relatedItemIdentifierType")]
        public DataCiteRelatedIdentifierType RelatedItemIdentifierType { get; set; }

        [JsonProperty("relatedMetadataScheme")]
        public string RelatedMetadataScheme { get; set; }

        [JsonProperty("schemeType")]
        public string SchemeType { get; set; }

        [JsonProperty("schemeUri")]
        public string SchemeUri { get; set; }
    }
}