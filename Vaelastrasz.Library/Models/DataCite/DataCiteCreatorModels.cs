using NameParser;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteCreator
    {
        public DataCiteCreator()
        {
            Affiliations = new List<DataCiteAffiliation>();
            NameIdentifiers = new List<DataCiteNameIdentifier>();
        }

        [JsonProperty("affiliations")]
        [XmlElement("affiliations")]
        public List<DataCiteAffiliation> Affiliations { get; set; }

        [JsonProperty("familyName")]
        [XmlElement("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("givenName")]
        [XmlElement("givenName")]
        public string GivenName { get; set; }

        [JsonProperty("lang")]
        [XmlElement("lang")]
        public string Lang { get; set; }

        [Required]
        [JsonProperty("name")]
        [XmlElement("name")]
        public string Name { get; set; }

        [JsonProperty("nameIdentifiers")]
        [XmlElement("nameIdentifiers")]
        public List<DataCiteNameIdentifier> NameIdentifiers { get; set; }

        [JsonProperty("nameType")]
        [XmlElement("nameType")]
        public DataCiteNameType NameType { get; set; }
    }
}