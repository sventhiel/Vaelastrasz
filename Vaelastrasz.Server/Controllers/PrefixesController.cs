using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaelastrasz.Server.Services;

//rdy
namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Authorize(Roles = "user"), Route("api")]
    public class PrefixesController : ControllerBase
    {
        private ConnectionString _connectionString;

        public PrefixesController(IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        [HttpGet("prefixes")]
        public async Task<IActionResult> GetAsync()
        {
            using var userService = new UserService(_connectionString);
            var user = await userService.FindByNameAsync(User.Identity.Name);

            // Prefix
            var prefix = user.Account.Prefix;

            return Ok(prefix);
        }
    }
}