using System;

namespace Vaelastrasz.Library.Entities
{
    public class Account
    {
        public Account()
        {
            CreationDate = DateTimeOffset.UtcNow;
            LastUpdateDate = DateTimeOffset.UtcNow;
        }

        public DateTimeOffset CreationDate { get; set; }
        public string Host { get; set; }
        public long Id { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Prefix { get; set; }
    }
}