using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nozdormu.Library.Models.DataCite
{
    public class DataCiteRight
    {
        [JsonProperty("rights")]
        public string Rights { get; set; }

        [JsonProperty("rightsUri")]
        public string RightsUri { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }
    }
}
