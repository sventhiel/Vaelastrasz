using LiteDB;
using Vaelastrasz.Server.Entities;

namespace Vaelastrasz.Server.Services
{
    public class AccountService : IDisposable
    {
        private readonly ConnectionString _connectionString;
        private bool disposed = false;

        public AccountService(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        ~AccountService()
        {
            Dispose(false);
        }

        public long Create(string name, string password, string host, string prefix)
        {
            using var db = new LiteDatabase(_connectionString);
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

        public bool Delete(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<Account>("accounts");

            return col.Delete(id);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public List<Account> Find()
        {
            var accounts = new List<Account>();

            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<Account>("accounts");

            accounts = col.Query().ToList();

            return accounts;
        }

        public Account FindById(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<Account>("accounts");

            return col.FindById(id);
        }

        public bool Update(long id, string name, string password, string host, string prefix)
        {
            using var db = new LiteDatabase(_connectionString);
            var accounts = db.GetCollection<Account>("accounts");

            var account = accounts.FindById(id);

            if (account == null)
                return false;

            if (!string.IsNullOrEmpty(name))
                account.Name = name;

            if (!string.IsNullOrEmpty(password))
                account.Password = password;

            if (!string.IsNullOrEmpty(host))
                account.Host = host;

            if (!string.IsNullOrEmpty(prefix))
                account.Prefix = prefix;

            account.LastUpdateDate = DateTimeOffset.UtcNow;

            return accounts.Update(account);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // dispose-only, i.e. non-finalizable logic
                }

                // shared cleanup logic
                disposed = true;
            }
        }
    }
}