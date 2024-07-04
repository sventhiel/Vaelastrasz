using LiteDB;
using Vaelastrasz.Library.Entities;
using Vaelastrasz.Library.Exceptions;

namespace Vaelastrasz.Server.Services
{
    public class PlaceholderService : IDisposable
    {
        private readonly ConnectionString _connectionString;
        private bool disposed = false;

        public PlaceholderService(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        ~PlaceholderService()
        {
            Dispose(false);
        }

        public long Create(string expression, string regularExpression, long userId)
        {
            using var db = new LiteDatabase(_connectionString);
            var placeholders = db.GetCollection<Placeholder>("placeholders");
            var users = db.GetCollection<User>("users");

            var user = users.FindById(userId);

            if (user == null)
                throw new NotFoundException($"The user (id:{userId}) does not exist.");

            var placeholder = new Placeholder()
            {
                Expression = expression,
                RegularExpression = regularExpression,
                User = user,
                CreationDate = DateTimeOffset.UtcNow,
                LastUpdateDate = DateTimeOffset.UtcNow
            };

            return placeholders.Insert(placeholder);
        }

        public bool Delete(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<Placeholder>("placeholders");

            return col.Delete(id);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public List<Placeholder> Find()
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<Placeholder>("placeholders");

            return col.Query().ToList();
        }

        public Placeholder FindById(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<Placeholder>("placeholders");

            var placeholder = col.FindById(id);

            if (placeholder == null)
                throw new NotFoundException($"The placeholder (id:{id}) does not exist.");

            return placeholder;
        }

        public List<Placeholder> FindByUserId(long userId)
        {
            List<Placeholder> placeholders = new();

            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<Placeholder>("placeholders");

            placeholders = col.Query().Where(p => p.User.Id == userId).ToList();

            return placeholders;
        }

        public bool Update(long id, string expression, string regularExpression, long userId)
        {
            using var db = new LiteDatabase(_connectionString);
            var placeholders = db.GetCollection<Placeholder>("placeholders");
            var users = db.GetCollection<User>("users");

            var placeholder = placeholders.FindById(id);
            if (placeholder == null)
                throw new NotFoundException($"The account (id:{id}) does not exist.");

            var user = users.FindById(userId);
            if (user == null)
                throw new NotFoundException($"The user (id:{userId}) does not exist.");

            placeholder.Expression = expression;
            placeholder.RegularExpression = regularExpression;
            placeholder.User = user;
            placeholder.LastUpdateDate = DateTimeOffset.UtcNow;

            return placeholders.Update(placeholder);
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