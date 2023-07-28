using LiteDB;

namespace Vaelastrasz.Server.Entities
{
    public class User
    {
        public User()
        {
            CreationDate = DateTimeOffset.UtcNow;
            LastUpdateDate = DateTimeOffset.UtcNow;
        }

        [BsonRef("accounts")]
        public Account Account { get; set; }

        public DateTimeOffset CreationDate { get; set; }
        public long Id { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Pattern { get; set; }
        public string Project { get; set; }
        public string Salt { get; set; }
    }
}