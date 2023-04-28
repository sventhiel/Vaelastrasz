using Fare;
using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Helpers;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class DOIsController : ControllerBase
    {
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;
        private List<Admin> _admins;

        public DOIsController(IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
        }

        [HttpPost("doi")]
        public IActionResult Post(CreateDOIModel model)
        {
            if (User?.Identity?.Name == null)
                return BadRequest();

            var username = User.Identity.Name;

            var userService = new UserService(_connectionString);
            var user = userService.FindByName(username);

            if (user == null)
                return BadRequest();

            if (user.Account == null)
                return BadRequest();

            var placeholderService = new PlaceholderService(_connectionString);
            var placeholders = placeholderService.FindByUserId(user.Id);

            // DOI
            var doi = DOIHelper.Create(user.Account.Prefix, user.Project, user.Pattern, model.Placeholders);

            // Validation
            if (DOIHelper.Validate(doi, user.Account.Prefix, user.Project, user.Pattern, new Dictionary<string, string>(placeholders.Select(p => new KeyValuePair<string, string>(p.Expression, p.RegularExpression)))))
                return Ok(doi);

            return BadRequest("Something went wrong.");
        }
    }
}