using Exceptionless;
using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Route("api"), Authorize(Roles = "admin")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private List<Admin> _admins;
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;

        public UsersController(ILogger<UsersController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>()!;
            _admins = configuration.GetSection("Admins").Get<List<Admin>>()!;
            _logger = logger;
        }

        [HttpDelete("users/{id}")]
        public IActionResult Delete(long id)
        {
            using var userService = new UserService(_connectionString);
            var response = userService.Delete(id);

            return Ok(response);
        }

        [HttpGet("users")]
        public IActionResult Get()
        {
            using var userService = new UserService(_connectionString);
            var users = userService.Find();
            return Ok(new List<ReadUserModel>(users.Select(u => ReadUserModel.Convert(u))));
        }

        [HttpGet("users/{id}")]
        public IActionResult GetById(long id)
        {
            using var userService = new UserService(_connectionString);
            var user = userService.FindById(id);
            return Ok(ReadUserModel.Convert(user));
        }

        [HttpPost("users")]
        public IActionResult Post(CreateUserModel model)
        {
            using var userService = new UserService(_connectionString);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = userService.Create(model.Name, model.Password, model.Project, model.Pattern, model.AccountId, true);
            var user = userService.FindById(id);
            return StatusCode((int)HttpStatusCode.Created, ReadUserModel.Convert(user));
        }

        [HttpPut("users/{id}")]
        public IActionResult Put(long id, UpdateUserModel model)
        {
            try
            {
                using var userService = new UserService(_connectionString);
                
                if (ModelState.IsValid)
                {
                    var result = userService.Update(id, model.Password, model.Project, model.Pattern, model.AccountId, model.IsActive);
                    var user = userService.FindById(id);

                    return Ok(ReadUserModel.Convert(user));
                }

                var errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(errorMessage);
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