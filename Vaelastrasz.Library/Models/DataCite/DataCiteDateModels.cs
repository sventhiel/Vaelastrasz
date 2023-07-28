using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataCiteDateType
    {
        [EnumMember(Value = "Accepted")]
        Accepted = 1,

        [EnumMember(Value = "Available")]
        Available = 2,

        [EnumMember(Value = "Copyrighted")]
        Copyrighted = 3,

        [EnumMember(Value = "Collected")]
        Collected = 4,

        [EnumMember(Value = "Created")]
        Created = 5,

        [EnumMember(Value = "Issued")]
        Issued = 6,

        [EnumMember(Value = "Submitted")]
        Submitted = 7,

        [EnumMember(Value = "Updated")]
        Updated = 8,

        [EnumMember(Value = "Valid")]
        Valid = 9,

        [EnumMember(Value = "Withdrawn")]
        Withdrawn = 10,

        [EnumMember(Value = "Other")]
        Other = 11
    }

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