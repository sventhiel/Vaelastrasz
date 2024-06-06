using System.ComponentModel.DataAnnotations;
using Vaelastrasz.Server.Entities;

namespace Vaelastrasz.Server.Models
{
    public class CreateUserModel
    {
        public long AccountId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Pattern { get; set; }

        public string Project { get; set; }
    }

    public class LoginUserModel
    {
        public string Password { get; set; }
        public string Username { get; set; }
    }

    public class ReadUserModel
    {
        public long AccountId { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public long Id { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public string Name { get; set; }
        public string Pattern { get; set; }

        public static ReadUserModel Convert(User user)
        {
            return new ReadUserModel()
            {
                Id = user.Id,
                Name = user.Name,
                Pattern = user.Pattern,
                CreationDate = user.CreationDate,
                LastUpdateDate = user.LastUpdateDate,
                AccountId = user.Account?.Id ?? 0
            };
        }
    }

    public class UpdateUserModel
    {
        public long? AccountId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Pattern { get; set; }
    }
}