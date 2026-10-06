using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vaelastrasz.Library.Models.ORCiD
{
    public class ORCiDPerson
    {
        [JsonProperty("last-modified-date")]
        public ORCiDDate LastModifiedDate { get; set; }

        [JsonProperty("name")]
        public ORCiDName Name { get; set; }
    }
}
