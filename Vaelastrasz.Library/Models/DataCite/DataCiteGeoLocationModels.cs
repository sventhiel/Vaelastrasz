﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteGeoLocationModel
    {
        [JsonPropertyName("geoLocationPoint")]
        public Dictionary<string, string> GeoLocationPoint { get; set; }

        [JsonPropertyName("geoLocationBox")]
        public Dictionary<string, string> GeoLocationBox { get; set; }

        [JsonPropertyName("geoLocationPlace")]
        public string GeoLocationPlace { get; set; }
    }
}