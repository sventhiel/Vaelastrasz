using Exceptionless;
using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Server.Helpers;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Authorize(Roles = "user"), Route("api")]
    public class DOIsController : ControllerBase
    {
        private ConnectionString _connectionString;

        public DOIsController(IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }
    }
}