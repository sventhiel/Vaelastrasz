using LiteDB;
using Vaelastrasz.Server.Entities;

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

        public long Create(string expression, string regularExpression, long? userId)
        {
            using var db = new LiteDatabase(_connectionString);
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

        public List<Placeholder> Find()
        {
            List<Placeholder> placeholders = new();

            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<Placeholder>("placeholders");

            placeholders = col.Query().ToList();

            return placeholders;
        }

        public List<Placeholder> FindByUserId(long id)
        {
            List<Placeholder> placeholders = new();

            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<Placeholder>("placeholders");

            placeholders = col.Query().Where(p => p.User.Id == id).ToList();

            return placeholders;
        }

        public Placeholder? FindById(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<Placeholder>("placeholders");

            return col.FindById(id);
        }

        public bool Delete(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<Placeholder>("placeholders");

            return col.Delete(id);
        }

        public bool Update(long id, string expression, string regularExpression, long? userId)
        {
            using var db = new LiteDatabase(_connectionString);
            var placeholders = db.GetCollection<Placeholder>("placeholders");
            var users = db.GetCollection<User>("users");

            var placeholder = placeholders.FindById(id);

            if (placeholder == null)
                return false;

            if (!string.IsNullOrEmpty(expression))
                placeholder.Expression = expression;

            if (!string.IsNullOrEmpty(regularExpression))
                placeholder.RegularExpression = regularExpression;

            placeholder.User = users.FindById(userId);

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

        ~PlaceholderService()
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