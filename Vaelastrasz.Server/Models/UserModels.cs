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
        public string Pattern { get; set; }
        public long? AccountId { get; set; }

        public static ReadUserModel Convert(User user)
        {
            return new ReadUserModel()
            {
                Id = user.Id,
                Name = user.Name,
                Pattern = user.Pattern,
                AccountId = user.Account?.Id
            };
        }
    }

    public class CreateUserModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required, Compare("Password")]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Pattern")]
        public string Pattern { get; set; }

        [Required]
        [Display(Name = "AccountId")]
        public long? AccountId { get; set; }
    }
}