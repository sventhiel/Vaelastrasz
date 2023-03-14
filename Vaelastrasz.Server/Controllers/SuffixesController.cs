using Fare;
using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class SuffixesController : ControllerBase
    {
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;
        private List<Admin> _admins;
        private readonly ILogger<SuffixesController> _logger;

        public SuffixesController(IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
        }

        [HttpGet("suffix/{regex}")]
        public IActionResult GetSuffixByRegex(string regex)
        {
            Xeger xeger = new Xeger($"{regex}", new Random());
            return Ok(xeger.Generate());
        }

        [HttpGet("suffix"), Authorize]
        public IActionResult GetSuffixByUser()
        {
            var username = User?.Identity?.Name;

            if (username == null || _admins.Any(a => a.Name.Equals(username, StringComparison.CurrentCultureIgnoreCase)))
                return BadRequest();

            var userService = new UserService(_connectionString);
            var user = userService.FindByName(username);

            if (user == null)
                return BadRequest();

            Xeger xeger = new Xeger($"{user.Pattern}", new Random());
            return Ok(xeger.Generate());
        }
    }
}