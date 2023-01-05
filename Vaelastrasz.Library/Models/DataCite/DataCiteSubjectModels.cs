using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nozdormu.Library.Models.DataCite
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

        [JsonConstructor]
        public DataCiteSubject()
        { }

        public DataCiteSubject(string subject)
        {
            Subject = subject;
        }
    }
}
