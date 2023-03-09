using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteFundingReference
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

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FunderIdentifierType
    {
        [EnumMember(Value = "Crossref Funder ID")]
        CrossrefFunderID = 1,
        [EnumMember(Value = "GRID")]
        GRID = 2,
        [EnumMember(Value = "ISNI")]
        ISNI = 3,
        [EnumMember(Value = "ROR")]
        ROR = 4,
        [EnumMember(Value = "Other")]
        Other = 5
    }
}