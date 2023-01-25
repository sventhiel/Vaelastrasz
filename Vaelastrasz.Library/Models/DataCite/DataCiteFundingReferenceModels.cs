using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteFundingReferenceModel
    {
        [JsonPropertyName("funderName")]
        public string FunderName { get; set; }

        [JsonPropertyName("funderIdentifier")]
        public string FunderIdentifier { get; set; }

        [JsonPropertyName("funderIdentifierType")]
        public FunderIdentifierType FunderIdentifierType { get; set; }

        [JsonPropertyName("awardNumber")]
        public string AwardNumber { get; set; }

        [JsonPropertyName("awardUri")]
        public string AwardUri { get; set; }

        [JsonPropertyName("awardTitle")]
        public string AwardTitle { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum FunderIdentifierType
    {
        [JsonPropertyName("Crossref Funder ID")]
        CrossrefFunderID = 1,

        [JsonPropertyName("GRID")]
        GRID = 2,

        [JsonPropertyName("ISNI")]
        ISNI = 3,

        [JsonPropertyName("ROR")]
        ROR = 4,

        [JsonPropertyName("Other")]
        Other = 5
    }
}