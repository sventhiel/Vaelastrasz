using NameParser;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;
using System.ComponentModel.DataAnnotations;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteCreator
    {
        public DataCiteCreator()
        {
            Affiliations = new List<DataCiteAffiliation>();
            NameIdentifiers = new List<DataCiteNameIdentifier>();
        }

        public DataCiteCreator(string name, DataCiteNameType type)
        {
            switch (type)
            {
                case DataCiteNameType.Personal:
                    var person = new HumanName(name);

                    //GivenName = name.Substring(0, name.IndexOf(" "));
                    GivenName = (person.Middle.Length > 0) ? $"{person.First} {person.Middle}" : $"{person.First}";
                    //FamilyName = name.Substring(name.IndexOf(" ") + 1);
                    FamilyName = person.Last;
                    Name = $"{GivenName} {FamilyName}";
                    NameType = type;
                    break;

                case DataCiteNameType.Organizational:
                    Name = name;
                    NameType = type;
                    break;

                default:
                    Name = name;
                    break;
            }
        }

        public DataCiteCreator(string firstname, string lastname)
        {
            GivenName = firstname;
            FamilyName = lastname;
            NameType = DataCiteNameType.Personal;
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