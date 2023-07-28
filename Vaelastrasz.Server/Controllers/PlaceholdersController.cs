using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Authorize(Roles = "user"), Route("api")]
    public class PlaceholdersController : ControllerBase
    {
        private readonly ILogger<PlaceholdersController> _logger;
        private List<Admin> _admins;
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;

        public PlaceholdersController(ILogger<PlaceholdersController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
            _logger = logger;
        }

        [HttpDelete("placeholders/{id}")]
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

        [HttpGet("placeholders")]
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

        [HttpPost("placeholders")]
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

        [HttpPut("placeholders/{id}")]
        public IActionResult Put(long id, UpdatePlaceholderModel model)
        {
            try
            {
                using (var placeholderService = new PlaceholderService(_connectionString))
                {
                    var placeholder = placeholderService.FindById(id);

                    if (placeholder == null)
                        return BadRequest();

                    if (ModelState.IsValid)
                    {
                        var result = placeholderService.Update(id, model.Expression, model.RegularExpression, model.UserId);

                        if (result)
                        {
                            placeholder = placeholderService.FindById(id);
                            return Ok(placeholder);
                        }
                    }

                    return BadRequest();
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