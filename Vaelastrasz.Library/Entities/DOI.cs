using LiteDB;
using System;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Entities
{
    public class DOI
    {
        public DOI()
        {
            CreationDate = DateTimeOffset.UtcNow;
            LastUpdateDate = DateTimeOffset.UtcNow;
        }

        public DateTimeOffset CreationDate { get; set; }
        public long Id { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public string Prefix { get; set; }
        public DOIStateType State { get; set; }
        public string Suffix { get; set; }

        [BsonRef("users")]
        public User User { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Prefix}/{Suffix}";
        }
    }
}