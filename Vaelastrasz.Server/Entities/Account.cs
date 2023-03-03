namespace Vaelastrasz.Server.Entities
{
    public class Account
    {
        public long Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public string Host { get; set; }
        public string Prefix { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public Account()
        {
            CreationDate = DateTimeOffset.UtcNow;
            LastUpdateDate = DateTimeOffset.UtcNow;
        }
    }
}