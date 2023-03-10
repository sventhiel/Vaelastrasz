using System.ComponentModel.DataAnnotations;
using Vaelastrasz.Server.Entities;

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
        public long Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Prefix { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }

        public static UpdateAccountModel Convert(Account account)
        {
            return new UpdateAccountModel
            {
                Id = account.Id,
                Name = account.Name,
                Password = account.Password,
                Host = account.Host,
                Prefix = account.Prefix,
                CreationDate = account.CreationDate,
                LastUpdateDate = account.LastUpdateDate
            };
        }
    }

    public class ReadAccountModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Prefix { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }

        public static ReadAccountModel Convert(Account account)
        {
            return new ReadAccountModel()
            {
                Id = account.Id,
                Name = account.Name,
                Password = account.Password,
                Host = account.Host,
                Prefix = account.Prefix,
                CreationDate = account.CreationDate,
                LastUpdateDate = account.LastUpdateDate
            };
        }
    }
}