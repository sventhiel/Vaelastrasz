using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteContributor
    {
        public DataCiteContributor()
        {
            Affiliations = new List<DataCiteAffiliation>();
            NameIdentifiers = new List<DataCiteNameIdentifier>();
        }

        [JsonProperty("affiliations")]
        public List<DataCiteAffiliation> Affiliations { get; set; }

        [JsonProperty("contributorType")]
        public DataCiteContributorType ContributorType { get; set; }

        [JsonProperty("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameIdentifiers")]
        public List<DataCiteNameIdentifier> NameIdentifiers { get; set; }

        [JsonProperty("nameType")]
        public DataCiteNameType? NameType { get; set; }
    }
}