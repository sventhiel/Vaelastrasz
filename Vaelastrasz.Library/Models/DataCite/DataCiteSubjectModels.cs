using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteSubject
    {
        public DataCiteSubject()
        { }

        [JsonProperty("lang")]
        public string Language { get; set; }

        [JsonProperty("schemeUri")]
        public string SchemeUri { get; set; }

        [Required]
        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("subjectScheme")]
        public string SubjectScheme { get; set; }

        [JsonProperty("valueUri")]
        public string ValueUri { get; set; }
    }
}