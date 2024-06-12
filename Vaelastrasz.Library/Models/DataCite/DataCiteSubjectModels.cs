using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteSubject
    {
        [JsonProperty("lang")]
        [XmlElement("lang")]
        public string Language { get; set; }

        [JsonProperty("schemeUri")]
        [XmlElement("schemeUri")]
        public string SchemeUri { get; set; }

        [JsonProperty("subject")]
        [XmlElement("subject")]
        public string Subject { get; set; }

        [JsonProperty("subjectScheme")]
        [XmlElement("subjectScheme")]
        public string SubjectScheme { get; set; }

        [JsonProperty("valueUri")]
        [XmlElement("valueUri")]
        public string ValueUri { get; set; }

        public DataCiteSubject()
        {
            
        }
    }
}