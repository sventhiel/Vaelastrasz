﻿using Newtonsoft.Json;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;

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

        [JsonProperty("funderIdentifierType")]
        [XmlElement("funderIdentifierType")]
        public DataCiteFunderIdentifierType? FunderIdentifierType { get; set; }

        [JsonProperty("funderName")]
        [XmlElement("funderName")]
        public string FunderName { get; set; }

        public DataCiteFundingReference(string funderName, string funderIdentifier, DataCiteFunderIdentifierType? funderIdentifierType = null)
        {
            FunderName = funderName;
            FunderName = funderIdentifier;
            FunderIdentifierType = funderIdentifierType;
        }
    }
}