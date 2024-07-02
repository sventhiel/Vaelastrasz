using Exceptionless;
using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Helpers;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Authorize(Roles = "user"), Route("api")]
    public class DOIsController : ControllerBase
    {
        private readonly ILogger<DataCiteController> _logger;
        private List<Admin> _admins;
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;

        public DOIsController(ILogger<DataCiteController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>()!;
            _admins = configuration.GetSection("Admins").Get<List<Admin>>()!;
            _logger = logger;
        }

        // DELETE
        [HttpDelete("dois/{prefix}/{suffix}")]
        public async Task<IActionResult> DeleteAsync(string prefix, string suffix)
        {
            try
            {
                using var userService = new UserService(_connectionString);
                var user = userService.FindByName(User?.Identity?.Name);

                if (user == null)
                    return Unauthorized();

                using var doiService = new DOIService(_connectionString);

                var result = doiService.FindByDOI(prefix, suffix);

                if (result?.User.Id != user.Id)
                    return Forbid();

                if (doiService.Delete(result.Id))
                    return Ok($"deletion of doi: {prefix}/{suffix} was successful.");

                return BadRequest($"something went wrong...");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET
        [HttpGet("dois")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                using var userService = new UserService(_connectionString);
                var user = userService.FindByName(User?.Identity?.Name);

                if (user == null)
                    return Unauthorized();

                using var doiService = new DOIService(_connectionString);
                var result = doiService.FindByUserId(user.Id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("dois/{prefix}/{suffix}")]
        public async Task<IActionResult> GetAsync(string prefix, string suffix)
        {
            try
            {
                using var userService = new UserService(_connectionString);
                var user = userService.FindByName(User?.Identity?.Name);

                if (user == null)
                    return Unauthorized();

                using var doiService = new DOIService(_connectionString);
                var result = doiService.FindByDOI(prefix, suffix);

                if (result == null || result.User.Id != user.Id)
                    return Forbid();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // POST
        [HttpPost("dois")]
        public async Task<IActionResult> PostAsync(CreateDOIModel model)
        {
            try
            {
                using var userService = new UserService(_connectionString);
                var user = userService.FindByName(User?.Identity?.Name);

                if (user == null)
                    return Unauthorized();

                if (user.Account == null)
                    return Forbid();

                using var placeholderService = new PlaceholderService(_connectionString);
                var placeholders = placeholderService.FindByUserId(user.Id);

                if (!DOIHelper.Validate($"{model.Prefix}/{model.Suffix}", user.Account.Prefix, user.Pattern, new Dictionary<string, string>(placeholders.Select(p => new KeyValuePair<string, string>(p.Expression, p.RegularExpression)))))
                    return Forbid();

                using var doiService = new DOIService(_connectionString);
                var result = doiService.Create(model.Prefix, model.Suffix, user.Id, DOIStateType.Draft);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT
        [HttpPut("dois/{prefix}/{suffix}")]
        public async Task<IActionResult> PutByDOIAsync(string prefix, string suffix, UpdateDOIModel model)
        {
            try
            {
                using var userService = new UserService(_connectionString);
                var user = userService.FindByName(User?.Identity?.Name);

                if (user == null)
                    return Unauthorized();

                using var doiService = new DOIService(_connectionString);
                var doi = doiService.FindByPrefixAndSuffix(prefix, suffix);

                if (doi == null || doi.User.Id != user.Id)
                    return Forbid();

                if (ModelState.IsValid)
                {
                    var result = doiService.Update(prefix, suffix, model.UserId);

                    if (result)
                    {
                        doi = doiService.FindByPrefixAndSuffix(prefix, suffix);
                        return Ok(doi);
                    }
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ex.ToExceptionless().Submit();
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}