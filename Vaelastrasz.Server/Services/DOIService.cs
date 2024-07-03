using LiteDB;
using NameParser;
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

        public long Create(string prefix, string suffix, DOIStateType state, long userId, string value)
        {
            try
            {
                using var db = new LiteDatabase(_connectionString);
                var dois = db.GetCollection<DOI>("dois");
                var users = db.GetCollection<User>("users");

                var user = users.FindById(userId);

                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                var doi = new DOI()
                {
                    Prefix = prefix,
                    Suffix = suffix,
                    State = state,
                    User = user,
                    Value = value,
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
            try
            {
                using var db = new LiteDatabase(_connectionString);
                var col = db.GetCollection<DOI>("dois");

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

        public List<DOI> Find()
        {
            try
            {
                using var db = new LiteDatabase(_connectionString);
                var col = db.GetCollection<DOI>("dois");

                return col.FindAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DOI FindByDOI(string doi)
        {
            try
            {
                if(!doi.Contains('/'))
                {
                    throw new ArgumentException($"The value of doi ({doi}) is invalid.", nameof(doi));
                }

                string prefix = doi.Split('/')[0];
                string suffix = doi.Split('/')[1];

                return FindByPrefixAndSuffix(prefix, suffix);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DOI FindById(long id)
        {
            try
            {
                using var db = new LiteDatabase(_connectionString);
                var col = db.GetCollection<DOI>("dois");

                var doi = col.FindById(id);

                if(doi == null)
                    throw new ArgumentException($"The doi (id:{id}) does not exist.", nameof(id));

                return doi;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DOI> FindByPrefix(string prefix)
        {
            try
            {
                if (prefix == null)
                    throw new ArgumentNullException(nameof(prefix));               

                using var db = new LiteDatabase(_connectionString);
                var col = db.GetCollection<DOI>("dois");

                return col.Find(d => d.Prefix.Equals(prefix, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DOI FindByPrefixAndSuffix(string prefix, string suffix)
        {
            try
            {
                if (prefix == null)
                    throw new ArgumentNullException(nameof(prefix));

                if (suffix == null)
                    throw new ArgumentNullException(nameof(suffix));

                using var db = new LiteDatabase(_connectionString);
                var col = db.GetCollection<DOI>("dois");

                var dois = col.Find(d => d.Prefix.Equals(prefix, StringComparison.OrdinalIgnoreCase) && d.Suffix.Equals(suffix, StringComparison.OrdinalIgnoreCase));

                if (dois.Count() == 0)
                    throw new ArgumentException($"The doi (prefix:{prefix}, suffix: {suffix}) does not exist.");

                if (dois.Count() > 1)
                    throw new Exception($"The doi (prefix:{prefix}, suffix: {suffix}) exists more than once.");

                return dois.Single();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DOI> FindBySuffix(string suffix)
        {
            try
            {
                if (suffix == null)
                    throw new ArgumentNullException(nameof(suffix));

                using var db = new LiteDatabase(_connectionString);
                var col = db.GetCollection<DOI>("dois");

                return col.Find(d => d.Suffix.Equals(suffix, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DOI> FindByUserId(long userId)
        {
            try
            {
                using var db = new LiteDatabase(_connectionString);
                var col = db.GetCollection<DOI>("dois");

                return col.Find(d => d.User.Id == userId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(string prefix, string suffix, DOIStateType state, string value)
        {
            try
            {
                if (prefix == null)
                    throw new ArgumentNullException(nameof(prefix));

                if (suffix == null)
                    throw new ArgumentNullException(nameof(suffix));

                using var db = new LiteDatabase(_connectionString);
                var dois = db.GetCollection<DOI>("dois");
                var users = db.GetCollection<User>("users");

                var doi = FindByPrefixAndSuffix(prefix, suffix);

                if (doi == null)
                    throw new ArgumentException($"The doi (doi:{prefix}/{suffix}) does not exist.", nameof(doi));

                doi.State = state;
                doi.Value = value;
                doi.LastUpdateDate = DateTimeOffset.UtcNow;

                return dois.Update(doi);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(string doi, DOIStateType state, string value)
        {
            try
            {
                if (!doi.Contains('/'))
                {
                    throw new ArgumentException($"The value of doi ({doi}) is invalid.", nameof(doi));
                }

                string prefix = doi.Split('/')[0];
                string suffix = doi.Split('/')[1];

                return Update(prefix, suffix, state, value);
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