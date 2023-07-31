namespace Vaelastrasz.Server.Models
{
    public class CreateDOIModel
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public long UserId { get; set; }
    }

    public class ReadDOIModel
    {
        public long Id { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public long UserId { get; set; }
        public DateTimeOffset CreateCreationDate { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
    }
}
