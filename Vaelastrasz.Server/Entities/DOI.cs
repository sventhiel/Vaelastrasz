using LiteDB;

namespace Vaelastrasz.Server.Entities
{
    public class DOI
    {
        public DOI()
        {
            CreationDate = DateTimeOffset.UtcNow;
            LastUpdateDate = DateTimeOffset.UtcNow;
        }

        public DateTimeOffset CreationDate { get; set; }
        public long Id { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }

        [BsonRef("users")]
        public User User { get; set; }
    }
}