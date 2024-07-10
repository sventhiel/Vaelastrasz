using Newtonsoft.Json;

namespace Vaelastrasz.Library.Models.DataCite
{
    /// <summary>
    /// [TODO] Not implemented yet!
    /// </summary>
    public class DataCiteGeoLocation
    {
        public DataCiteGeoLocation()
        { }

        [JsonProperty("geoLocationBox")]
        public DataCiteGeoLocationBox GeoLocationBox { get; set; }

        [JsonProperty("geoLocationPlace")]
        public string geoLocationPlace { get; set; }

        [JsonProperty("geoLocationPoint")]
        public DataCiteGeoLocationPoint GeoLocationPoint { get; set; }
    }

    public class DataCiteGeoLocationBox
    {
        public DataCiteGeoLocationBox()
        { }

        [JsonProperty("eastBoundLongitude")]
        public string EastBoundLongitude { get; set; }

        [JsonProperty("northBoundLatitude")]
        public string NorthBoundLatitude { get; set; }

        [JsonProperty("southBoundLatitude")]
        public string SouthBoundLatitude { get; set; }

        [JsonProperty("westBoundLongitude")]
        public string WestBoundLongitude { get; set; }
    }

    public class DataCiteGeoLocationPoint
    {
        public DataCiteGeoLocationPoint()
        { }

        [JsonProperty("pointLatitude")]
        public string PointLatitude { get; set; }

        [JsonProperty("pointLongitude")]
        public string PointLongitude { get; set; }
    }
}