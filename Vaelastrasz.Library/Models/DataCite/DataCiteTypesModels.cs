using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteTypes
    {
        public DataCiteTypes()
        { }

        [JsonProperty("bibtex")]
        public string Bibtex { get; set; }

        [JsonProperty("citeproc")]
        public string Citeproc { get; set; }

        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [Required]
        [JsonProperty("resourceTypeGeneral")]
        public DataCiteResourceTypeGeneral ResourceTypeGeneral { get; set; }

        [JsonProperty("ris")]
        public string Ris { get; set; }

        [JsonProperty("schemaOrg")]
        public string SchemaOrg { get; set; }
    }
}