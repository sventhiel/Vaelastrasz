using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteSubjectModel
    {
        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("subjectScheme")]
        public string SubjectScheme { get; set; }

        [JsonPropertyName("schemeUri")]
        public string SchemeUri { get; set; }

        [JsonPropertyName("valueUri")]
        public string ValueUri { get; set; }

        [JsonPropertyName("lang")]
        public string Language { get; set; }

        [JsonConstructor]
        public DataCiteSubjectModel() { }

        public DataCiteSubjectModel(string subject)
        {
            Subject = subject;
        }
    }
}