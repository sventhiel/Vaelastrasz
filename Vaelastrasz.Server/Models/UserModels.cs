using System.ComponentModel.DataAnnotations;
using Vaelastrasz.Server.Entities;

namespace Vaelastrasz.Server.Models
{
    public class LoginUserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ReadUserModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Project { get; set; }
        public string Pattern { get; set; }
        public long AccountId { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }

        public static ReadUserModel Convert(User user)
        {
            var u = new ReadUserModel()
            {
                Id = user.Id,
                Name = user.Name,
                Project = user.Project,
                Pattern = user.Pattern,
                CreationDate = user.CreationDate,
                LastUpdateDate = user.LastUpdateDate
            };

            if (user.Account != null)
                u.AccountId = user.Account.Id;

            return u;
        }
    }

    public class CreateUserModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public string Project { get; set; }

        [Required]
        public string Pattern { get; set; }

        public long AccountId { get; set; }
    }

    public class UpdateUserModel
    {
        public string Name { get; set; }

        public string Pattern { get; set; }

        public string Password { get; set; }

        public long AccountId { get; set; }

        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }

        public static UpdateUserModel Convert(User user)
        {
            var u = new UpdateUserModel()
            {
                Name = user.Name,
                Pattern = user.Pattern,
                CreationDate = user.CreationDate,
                LastUpdateDate = user.LastUpdateDate
            };

            if (user.Account != null)
                u.AccountId = user.Account.Id;

            return u;
        }
    }
}