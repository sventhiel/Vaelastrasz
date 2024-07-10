using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteRelatedItemIdentifier
    {
        public DataCiteRelatedItemIdentifier() { }

        [Required]
        [JsonProperty("relatedItemIdentifier")]
        [XmlElement("relatedItemIdentifier")]
        public string RelatedItemIdentifier { get; set; }

        [Required]
        [JsonProperty("relatedItemIdentifierType")]
        [XmlElement("relatedItemIdentifierType")]
        public DataCiteRelatedIdentifierType RelatedItemIdentifierType { get; set; }

        [JsonProperty("relatedMetadataScheme")]
        [XmlElement("relatedMetadataScheme")]
        public string RelatedMetadataScheme { get; set; }

        [JsonProperty("schemeUri")]
        [XmlElement("schemeUri")]
        public string SchemeUri { get; set; }

        [JsonProperty("schemeType")]
        [XmlElement("schemeType")]
        public string SchemeType { get; set; }
    }
}
