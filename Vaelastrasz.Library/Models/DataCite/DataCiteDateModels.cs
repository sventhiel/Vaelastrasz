using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteDate
    {
        public DataCiteDate()
        { }

        [Required]
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("dateInformation")]
        public string DateInformation { get; set; }

        [Required]
        [JsonProperty("dateType")]
        public DataCiteDateType DateType { get; set; }
    }
}