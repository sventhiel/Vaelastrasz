using LiteDB;
using Vaelastrasz.Server.Entities;

namespace Vaelastrasz.Server.Services
{
    public class DOIService : IDisposable
    {
        private readonly ConnectionString _connectionString;
        private bool disposed = false;

        public DOIService(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public long? Create(string prefix, string suffix, long userId)
        {
            using var db = new LiteDatabase(_connectionString);
            var dois = db.GetCollection<DOI>("dois");
            var users = db.GetCollection<User>("users");

            var doi = new DOI()
            {
                Prefix = prefix,
                Suffix = suffix
            };

            if (users.FindById(userId) == null)
                return null;

            doi.User = users.FindById(userId);

            return dois.Insert(doi);
        }

        public bool Delete(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return col.Delete(id);
        }

        public DOI? FindById(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return col.FindById(id);
        }

        public DOI? FindByPrefixAndSuffix(string prefix, string suffix)
        {
            if (prefix == null || suffix == null)
                return null;

            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            var dois = col.Find(d => d.Prefix.Equals(prefix, StringComparison.OrdinalIgnoreCase) && d.Suffix.Equals(suffix, StringComparison.OrdinalIgnoreCase));

            if (dois.Count() != 1)
                return null;

            return dois.First();
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

        ~DOIService()
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