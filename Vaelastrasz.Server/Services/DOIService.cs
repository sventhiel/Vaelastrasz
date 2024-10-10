using LiteDB;
using Vaelastrasz.Library.Entities;
using Vaelastrasz.Library.Exceptions;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Server.Services
{
    /// <summary>
    ///
    /// </summary>
    public class DOIService : IDisposable
    {
        private readonly ConnectionString _connectionString;
        private bool disposed = false;

        /// <summary>
        ///
        /// </summary>
        /// <param name="connectionString"></param>
        public DOIService(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        ///
        /// </summary>
        ~DOIService()
        {
            Dispose(false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <param name="state"></param>
        /// <param name="userId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="ForbidException"></exception>
        /// <exception cref="ConflictException"></exception>
        public async Task<long> CreateAsync(string prefix, string suffix, DOIStateType state, long userId, string value)
        {
            using var db = new LiteDatabase(_connectionString);
            var dois = db.GetCollection<DOI>("dois");
            var users = db.GetCollection<User>("users");

            var user = users.Include(u => u.Account).FindById(userId) ?? throw new NotFoundException($"The user (id:{userId}) does not exist.");

            if (!user.Account.Prefix.Equals(prefix, StringComparison.InvariantCultureIgnoreCase))
                throw new ForbidException($"The doi (prefix:{prefix}) is invalid.");

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

        /// <summary>
        ///
        /// </summary>
        /// <param name="doi"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        public async Task<bool> DeleteByDOIAsync(string doi)
        {
            if (!doi.Contains('/'))
                throw new BadRequestException($"The value of doi ({doi}) is invalid.");

            string prefix = doi.Split('/')[0];
            string suffix = doi.Split('/')[1];

            return await DeleteByPrefixAndSuffixAsync(prefix, suffix);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return await Task.FromResult(col.Delete(id));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="ConflictException"></exception>
        /// <exception cref="ForbidException"></exception>
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
                throw new ForbidException($"The doi (prefix:{prefix}) is invalid.");

            return await Task.FromResult(dois.Delete(doi.Single().Id));
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<List<DOI>> FindAsync()
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return await Task.FromResult(col.FindAll().ToList());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="doi"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        public async Task<DOI> FindByDOIAsync(string doi)
        {
            if (!doi.Contains('/'))
                throw new BadRequestException($"The value of doi ({doi}) is invalid.");

            string prefix = doi.Split('/')[0];
            string suffix = doi.Split('/')[1];

            return await FindByPrefixAndSuffixAsync(prefix, suffix);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<DOI> FindByIdAsync(long id)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            var doi = col.FindById(id);

            return doi == null ? throw new NotFoundException($"The doi (id:{id}) does not exist.") : await Task.FromResult(doi);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="ConflictException"></exception>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public async Task<List<DOI>> FindByPrefixAsync(string prefix)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return await Task.FromResult(col.Find(d => d.Prefix.Equals(prefix, StringComparison.OrdinalIgnoreCase)).ToList());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="suffix"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<List<DOI>> FindBySuffixAsync(string suffix)
        {
            if (suffix == null)
                throw new ArgumentNullException(nameof(suffix));

            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return await Task.FromResult(col.Find(d => d.Suffix.Equals(suffix, StringComparison.OrdinalIgnoreCase)).ToList());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<DOI>> FindByUserIdAsync(long userId)
        {
            using var db = new LiteDatabase(_connectionString);
            var col = db.GetCollection<DOI>("dois");

            return await Task.FromResult(col.Find(d => d.User.Id == userId).ToList());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="doi"></param>
        /// <param name="state"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        public async Task<bool> UpdateByDOIAsync(string doi, DOIStateType state, string value)
        {
            if (!doi.Contains('/'))
                throw new BadRequestException($"The value of doi ({doi}) is invalid.");

            string prefix = doi.Split('/')[0];
            string suffix = doi.Split('/')[1];

            return await UpdateByPrefixAndSuffixAsync(prefix, suffix, state, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <param name="state"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
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