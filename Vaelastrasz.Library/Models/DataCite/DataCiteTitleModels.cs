using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteTitle
    {
        public DataCiteTitle()
        {
        }

        [JsonProperty("lang")]
        public string Language { get; set; }

        [Required]
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("titleType")]
        public DataCiteTitleType? TitleType { get; set; }
    }
}