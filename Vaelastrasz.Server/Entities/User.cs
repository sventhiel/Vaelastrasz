using LiteDB;

namespace Vaelastrasz.Server.Entities
{
    public class User : BaseEntity
    {
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