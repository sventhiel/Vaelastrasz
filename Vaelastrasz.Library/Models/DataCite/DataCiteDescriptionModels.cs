using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteDescription
    {
        public DataCiteDescription()
        { }

        [Required]
        [JsonProperty("description")]
        public string Description { get; set; }

        [Required]
        [JsonProperty("descriptionType")]
        public DataCiteDescriptionType DescriptionType { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }
    }
}