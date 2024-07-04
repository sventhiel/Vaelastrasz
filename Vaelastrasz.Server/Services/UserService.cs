using LiteDB;
using Vaelastrasz.Library.Entities;
using Vaelastrasz.Library.Exceptions;
using Vaelastrasz.Server.Utilities;

namespace Vaelastrasz.Server.Services
{
    public class UserService : IDisposable
    {
        private readonly ConnectionString _connectionString;
        private bool disposed = false;

        public UserService(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        ~UserService()
        {
            Dispose(false);
        }

        public long Create(string name, string password, string project, string pattern, long accountId, bool isActive)
        {
            using var db = new LiteDatabase(_connectionString);
            var users = db.GetCollection<User>("users");
            var accounts = db.GetCollection<Account>("accounts");

            if (users.Exists(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                throw new ConflictException($"The user (name:{name}) already exists.");

            var account = accounts.FindById(accountId);

            if (account == null)
                throw new NotFoundException($"The account (id:{accountId}) does not exist.");

            // salt
            var salt = CryptographyUtils.GetRandomBase64String(16);

            var user = new User
            {
                Name = name,
                Salt = salt,
                Password = CryptographyUtils.GetSHA512HashAsBase64(salt, password),
                Pattern = pattern,
                Project = project,
                IsActive = isActive,
                CreationDate = DateTime.UtcNow,
                LastUpdateDate = DateTime.UtcNow,
                Account = account
            };

            return users.Insert(user);
        }

        public bool DeleteById(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<User>("users");

            return col.Delete(id);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public List<User> Find()
        {
            List<User> users = new List<User>();

            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<User>("users");
            users = col.Query().ToList();

            return users.ToList();
        }

        public User FindById(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<User>("users");

            var user = col.FindById(id);

            return user ?? throw new NotFoundException($"The user (id:{id}) does not exist.");
        }

        public User FindByName(string name)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<User>("users");

            var users = col.Find(u => u.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            if (users.Count() == 0)
                throw new NotFoundException($"The user (name:{name}) does not exist.");

            if (users.Count() > 1)
                throw new ConflictException($"The user (name:{name}) exists more than once.");

            return users.Single();
        }

        public bool UpdateById(long id, string password, string project, string pattern, long accountId, bool isActive)
        {
            using var db = new LiteDatabase(_connectionString);
            var users = db.GetCollection<User>("users");
            var accounts = db.GetCollection<Account>("accounts");

            var user = users.FindById(id);
            if (user == null)
                throw new NotFoundException($"The user (id:{id}) does not exist.");

            var account = accounts.FindById(accountId);
            if (account == null)
                throw new NotFoundException($"The account (id:{accountId}) does not exist.");

            var salt = CryptographyUtils.GetRandomBase64String(16);

            user.Account = account;
            user.Pattern = pattern;
            user.Salt = salt;
            user.Password = CryptographyUtils.GetSHA512HashAsBase64(salt, password);
            user.LastUpdateDate = DateTimeOffset.UtcNow;

            return users.Update(user);
        }

        public bool Verify(string name, string password)
        {
            using var db = new LiteDatabase(_connectionString);
            var users = db.GetCollection<User>("users");

            var user = users.FindOne(u => u.Name == name);

            if (user == null)
                throw new NotFoundException($"The user (name:{name}) does not exist.");

            return (user.Password == CryptographyUtils.GetSHA512HashAsBase64(user.Salt, password));
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