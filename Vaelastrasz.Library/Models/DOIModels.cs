using System;

namespace Vaelastrasz.Library.Models
{
    public class CreateDOIModel
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public long UserId { get; set; }
    }

    public class ReadDOIModel
    {
        public DateTimeOffset CreateCreationDate { get; set; }
        public long Id { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public long UserId { get; set; }
    }

    public class UpdateDOIModel
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public long UserId { get; set; }
    }
}