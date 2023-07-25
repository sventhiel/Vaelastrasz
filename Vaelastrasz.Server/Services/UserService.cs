﻿using LiteDB;
using Vaelastrasz.Server.Entities;
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

        public long? Create(string name, string password, string project, string pattern, long? accountId)
        {
            using var db = new LiteDatabase(_connectionString);
            var users = db.GetCollection<User>("users");
            var accounts = db.GetCollection<Account>("accounts");

            if (users.Exists(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                return null;

            // salt
            var salt = CryptographyUtils.GetRandomBase64String(16);

            var user = new User()
            {
                Name = name,
                Salt = salt,
                Password = CryptographyUtils.GetSHA512HashAsBase64(salt, password),
                Project = project,
                Pattern = pattern,
                CreationDate = DateTime.UtcNow,
                LastUpdateDate = DateTime.UtcNow
            };

            if (accountId != null)
                user.Account = accounts.FindById(accountId);

            return users.Insert(user);
        }

        public bool Verify(string name, string password)
        {
            using var db = new LiteDatabase(_connectionString);
            var users = db.GetCollection<User>("users");

            var user = users.FindOne(u => u.Name == name);

            if (user == null)
                return false;

            return (user.Password == CryptographyUtils.GetSHA512HashAsBase64(user.Salt, password));
        }

        public bool Delete(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<User>("users");

            return col.Delete(id);
        }

        public User? FindById(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<User>("users");

            return col.Include(u => u.Account).FindById(id);
        }

        public User? FindByName(string name)
        {
            if (name == null)
                return null;

            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<User>("users");

            var users = col.Include(u => u.Account).Find(u => u.Name.Equals(name));

            if (users.Count() != 1)
                return null;

            return users.First();
        }

        public List<User> Find()
        {
            List<User> users = new List<User>();

            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<User>("users");
            users = col.Query().ToList();

            return users;
        }

        public bool Update(long id, string name, string password, string pattern, long? accountId)
        {
            using var db = new LiteDatabase(_connectionString);
            var users = db.GetCollection<User>("users");

            var user = users.Include(u => u.Account).FindById(id);

            if (user == null)
                return false;

            if (!string.IsNullOrEmpty(name))
                user.Name = name;

            if (!string.IsNullOrEmpty(pattern))
                user.Pattern = pattern;

            if (!string.IsNullOrEmpty(password))
            {
                var salt = CryptographyUtils.GetRandomBase64String(16);
                user.Salt = salt;
                user.Password = CryptographyUtils.GetSHA512HashAsBase64(salt, password);
            }

            user.LastUpdateDate = DateTimeOffset.UtcNow;

            return users.Update(user);
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

        ~UserService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}