using LiteDB;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Server.Entities;
using Vaelastrasz.Server.Utilities;

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

        public long Create(string prefix, string suffix, long? userId, DOIType type = DOIType.DataCite)
        {
            using var db = new LiteDatabase(_connectionString);
            var dois = db.GetCollection<DOI>("dois");
            var users = db.GetCollection<User>("users");

            var doi = new DOI()
            {
                Prefix = prefix,
                Suffix = suffix,
                Type = type,
            };

            if (userId != null)
                doi.User = users.FindById(userId);

            return dois.Insert(doi);
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