using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteFundingReference
    {
        [JsonProperty("funderName")]
        public string FunderName { get; set; }

        [JsonProperty("funderIdentifier")]
        public string FunderIdentifier { get; set; }

        [JsonProperty("funderIdentifierType")]
        public FunderIdentifierType FunderIdentifierType { get; set; }

        [JsonProperty("awardNumber")]
        public string AwardNumber { get; set; }

        [JsonProperty("awardUri")]
        public string AwardUri { get; set; }

        [JsonProperty("awardTitle")]
        public string AwardTitle { get; set; }
    }

    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
    public enum FunderIdentifierType
    {
        [EnumMember(Value = "Crossref Funder ID")]
        CrossrefFunderID = 1,

        GRID = 2,
        ISNI = 3,
        ROR = 4,
        Other = 5
    }
}