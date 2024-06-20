using Newtonsoft.Json;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;

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

        [JsonProperty("resourceTypeGeneral")]
        [XmlElement("resourceTypeGeneral")]
        public DataCiteResourceTypeGeneral ResourceTypeGeneral { get; set; }

        [JsonProperty("ris")]
        [XmlElement("ris")]
        public string Ris { get; set; }

        [JsonProperty("schemaOrg")]
        [XmlElement("schemaOrg")]
        public string SchemaOrg { get; set; }

        public DataCiteTypes(string resourceType, string schemaOrg, string bibtex, string citeproc, string ris, DataCiteResourceTypeGeneral resourceTypeGeneral = DataCiteResourceTypeGeneral.Other) 
        {
            ResourceTypeGeneral = resourceTypeGeneral;

            if (!string.IsNullOrEmpty(resourceType))
                ResourceType = resourceType;
            
            if(!string.IsNullOrEmpty(schemaOrg))
                SchemaOrg = schemaOrg;

            if (!string.IsNullOrEmpty(bibtex))
                Bibtex = bibtex;

            if (!string.IsNullOrEmpty(citeproc))
                Citeproc = citeproc;

            if (!string.IsNullOrEmpty(ris))
                Ris = ris;
        }

        public DataCiteTypes()
        {
            ResourceTypeGeneral = DataCiteResourceTypeGeneral.Dataset;
        }
    }
}