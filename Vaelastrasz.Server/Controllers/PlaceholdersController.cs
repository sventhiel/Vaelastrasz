using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Helpers;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class PlaceholdersController : ControllerBase
    {
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;
        private List<Admin> _admins;
        private readonly ILogger<PlaceholdersController> _logger;

        public PlaceholdersController(ILogger<PlaceholdersController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
            _logger = logger;
        }

        [HttpPost("placeholder")]
        public IActionResult Post(CreatePlaceholderModel model)
        {
            if (User?.Identity?.Name == null)
                return BadRequest();

            var username = User.Identity.Name;

            var userService = new UserService(_connectionString);
            var user = userService.FindByName(username);

            if (user == null)
                return BadRequest();

            var placeholderService = new PlaceholderService(_connectionString);
            placeholderService.Create(model.Expression, model.RegularExpression, user.Id);

            return Ok();
        }

        [HttpGet("placeholder")]
        public IActionResult Get()
        {
            if (User?.Identity?.Name == null)
                return BadRequest();

            var username = User.Identity.Name;

            var userService = new UserService(_connectionString);
            var user = userService.FindByName(username);

            if (user == null)
                return BadRequest();

            var placeholderService = new PlaceholderService(_connectionString);
            var placeholders = placeholderService.FindByUserId(user.Id).Select(p => ReadPlaceholderModel.Convert(p));

            return Ok(placeholders);
        }

        [HttpDelete("placeholder/{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                using (var placeholderService = new PlaceholderService(_connectionString))
                {
                    var result = placeholderService.Delete(id);

                    if (result)
                        return Ok($"deletion of placeholder (id:{id}) was successful.");

                    return BadRequest($"something went wrong...");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
