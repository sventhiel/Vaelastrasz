using Exceptionless;
using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaelastrasz.Server.Services;

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
            try
            {
                using var userService = new UserService(_connectionString);
                var user = userService.FindByName(User?.Identity?.Name);

                if (user == null)
                    return Unauthorized();

                // Prefix
                var prefix = user.Account.Prefix;

                return Ok(prefix);
            }
            catch (Exception e)
            {
                e.ToExceptionless().Submit();
                return BadRequest(e.Message);
            }
        }
    }
}