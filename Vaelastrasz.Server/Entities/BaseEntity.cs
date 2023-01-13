namespace Vaelastrasz.Server.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
    }
}
