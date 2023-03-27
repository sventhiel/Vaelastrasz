using Vaelastrasz.Server.Entities;

namespace Vaelastrasz.Server.Models
{
    public class CreatePlaceholderModel
    {
        public string Expression { get; set; }
        public string RegularExpression { get; set; }
    }

    public class ReadPlaceholderModel
    {
        public long Id { get; set; }
        public string Expression { get; set; }
        public string RegularExpression { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }

        public static ReadPlaceholderModel Convert(Placeholder placeholder)
        {
            return new ReadPlaceholderModel()
            {
                Id = placeholder.Id,
                Expression = placeholder.Expression,
                RegularExpression = placeholder.RegularExpression,
                CreationDate = placeholder.CreationDate,
                LastUpdateDate = placeholder.LastUpdateDate
            };
        }
    }

    public class UpdatePlaceholderModel
    {
    }
}