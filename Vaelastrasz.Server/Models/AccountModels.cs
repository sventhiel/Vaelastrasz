using System.ComponentModel.DataAnnotations;

namespace Vaelastrasz.Server.Models
{
    public class CreateAccountModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Host { get; set; }

        [Required]
        public string Prefix { get; set; }
    }

    public class UpdateAccountModel
    {

    }

    public class ReadAccountModel
    {

    }
}
