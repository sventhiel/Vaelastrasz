using LiteDB;
using Vaelastrasz.Library.Entities;
using Vaelastrasz.Library.Exceptions;

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

            if (accounts.Exists(u => u.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) && u.Host.Equals(host, StringComparison.InvariantCultureIgnoreCase)))
                throw new ConflictException($"The account (name:{name}, host: {host}) already exists.");

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

        public bool DeleteById(long id)
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
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<Account>("accounts");

            return col.Query().ToList();
        }

        public Account FindById(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<Account>("accounts");

            var account = col.FindById(id);

            if (account == null)
                throw new NotFoundException($"The account (id:{id}) does not exist.");

            return account;
        }

        public bool UpdateById(long id, string name, string password, string host, string prefix)
        {
            using var db = new LiteDatabase(_connectionString);
            var accounts = db.GetCollection<Account>("accounts");

            var account = accounts.FindById(id);

            if (account == null)
                throw new NotFoundException($"The account (id:{id}) does not exist.");

            account.Prefix = prefix;
            account.Name = name;
            account.Password = password;
            account.Host = host;
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