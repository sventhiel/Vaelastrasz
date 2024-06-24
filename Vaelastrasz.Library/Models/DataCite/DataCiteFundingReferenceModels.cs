using Newtonsoft.Json;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;
using System.ComponentModel.DataAnnotations;

namespace Vaelastrasz.Library.Models.DataCite
{
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

        [Required]
        [JsonProperty("funderIdentifierType")]
        [XmlElement("funderIdentifierType")]
        public DataCiteFunderIdentifierType FunderIdentifierType { get; set; }

        [Required]
        [JsonProperty("funderName")]
        [XmlElement("funderName")]
        public string FunderName { get; set; }

        public DataCiteFundingReference()
        { }

        public DataCiteFundingReference(DataCiteFunderIdentifierType funderIdentifierType, string funderName, string awardNumber = null, string awardTitle = null, string awardUri = null, string funderIdentifier = null)
        {
            FunderName = funderName;
            FunderIdentifierType = funderIdentifierType;
            FunderIdentifier = funderIdentifier;
            AwardTitle = awardTitle;
            AwardNumber = awardNumber;
            AwardUri = awardUri;
        }
    }
}