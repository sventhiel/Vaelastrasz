using LiteDB;

namespace Vaelastrasz.Server.Entities
{
    public class DOI : BaseEntity
    {
        public DOIType Type { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public Status Status { get; set; }

        [BsonRef("users")]
        public User User { get; set; }
    }

    public enum DOIType
    {
        DataCite = 0
    }

    public enum Status
    {
        Draft = 0,
        Registered = 1,
        Findable = 2
    }
}