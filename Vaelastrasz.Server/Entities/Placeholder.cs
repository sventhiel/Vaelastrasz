using LiteDB;

namespace Vaelastrasz.Server.Entities
{
    public class Placeholder
    {
        public long Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public string Expression { get; set; }
        public string RegularExpression { get; set; }

        [BsonRef("users")]
        public User User { get; set; }
    }
}