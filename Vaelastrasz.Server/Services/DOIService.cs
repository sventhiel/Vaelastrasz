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

        public async Task<long> CreateAsync(string prefix, string suffix, DOIStateType state, long userId, string value)
        {
            using var db = new LiteDatabase(_connectionString);
            var dois = db.GetCollection<DOI>("dois");
            var users = db.GetCollection<User>("users");

            var user = users.Include(u => u.Account).FindById(userId) ?? throw new NotFoundException($"The user (id:{userId}) does not exist.");

            if (!user.Account.Prefix.Equals(prefix, StringComparison.InvariantCultureIgnoreCase))
                throw new ForbiddenException($"The doi (prefix:{prefix}) is invalid.");

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

            return await Task.FromResult(dois.Insert(doi));
        }

        public async Task<bool> DeleteByDOIAsync(string doi)
        {
            if (!doi.Contains('/'))
                throw new BadRequestException($"The value of doi ({doi}) is invalid.");

            string prefix = doi.Split('/')[0];
            string suffix = doi.Split('/')[1];

            return await DeleteByPrefixAndSuffixAsync(prefix, suffix);
        }

        public async Task<bool> DeleteByIdAsync(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return await Task.FromResult(col.Delete(id));
        }

        public async Task<bool> DeleteByPrefixAndSuffixAsync(string prefix, string suffix)
        {
            using var db = new LiteDatabase(_connectionString);
            var dois = db.GetCollection<DOI>("dois");
            var users = db.GetCollection<User>("users");

            var doi = dois.Find(d => d.Prefix.Equals(prefix, StringComparison.OrdinalIgnoreCase) && d.Suffix.Equals(suffix, StringComparison.OrdinalIgnoreCase));

            if (doi == null || !doi.Any())
                throw new NotFoundException($"The doi (prefix:{prefix}, suffix: {suffix}) does not exist.");

            if (doi.Count() > 1)
                throw new ConflictException($"The doi (prefix:{prefix}, suffix: {suffix}) exists more than once.");

            var user = users.FindById(doi.Single().User.Id) ?? throw new NotFoundException($"The user of doi (prefix:{prefix}, suffix: {suffix}) does not exist.");

            if (!user.Account.Prefix.Equals(prefix, StringComparison.OrdinalIgnoreCase))
                throw new ForbiddenException($"The doi (prefix:{prefix}) is invalid.");

            return await Task.FromResult(dois.Delete(doi.Single().Id));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<List<DOI>> FindAsync()
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return await Task.FromResult(col.FindAll().ToList());
        }

        public async Task<DOI> FindByDOIAsync(string doi)
        {
            if (!doi.Contains('/'))
                throw new BadRequestException($"The value of doi ({doi}) is invalid.");

            string prefix = doi.Split('/')[0];
            string suffix = doi.Split('/')[1];

            return await FindByPrefixAndSuffixAsync(prefix, suffix);
        }

        public async Task<DOI> FindByIdAsync(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            var doi = col.FindById(id);

            return doi == null ? throw new NotFoundException($"The doi (id:{id}) does not exist.") : await Task.FromResult(doi);
        }

        public async Task<DOI> FindByPrefixAndSuffixAsync(string prefix, string suffix)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            var dois = col.Find(d => d.Prefix.Equals(prefix, StringComparison.OrdinalIgnoreCase) && d.Suffix.Equals(suffix, StringComparison.OrdinalIgnoreCase));

            if (dois == null || !dois.Any())
                throw new NotFoundException($"The doi (prefix:{prefix}, suffix: {suffix}) does not exist.");

            if (dois.Count() > 1)
                throw new ConflictException($"The doi (prefix:{prefix}, suffix: {suffix}) exists more than once.");

            return await Task.FromResult(dois.Single());
        }

        public async Task<List<DOI>> FindByPrefixAsync(string prefix)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return await Task.FromResult(col.Find(d => d.Prefix.Equals(prefix, StringComparison.OrdinalIgnoreCase)).ToList());
        }

        public async Task<List<DOI>> FindBySuffixAsync(string suffix)
        {
            if (suffix == null)
                throw new ArgumentNullException(nameof(suffix));

            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return await Task.FromResult(col.Find(d => d.Suffix.Equals(suffix, StringComparison.OrdinalIgnoreCase)).ToList());
        }

        public async Task<List<DOI>> FindByUserIdAsync(long userId)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return await Task.FromResult(col.Find(d => d.User.Id == userId).ToList());
        }

        public async Task<bool> UpdateByDOIAsync(string doi, DOIStateType state, string value)
        {
            if (!doi.Contains('/'))
                throw new BadRequestException($"The value of doi ({doi}) is invalid.");

            string prefix = doi.Split('/')[0];
            string suffix = doi.Split('/')[1];

            return await UpdateByPrefixAndSuffixAsync(prefix, suffix, state, value);
        }

        public async Task<bool> UpdateByIdAsync(long id, DOIStateType state, string value)
        {
            using var db = new LiteDatabase(_connectionString);
            var dois = db.GetCollection<DOI>("dois");

            var doi = dois.FindById(id) ?? throw new NotFoundException($"The doi (id:{id}) does not exist.");

            doi.State = state;
            doi.Value = value;
            doi.LastUpdateDate = DateTimeOffset.UtcNow;

            return await Task.FromResult(dois.Update(doi));
        }

        public async Task<bool> UpdateByPrefixAndSuffixAsync(string prefix, string suffix, DOIStateType state, string value)
        {
            using var db = new LiteDatabase(_connectionString);
            var dois = db.GetCollection<DOI>("dois");
            var users = db.GetCollection<User>("users");

            var doi = await FindByPrefixAndSuffixAsync(prefix, suffix);

            if (doi == null)
                throw new NotFoundException($"The doi (doi:{prefix}/{suffix}) does not exist.");

            doi.State = state;
            doi.Value = value;
            doi.LastUpdateDate = DateTimeOffset.UtcNow;

            return await Task.FromResult(dois.Update(doi));
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