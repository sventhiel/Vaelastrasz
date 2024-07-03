using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>()!;
            _admins = configuration.GetSection("Admins").Get<List<Admin>>()!;
            _logger = logger;
        }

        [HttpDelete("placeholders/{id}")]
        public async Task<IActionResult> DeleteByIdAsync(long id)
        {
            try
            {
                using var userService = new UserService(_connectionString);
                var user = userService.FindByName(User?.Identity?.Name);

                if (user == null)
                    return Unauthorized();

                using var placeholderService = new PlaceholderService(_connectionString);
                var placeholder = placeholderService.FindById(id);

                if (placeholder == null || placeholder.User.Id != user.Id)
                    return Forbid();

                var result = placeholderService.Delete(id);

                if (result)
                    return Ok($"deletion of placeholder (id:{id}) was successful.");

                return BadRequest($"something went wrong...");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("placeholders")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                using var userService = new UserService(_connectionString);
                var user = userService.FindByName(User?.Identity?.Name);

                if (user == null)
                    return Unauthorized();

                var placeholderService = new PlaceholderService(_connectionString);
                var placeholders = placeholderService.FindByUserId(user.Id).Select(p => ReadPlaceholderModel.Convert(p));

                return Ok(placeholders);
            }
            catch (Exception ex)
            {
                // TODO: exception handling
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("placeholders")]
        public async Task<IActionResult> PostAsync(CreatePlaceholderModel model)
        {
            try
            {
                using var userService = new UserService(_connectionString);
                var user = userService.FindByName(User?.Identity?.Name);

                if (user == null)
                    return Unauthorized();

                using var placeholderService = new PlaceholderService(_connectionString);
                var placeholder = placeholderService.Create(model.Expression, model.RegularExpression, user.Id);

                if (placeholder == null)
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                
                return Ok(placeholder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("placeholders/{id}")]
        public IActionResult PutAsync(long id, UpdatePlaceholderModel model)
        {
            try
            {
                using var userService = new UserService(_connectionString);
                var user = userService.FindByName(User?.Identity?.Name);

                if (user == null)
                    return Unauthorized();

                using var placeholderService = new PlaceholderService(_connectionString);
                var placeholder = placeholderService.FindById(id);

                if (placeholder == null || placeholder.User.Id != user.Id)
                    return Forbid();

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
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}