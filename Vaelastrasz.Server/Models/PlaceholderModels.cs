using Vaelastrasz.Library.Entities;

namespace Vaelastrasz.Server.Models
{
    public class CreatePlaceholderModel
    {
        public required string Expression { get; set; }
        public required string RegularExpression { get; set; }
        public required long UserId { get; set; }
    }

    public class ReadPlaceholderModel
    {
        public required DateTimeOffset CreationDate { get; set; }
        public required string Expression { get; set; }
        public required long Id { get; set; }
        public required DateTimeOffset LastUpdateDate { get; set; }
        public required string RegularExpression { get; set; }

        public required long UserId { get; set; }

        public static ReadPlaceholderModel Convert(Placeholder placeholder)
        {
            return new ReadPlaceholderModel()
            {
                Id = placeholder.Id,
                Expression = placeholder.Expression,
                RegularExpression = placeholder.RegularExpression,
                CreationDate = placeholder.CreationDate,
                LastUpdateDate = placeholder.LastUpdateDate,
                UserId = placeholder.User?.Id ?? 0
            };
        }
    }

    public class UpdatePlaceholderModel
    {
        public required string Expression { get; set; }
        public required string RegularExpression { get; set; }
        public required long UserId { get; set; }
    }
}