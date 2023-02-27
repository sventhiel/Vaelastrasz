using LiteDB;
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

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum DOIType
    {
        [JsonPropertyName("DataCite")]
        DataCite = 0
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum Status
    {
        [JsonPropertyName("draft")]
        Draft = 0,

        [JsonPropertyName("registered")]
        Registered = 1,

        [JsonPropertyName("findable")]
        Findable = 2
    }
}