using NameParser;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteCreatorModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("givenName")]
        public string GivenName { get; set; }

        [JsonPropertyName("familyName")]
        public string FamilyName { get; set; }

        [JsonPropertyName("nameType")]
        public DataCiteCreatorType NameType { get; set; }

        [JsonPropertyName("affiliation")]
        public List<DataCiteAffiliationModel> Affiliation { get; set; }

        [JsonPropertyName("nameIdentifiers")]
        public List<DataCiteNameIdentifierModel> NameIdentifiers { get; set; }

        public DataCiteCreatorModel()
        {
            Affiliation = new List<DataCiteAffiliationModel>();
            NameIdentifiers = new List<DataCiteNameIdentifierModel>();
        }

        public DataCiteCreatorModel(string name, DataCiteCreatorType type)
        {
            switch (type)
            {
                case DataCiteCreatorType.Personal:
                    var person = new HumanName(name);

                    //GivenName = name.Substring(0, name.IndexOf(" ")),
                    GivenName = (person.Middle.Length > 0) ? $"{person.First} {person.Middle}" : $"{person.First}";
                    //FamilyName = name.Substring(name.IndexOf(" ") + 1),
                    FamilyName = person.Last;
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

        public DataCiteCreatorModel(string firstname, string lastname)
        {
            GivenName = firstname;
            FamilyName = lastname;
            NameType = DataCiteCreatorType.Personal;
        }
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum DataCiteCreatorType
    {
        [JsonPropertyName("Personal")]
        Personal = 1,

        [JsonPropertyName("Organizational")]
        Organizational = 2
    }
}