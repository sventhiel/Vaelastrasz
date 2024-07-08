using LiteDB;
using Vaelastrasz.Library.Entities;
using Vaelastrasz.Library.Exceptions;
using Vaelastrasz.Library.Models;

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
            using var db = new LiteDatabase(_connectionString);
            var dois = db.GetCollection<DOI>("dois");
            var users = db.GetCollection<User>("users");

            var user = users.FindById(userId) ?? throw new NotFoundException($"The user (id:{userId}) does not exist.");

            if (!user.Account.Prefix.Equals(prefix, StringComparison.InvariantCultureIgnoreCase))
                throw new ForbidException();

            if (dois.Find(d => d.Prefix.Equals(prefix, StringComparison.InvariantCultureIgnoreCase) && d.Suffix.Equals(suffix, StringComparison.InvariantCultureIgnoreCase)).Count() > 0)
                throw new ConflictException($"The doi (prefix:{prefix}, suffix: {suffix}) already exists.");

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

        public bool DeleteById(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return col.Delete(id);
        }

        public bool DeleteByPrefixAndSuffix(string prefix, string suffix)
        {
            using var db = new LiteDatabase(_connectionString);
            var dois = db.GetCollection<DOI>("dois");
            var users = db.GetCollection<User>("users");

            var doi = dois.Find(d => d.Prefix.Equals(prefix, StringComparison.InvariantCultureIgnoreCase) && d.Suffix.Equals(suffix, StringComparison.InvariantCultureIgnoreCase));

            if (doi.Count() == 0)
                throw new NotFoundException($"The doi (prefix:{prefix}, suffix: {suffix}) does not exist.");

            if (doi.Count() > 1)
                throw new ConflictException($"The doi (prefix:{prefix}, suffix: {suffix}) exists more than once.");

            var user = users.FindById(doi.Single().User.Id) ?? throw new NotFoundException($"The user of doi (prefix:{prefix}, suffix: {suffix}) does not exist.");

            if (!user.Account.Prefix.Equals(prefix, StringComparison.InvariantCultureIgnoreCase))
                throw new ForbidException();

            return dois.Delete(doi.Single().Id);
        }

        public bool DeleteByDOI(string doi)
        {
            if (!doi.Contains('/'))
                throw new BadRequestException($"The value of doi ({doi}) is invalid.");

            string prefix = doi.Split('/')[0];
            string suffix = doi.Split('/')[1];

            return DeleteByPrefixAndSuffix(prefix, suffix);
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

            return col.FindAll().ToList();
        }

        public DOI FindByDOI(string doi)
        {
            if (!doi.Contains('/'))
                throw new BadRequestException($"The value of doi ({doi}) is invalid.");

            string prefix = doi.Split('/')[0];
            string suffix = doi.Split('/')[1];

            return FindByPrefixAndSuffix(prefix, suffix);
        }

        public DOI FindById(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            var doi = col.FindById(id);

            if (doi == null)
                throw new NotFoundException($"The doi (id:{id}) does not exist.");

            return doi;
        }

        public List<DOI> FindByPrefix(string prefix)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return col.Find(d => d.Prefix.Equals(prefix, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public DOI FindByPrefixAndSuffix(string prefix, string suffix)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            var dois = col.Find(d => d.Prefix.Equals(prefix, StringComparison.OrdinalIgnoreCase) && d.Suffix.Equals(suffix, StringComparison.OrdinalIgnoreCase));

            if (dois.Count() == 0)
                throw new NotFoundException($"The doi (prefix:{prefix}, suffix: {suffix}) does not exist.");

            if (dois.Count() > 1)
                throw new ConflictException($"The doi (prefix:{prefix}, suffix: {suffix}) exists more than once.");

            return dois.Single();
        }

        public List<DOI> FindBySuffix(string suffix)
        {
            if (suffix == null)
                throw new ArgumentNullException(nameof(suffix));

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

        public bool UpdateByPrefixAndSuffix(string prefix, string suffix, DOIStateType state, string value)
        {
            using var db = new LiteDatabase(_connectionString);
            var dois = db.GetCollection<DOI>("dois");
            var users = db.GetCollection<User>("users");

            var doi = FindByPrefixAndSuffix(prefix, suffix);

            if (doi == null)
                throw new NotFoundException($"The doi (doi:{prefix}/{suffix}) does not exist.");

            doi.State = state;
            doi.Value = value;
            doi.LastUpdateDate = DateTimeOffset.UtcNow;

            return dois.Update(doi);
        }

        public bool UpdateByDOI(string doi, DOIStateType state, string value)
        {
            if (!doi.Contains('/'))
                throw new BadRequestException($"The value of doi ({doi}) is invalid.");

            string prefix = doi.Split('/')[0];
            string suffix = doi.Split('/')[1];

            return UpdateByPrefixAndSuffix(prefix, suffix, state, value);
        }

        public bool UpdateById(long id, DOIStateType state, string value)
        {
            using var db = new LiteDatabase(_connectionString);
            var dois = db.GetCollection<DOI>("dois");

            var doi = dois.FindById(id);

            if (doi == null)
                throw new NotFoundException($"The doi (id:{id}) does not exist.");

            doi.State = state;
            doi.Value = value;
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