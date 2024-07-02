using LiteDB;
using Vaelastrasz.Library.Models;
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

        ~DOIService()
        {
            Dispose(false);
        }

        public long? Create(string prefix, string suffix, long userId, DOIStateType state = DOIStateType.Draft)
        {
            try
            {
                using var db = new LiteDatabase(_connectionString);
                var dois = db.GetCollection<DOI>("dois");
                var users = db.GetCollection<User>("users");

                var user = users.FindById(userId);

                if (user == null)
                    return null;

                var doi = new DOI()
                {
                    Prefix = prefix,
                    Suffix = suffix,
                    State = state,
                    User = user,
                    CreationDate = DateTimeOffset.UtcNow,
                    LastUpdateDate = DateTimeOffset.UtcNow
                };

                return dois.Insert(doi);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return col.Delete(id);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public List<DOI> Find()
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return col.Query().ToList();
        }

        public DOI? FindByDOI(string prefix, string suffix)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            var dois = col.Find(d => d.Prefix.Equals(prefix, StringComparison.InvariantCultureIgnoreCase) && d.Suffix.Equals(suffix, StringComparison.InvariantCultureIgnoreCase));

            if (dois.Count() != 1)
                return null;

            return dois.First();
        }

        public DOI? FindById(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return col.FindById(id);
        }

        public List<DOI> FindByPrefix(string prefix)
        {
            var dois = new List<DOI>();

            if (prefix == null)
                return dois;

            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return col.Find(d => d.Prefix.Equals(prefix, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public DOI? FindByPrefixAndSuffix(string prefix, string suffix)
        {
            if (prefix == null || suffix == null)
                return null;

            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            var dois = col.Include(d => d.User).Find(d => d.Prefix.Equals(prefix, StringComparison.OrdinalIgnoreCase) && d.Suffix.Equals(suffix, StringComparison.OrdinalIgnoreCase));

            if (dois.Count() != 1)
                return null;

            return ReadDOIModel.Convert(dois.First());
        }

        public List<DOI> FindBySuffix(string suffix)
        {
            var dois = new List<DOI>();

            if (suffix == null)
                return dois;

            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return col.Find(d => d.Suffix.Equals(suffix, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<DOI> FindByUserId(long userId)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return col.Find(d => d.User.Id == userId).ToList();
        }

        public bool Update(string prefix, string suffix, UpdateDOIModel model)
        {
            try
            {
                using var db = new LiteDatabase(_connectionString);
                var dois = db.GetCollection<DOI>("dois");
                var users = db.GetCollection<User>("users");

                var doi = FindByPrefixAndSuffix(prefix, suffix);

            }
            catch (Exception)
            {
                throw;
            }



            

            doi.State = model.State;

            if (model.UserId > 0)
                doi.User = users.FindById(model.UserId);

            doi.LastUpdateDate = DateTimeOffset.UtcNow;

            return dois.Update(doi);
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