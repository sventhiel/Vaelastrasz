using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vaelastrasz.Library.Models.ORCiD
{
    public class ORCiDPreferences
    {
        public ORCiDPreferences()
        { }

        [JsonProperty("locale")]
        public string Locale { get; set; }
    }
}
