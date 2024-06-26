﻿using LiteDB;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Vaelastrasz.Server.Entities;
using Vaelastrasz.Server.Models;
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

        public long? Create(string name, string password, string project, string pattern, long accountId, bool isActive)
        {
            using var db = new LiteDatabase(_connectionString);
            var users = db.GetCollection<User>("users");
            var accounts = db.GetCollection<Account>("accounts");

            if (users.Exists(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                return null;

            var account = accounts.FindById(accountId);

            if(account == null)
                return null;

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

        public bool Delete(long id)
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
            try
            {
                List<User> users = new List<User>();

                using var db = new LiteDatabase(_connectionString);
                var col = db.GetCollection<User>("users");
                users = col.Include(u => u.Account).Query().ToList();

                return users.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User? FindById(long id)
        {
            try
            {
                using var db = new LiteDatabase(_connectionString);
                var col = db.GetCollection<User>("users");
                var user = col.Include(u => u.Account).FindById(id);

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User? FindByName(string? name)
        {
            try
            {
                if (name == null)
                    return null;

                using var db = new LiteDatabase(_connectionString);
                var col = db.GetCollection<User>("users");

                var users = col.Include(u => u.Account).Find(u => u.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

                if (users.Count() != 1)
                    return null;

                return users.First();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(long id, string password, string project, string pattern, long? accountId, bool isActive)
        {
            try
            {
                using var db = new LiteDatabase(_connectionString);
                var users = db.GetCollection<User>("users");
                var accounts = db.GetCollection<Account>("accounts");

                var user = users.Include(u => u.Account).FindById(id);
                if (user == null)
                    return false;

                user.Account = accountId.HasValue ? accounts.FindById(accountId) : null;

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
            catch (Exception)
            {
                throw;
            }
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