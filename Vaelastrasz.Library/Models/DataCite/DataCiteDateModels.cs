using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteDate
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("dateType")]
        public DataCiteDateType DateType { get; set; }

        [Newtonsoft.Json.JsonConstructor]
        public DataCiteDate()
        { }

        public DataCiteDate(string date, DataCiteDateType type)
        {
            Date = date;
            DateType = type;
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteDateType
    {
        Accepted = 1,
        Available = 2,
        Copyrighted = 3,
        Collected = 4,
        Created = 5,
        Issued = 6,
        Submitted = 7,
        Updated = 8,
        Valid = 9,
        Withdrawn = 10,
        Other = 11
    }
}