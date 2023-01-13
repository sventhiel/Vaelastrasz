using LiteDB;
using Vaelastrasz.Server.Entities;
using Vaelastrasz.Server.Utilities;

namespace Vaelastrasz.Server.Services
{
    public class AccountService
    {
        private readonly ConnectionString _connectionString;

        public AccountService(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public long Create(string name, string password, string host, string prefix)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var accounts = db.GetCollection<Account>("accounts");

                var account = new Account()
                {
                    Name = name,
                    Password = password,
                    Host = host,
                    Prefix = prefix,
                    CreationDate = DateTimeOffset.UtcNow,
                    LastUpdateDate = DateTimeOffset.UtcNow
                };

                return accounts.Insert(account);
            }
        }

        public Account FindById(long id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Account>("accounts");

                return col.FindById(id);
            }
        }

        public List<Account> Find()
        {
            List<Account> accounts = new List<Account>();

            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Account>("accounts");

                accounts = col.Query().ToList();
            }

            return accounts;
        }

        public bool Delete(long id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Account>("accounts");

                return col.Delete(id);
            }
        }
    }
}
