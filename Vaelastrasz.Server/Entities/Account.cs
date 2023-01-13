namespace Vaelastrasz.Server.Entities
{
    public class Account : BaseEntity
    {
        public string Host { get; set; }
        public string Prefix { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}