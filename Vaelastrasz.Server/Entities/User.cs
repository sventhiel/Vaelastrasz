using LiteDB;

namespace Vaelastrasz.Server.Entities
{
    public class User
    {
        public long Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Pattern { get; set; }

        [BsonRef("accounts")]
        public Account Account { get; set; }

        public User()
        {
            Account = null;
        }
    }
}