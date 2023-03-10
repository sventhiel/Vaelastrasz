using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Vaelastrasz.Library.Models.DataCite
{
    public class DataCiteDate
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("dateType")]
        public DataCiteDateType DateType { get; set; }

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
}