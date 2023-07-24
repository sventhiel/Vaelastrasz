using LiteDB;
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
        public State State { get; set; }

        [BsonRef("users")]
        public User User { get; set; }

        public DOI()
        {
            CreationDate = DateTimeOffset.UtcNow;
            LastUpdateDate = DateTimeOffset.UtcNow;
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DOIType
    {
        [EnumMember(Value = "DataCite")]
        DataCite = 0
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum State
    {
        [EnumMember(Value = "findable")]
        Findable = 1,

        [EnumMember(Value = "registered")]
        Registered = 2,

        [EnumMember(Value = "draft")]
        Draft = 3
    }
}