using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    /// <summary>
    /// [TODO] Not implemented yet!
    /// </summary>
    public class DataCiteGeoLocation
    {
        [JsonProperty("geoLocationBox")]
        [XmlElement("geoLocationBox")]
        public Dictionary<string, string> GeoLocationBox { get; set; }

        [JsonProperty("geoLocationPlace")]
        [XmlElement("geoLocationPlace")]
        public string geoLocationPlace { get; set; }

        [JsonProperty("geoLocationPoint")]
        [XmlElement("geoLocationPoint")]
        public Dictionary<string, string> GeoLocationPoint { get; set; }

        public DataCiteGeoLocation()
        { }
    }
}