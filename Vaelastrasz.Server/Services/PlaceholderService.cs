﻿using LiteDB;
using Vaelastrasz.Server.Entities;
using Vaelastrasz.Server.Utilities;

namespace Vaelastrasz.Server.Services
{
    public class PlaceholderService
    {
        private readonly ConnectionString _connectionString;

        public PlaceholderService(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public long Create(string expression, string regularExpression, long? userId)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var placeholders = db.GetCollection<Placeholder>("placeholders");
                var users = db.GetCollection<User>("users");

                var placeholder = new Placeholder()
                {
                    Expression = expression,
                    RegularExpression = regularExpression,

                    CreationDate = DateTimeOffset.UtcNow,
                    LastUpdateDate = DateTimeOffset.UtcNow
                };

                if (userId != null)
                    placeholder.User = users.FindById(userId);

                return placeholders.Insert(placeholder);
            }
        }

        public List<Placeholder> Find()
        {
            List<Placeholder> placeholders = new List<Placeholder>();

            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Placeholder>("placeholders");

                placeholders = col.Query().ToList();
            }

            return placeholders;
        }

        public List<Placeholder> FindByUserId(long id)
        {
            List<Placeholder> placeholders = new List<Placeholder>();

            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Placeholder>("placeholders");


                placeholders = col.Query().Where(p => p.User.Id == id).ToList();
            }

            return placeholders;
        }

        public Placeholder FindById(long id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Placeholder>("placeholders");

                return col.FindById(id);
            }
        }

        public bool Delete(long id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Placeholder>("placeholders");

                return col.Delete(id);
            }
        }

        public bool Update(Placeholder placeholder)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<Placeholder>("placeholders");

                var p = col.FindById(placeholder.Id);

                if (p == null)
                    return false;

                p.Expression = placeholder.Expression;
                p.RegularExpression = placeholder.RegularExpression;
                p.User = placeholder.User;
                p.LastUpdateDate = DateTimeOffset.UtcNow;

                return col.Update(p);
            }
        }
    }
}
