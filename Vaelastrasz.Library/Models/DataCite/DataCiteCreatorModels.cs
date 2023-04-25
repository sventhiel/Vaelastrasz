
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteCreator
    {
        [JsonProperty("name")]
        [XmlElement("name")]
        public string Name { get; set; }

        [JsonProperty("givenName")]
        [XmlElement("givenName")]
        public string GivenName { get; set; }

        [JsonProperty("familyName")]
        [XmlElement("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("nameType")]
        [XmlElement("nameType")]
        public DataCiteCreatorType NameType { get; set; }

        [JsonProperty("affiliations")]
        [XmlElement("affiliations")]
        public List<DataCiteAffiliation> Affiliations { get; set; }

        [JsonProperty("nameIdentifiers")]
        [XmlElement("nameIdentifiers")]
        public List<DataCiteNameIdentifier> NameIdentifiers { get; set; }

        public DataCiteCreator()
        {
            Affiliations = new List<DataCiteAffiliation>();
            NameIdentifiers = new List<DataCiteNameIdentifier>();
        }

        public DataCiteCreator(string name, DataCiteCreatorType type)
        {
            switch (type)
            {
                case DataCiteCreatorType.Personal:
                    //var person = new HumanName(name);

                    GivenName = name.Substring(0, name.IndexOf(" "));
                    //GivenName = (person.Middle.Length > 0) ? $"{person.First} {person.Middle}" : $"{person.First}";
                    FamilyName = name.Substring(name.IndexOf(" ") + 1);
                    //FamilyName = person.Last;
                    Name = $"{GivenName} {FamilyName}";
                    NameType = type;
                    break;

                case DataCiteCreatorType.Organizational:
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
            NameType = DataCiteCreatorType.Personal;
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteCreatorType
    {
        [EnumMember(Value = "Personal")]
        Personal = 1,

        [EnumMember(Value = "Organizational")]
        Organizational = 2
    }
}