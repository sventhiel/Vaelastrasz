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

        ~PlaceholderService()
        {
            Dispose(false);
        }

        public long? Create(string expression, string regularExpression, long userId)
        {
            try
            {
                using var db = new LiteDatabase(_connectionString);
                var placeholders = db.GetCollection<Placeholder>("placeholders");
                var users = db.GetCollection<User>("users");

                var user = users.FindById(userId);

                if (user == null)
                    return null;

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
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(long id)
        {
            try
            {
                using var db = new LiteDatabase(_connectionString);
                var col = db.GetCollection<Placeholder>("placeholders");

                return col.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public List<Placeholder> Find()
        {
            try
            {
                List<Placeholder> placeholders = new();

                using var db = new LiteDatabase(_connectionString);
                var col = db.GetCollection<Placeholder>("placeholders");

                placeholders = col.Query().ToList();

                return placeholders;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Placeholder? FindById(long id)
        {
            try
            {
                using var db = new LiteDatabase(_connectionString);
                var col = db.GetCollection<Placeholder>("placeholders");

                return col.FindById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Placeholder> FindByUserId(long userId)
        {
            try
            {
                List<Placeholder> placeholders = new();

                using var db = new LiteDatabase(_connectionString);
                var col = db.GetCollection<Placeholder>("placeholders");

                placeholders = col.Query().Where(p => p.User.Id == userId).ToList();

                return placeholders;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(long id, string expression, string regularExpression, long userId)
        {
            try
            {
                using var db = new LiteDatabase(_connectionString);
                var placeholders = db.GetCollection<Placeholder>("placeholders");
                var users = db.GetCollection<User>("users");

                var placeholder = placeholders.FindById(id);
                if (placeholder == null)
                    return false;

                var user = users.FindById(userId);
                if (user == null)
                    return false;

                placeholder.Expression = expression;
                placeholder.RegularExpression = regularExpression;
                placeholder.User = user;
                placeholder.LastUpdateDate = DateTimeOffset.UtcNow;

                return placeholders.Update(placeholder);
            }
            catch (Exception)
            {
                throw;
            }
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