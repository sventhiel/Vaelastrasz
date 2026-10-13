using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vaelastrasz.Library.Models.ORCiD
{
    public class ORCiDKeywords
    {
        [JsonProperty("last-modified-date")]
        public ORCiDDate LastModifiedDate { get; set; }


        [JsonProperty("keyword")]
        public List<string> Keyword { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
