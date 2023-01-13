using LiteDB;
using Vaelastrasz.Server.Entities;
using Vaelastrasz.Server.Utilities;

namespace Vaelastrasz.Server.Services
{
    public class UserService
    {
        private readonly ConnectionString _connectionString;

        public UserService(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public long Create(string name, string password, string pattern, long? accountId)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var users = db.GetCollection<User>("users");
                var accounts = db.GetCollection<Account>("accounts");

                // salt
                var salt = CryptographyUtils.GetRandomBase64String(16);

                var user = new User()
                {
                    Name = name,
                    Salt = salt,
                    Password = CryptographyUtils.GetSHA512HashAsBase64(salt, password),
                    Pattern = pattern,
                    CreationDate= DateTime.UtcNow,
                    LastUpdateDate= DateTime.UtcNow
                };

                if (accountId != null)
                    user.Account = accounts.FindById(accountId);

                return users.Insert(user);
            }
        }

        public bool Verify(string name, string password)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var users = db.GetCollection<User>("users");

                var user = users.FindOne(u => u.Name == name);

                if (user == null)
                    return false;

                return (user.Password == CryptographyUtils.GetSHA512HashAsBase64(user.Salt, password));
            }
        }

        public bool Delete(long id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<User>("users");

                return col.Delete(id);
            }
        }

        public User FindById(long id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<User>("users");

                return col.FindById(id);
            }
        }

        public User? FindByName(string? name)
        {
            if (name == null)
                return null;

            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<User>("users");

                var users = col.Find(u => u.Name.Equals(name));

                if (users.Count() != 1)
                    return null;

                return users.First();
            }
        }

        public List<User> Find()
        {
            List<User> users = new List<User>();

            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<User>("users");

                users = col.Query().ToList();
            }

            return users;
        }

        public bool Update(User user)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<User>("users");

                var u = col.FindById(user.Id);

                if (u == null)
                    return false;

                u.Name = user.Name;
                u.Pattern = user.Pattern;
                u.Account = user.Account;
                u.LastUpdateDate = DateTimeOffset.UtcNow;

                return col.Update(u);
            }
        }
    }
}