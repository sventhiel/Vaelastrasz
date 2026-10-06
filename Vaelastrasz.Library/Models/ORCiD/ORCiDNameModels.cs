using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vaelastrasz.Library.Models.ORCiD
{
    public class ORCiDName
    {
        [JsonProperty("created-date")]
        public ORCiDDate CreatedDate { get; set; }

        [JsonProperty("last-modified-date")]
        public ORCiDDate LastModifiedDate { get; set; }
    }
}
