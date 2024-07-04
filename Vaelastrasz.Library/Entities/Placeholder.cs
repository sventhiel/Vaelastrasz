using LiteDB;
using System;

namespace Vaelastrasz.Library.Entities
{
    public class Placeholder
    {
        public Placeholder()
        {
            CreationDate = DateTimeOffset.UtcNow;
            LastUpdateDate = DateTimeOffset.UtcNow;
        }

        public DateTimeOffset CreationDate { get; set; }
        public string Expression { get; set; }
        public long Id { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public string RegularExpression { get; set; }

        [BsonRef("users")]
        public User User { get; set; }
    }
}