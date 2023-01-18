using LiteDB;

namespace Vaelastrasz.Server.Entities
{
    public class Placeholder : BaseEntity
    {
        public string Expression { get; set; }
        public string RegularExpression { get; set; }

        [BsonRef("users")]
        public User User { get; set; }
    }
}