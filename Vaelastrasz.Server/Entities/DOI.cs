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
        public string Prefix { get; set; }
        public string Suffix { get; set; }

        [BsonRef("users")]
        public User User { get; set; }

        public DOI()
        {
            CreationDate = DateTimeOffset.UtcNow;
            LastUpdateDate = DateTimeOffset.UtcNow;
        }
    }
}