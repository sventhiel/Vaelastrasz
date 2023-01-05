using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nozdormu.Library.Models.DataCite
{
    public class DataCiteGeoLocation
    {
        [JsonProperty("geoLocationPoint")]
        public Dictionary<string, string> GeoLocationPoint { get; set; }

        [JsonProperty("geoLocationBox")]
        public Dictionary<string, string> GeoLocationBox { get; set; }

        [JsonProperty("geoLocationPlace")]
        public string geoLocationPlace { get; set; }
    }
}
