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
        public DataCiteGeoLocationBox GeoLocationBox { get; set; }

        [JsonProperty("geoLocationPlace")]
        [XmlElement("geoLocationPlace")]
        public string geoLocationPlace { get; set; }

        [JsonProperty("geoLocationPoint")]
        [XmlElement("geoLocationPoint")]
        public DataCiteGeoLocationPoint GeoLocationPoint { get; set; }

        public DataCiteGeoLocation()
        { }
    }

    public class DataCiteGeoLocationBox
    {
        [JsonProperty("westBoundLongitude")]
        [XmlElement("westBoundLongitude")]
        public string WestBoundLongitude { get; set; }

        [JsonProperty("eastBoundLongitude")]
        [XmlElement("eastBoundLongitude")]
        public string EastBoundLongitude { get; set; }

        [JsonProperty("southBoundLatitude")]
        [XmlElement("southBoundLatitude")]
        public string SouthBoundLatitude { get; set; }

        [JsonProperty("northBoundLatitude")]
        [XmlElement("northBoundLatitude")]
        public string NorthBoundLatitude { get; set; }

        public DataCiteGeoLocationBox() { }
    }

    public class DataCiteGeoLocationPoint
    {
        [JsonProperty("pointLongitude")]
        [XmlElement("pointLongitude")]
        public string PointLongitude { get; set; }

        [JsonProperty("pointLatitude")]
        [XmlElement("pointLatitude")]
        public string PointLatitude { get; set; }

        public DataCiteGeoLocationPoint() { }
    }
}