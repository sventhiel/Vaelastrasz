using Newtonsoft.Json;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteRelatedItemCreator
    {
        public DataCiteRelatedItemCreator()
        { }

        [JsonProperty("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameType")]
        public DataCiteNameType NameType { get; set; }
    }
}