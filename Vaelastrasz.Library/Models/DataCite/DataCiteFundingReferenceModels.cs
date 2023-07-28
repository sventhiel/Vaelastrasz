using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    [JsonConverter(typeof(StringEnumConverter))]
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

    public class DataCiteFundingReference
    {
        [JsonProperty("awardNumber")]
        [XmlElement("awardNumber")]
        public string AwardNumber { get; set; }

        [JsonProperty("awardTitle")]
        [XmlElement("awardTitle")]
        public string AwardTitle { get; set; }

        [JsonProperty("awardUri")]
        [XmlElement("awardUri")]
        public string AwardUri { get; set; }

        [JsonProperty("funderIdentifier")]
        [XmlElement("funderIdentifier")]
        public string FunderIdentifier { get; set; }

        [JsonProperty("funderIdentifierType")]
        [XmlElement("funderIdentifierType")]
        public FunderIdentifierType FunderIdentifierType { get; set; }

        [JsonProperty("funderName")]
        [XmlElement("funderName")]
        public string FunderName { get; set; }
    }
}