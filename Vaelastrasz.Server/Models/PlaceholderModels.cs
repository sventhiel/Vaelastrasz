namespace Vaelastrasz.Server.Models
{
    public class CreatePlaceholderModel
    {
        public string Expression { get; set; }
        public string RegularExpression { get; set; }

        public long UserId { get; set; }
    }

    public class ReadPlaceholderModel
    {
        public long Id { get; set; }
        public string Expression { get; set; }
        public string RegularExpression { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
    }

    public class UpdatePlaceholderModel
    {
    }
}