using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vaelastrasz.Library.Models.ORCiD
{
    public class ORCiDDate
    {
        public ORCiDDate()
        { }

        [JsonProperty("value")]
        public long Value { get; set; }
    }
}
