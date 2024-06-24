using Newtonsoft.Json;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;
using System.ComponentModel.DataAnnotations;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteTypes
    {
        [JsonProperty("bibtex")]
        [XmlElement("bibtex")]
        public string Bibtex { get; set; }

        [JsonProperty("citeproc")]
        [XmlElement("citeproc")]
        public string Citeproc { get; set; }

        [JsonProperty("resourceType")]
        [XmlElement("resourceType")]
        public string ResourceType { get; set; }

        [Required]
        [JsonProperty("resourceTypeGeneral")]
        [XmlElement("resourceTypeGeneral")]
        public DataCiteResourceTypeGeneral ResourceTypeGeneral { get; set; }

        [JsonProperty("ris")]
        [XmlElement("ris")]
        public string Ris { get; set; }

        [JsonProperty("schemaOrg")]
        [XmlElement("schemaOrg")]
        public string SchemaOrg { get; set; }

        public DataCiteTypes()
        { }
    }
}