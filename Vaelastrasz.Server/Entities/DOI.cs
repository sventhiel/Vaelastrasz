using LiteDB;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Vaelastrasz.Server.Entities
{
    public class DOI
    {
        public long Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public DOIType Type { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public Status Status { get; set; }

        [BsonRef("users")]
        public User User { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DOIType
    {
        [EnumMember(Value = "DataCite")]
        DataCite = 0
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        [EnumMember(Value = "draft")]
        Draft = 0,

        [EnumMember(Value = "registered")]
        Registered = 1,

        [EnumMember(Value = "findable")]
        Findable = 2
    }
}