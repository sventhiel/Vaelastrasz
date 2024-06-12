using Newtonsoft.Json;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteRelatedIdentifier
    {
        [JsonProperty("relatedIdentifier")]
        [XmlElement("relatedIdentifier")]
        public string RelatedIdentifier { get; set; }

        [JsonProperty("relatedIdentifierType")]
        [XmlElement("relatedIdentifierType")]
        public DataCiteRelatedIdentifierType RelatedIdentifierType { get; set; }

        [JsonProperty("relationType")]
        [XmlElement("relationType")]
        public DataCiteRelationType RelationType { get; set; }

        [JsonProperty("resourceTypeGeneral")]
        [XmlElement("resourceTypeGeneral")]
        public DataCiteResourceTypeGeneral ResourceTypeGeneral { get; set; }

        [JsonProperty("relatedMetadataScheme")]
        [XmlElement("relatedMetadataScheme")]
        public string RelatedMetadataScheme { get; set; }

        [JsonProperty("schemeUri")]
        [XmlElement("schemeUri")]
        public string SchemeUri { get; set; }

        [JsonProperty("schemeType")]
        [XmlElement("schemeType")]
        public string SchemeType { get; set; }

        public DataCiteRelatedIdentifier()
        {
            
        }
    }
}