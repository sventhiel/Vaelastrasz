using Newtonsoft.Json;
using System.Xml.Serialization;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteDate
    {
        public DataCiteDate()
        { }

        public DataCiteDate(string date, DataCiteDateType type)
        {
            Date = date;
            DateType = type;
        }

        [JsonProperty("date")]
        [XmlElement("date")]
        public string Date { get; set; }

        [JsonProperty("dateType")]
        [XmlElement("dateType")]
        public DataCiteDateType DateType { get; set; }
    }
}