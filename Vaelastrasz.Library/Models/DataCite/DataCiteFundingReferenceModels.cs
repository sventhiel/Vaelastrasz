using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteFundingReference
    {
        public DataCiteFundingReference()
        { }

        [JsonProperty("awardNumber")]
        public string AwardNumber { get; set; }

        [JsonProperty("awardTitle")]
        public string AwardTitle { get; set; }

        [JsonProperty("awardUri")]
        public string AwardUri { get; set; }

        [JsonProperty("funderIdentifier")]
        public string FunderIdentifier { get; set; }

        [Required]
        [JsonProperty("funderIdentifierType")]
        public DataCiteFunderIdentifierType FunderIdentifierType { get; set; }

        [Required]
        [JsonProperty("funderName")]
        public string FunderName { get; set; }
    }
}