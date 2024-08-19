using Newtonsoft.Json;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteRelatedItemContributor
    {
        public DataCiteRelatedItemContributor()
        { }

        [JsonProperty("contributorType")]
        public DataCiteContributorType ContributorType { get; set; }

        [JsonProperty("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameType")]
        public DataCiteNameType? NameType { get; set; }
    }
}