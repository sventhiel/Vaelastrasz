using Newtonsoft.Json;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteSubject
    {
        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("subjectScheme")]
        public string SubjectScheme { get; set; }

        [JsonProperty("schemeUri")]
        public string SchemeUri { get; set; }

        [JsonProperty("valueUri")]
        public string ValueUri { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }
    }
}