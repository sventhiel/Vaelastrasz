using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nozdormu.Library.Models.DataCite
{
    public class DataCiteNameIdentifier
    {
        [JsonProperty("nameIdentifier")]
        public string NameIdentifier { get; set; }

        [JsonProperty("nameIdentifierScheme")]
        public string NameIdentifierScheme { get; set; }

        [JsonProperty("schemeUri")]
        public string SchemeUri { get; set; }
    }
}
