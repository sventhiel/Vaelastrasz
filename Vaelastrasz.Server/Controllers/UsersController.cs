using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
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

        public UsersController(ILogger<UsersController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _admins = configuration.GetSection("Admins").Get<List<Admin>>()!;
            _logger = logger;
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            using var userService = new UserService(_connectionString);
            var response = await userService.DeleteByIdAsync(id);

            return Ok(response);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAsync()
        {
            using var userService = new UserService(_connectionString);
            var users = await userService.FindAsync();

            return Ok(new List<ReadUserModel>(users.Select(u => ReadUserModel.Convert(u))));
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            using var userService = new UserService(_connectionString);
            var user = await userService.FindByIdAsync(id);

            return Ok(ReadUserModel.Convert(user));
        }

        [HttpPost("users")]
        public async Task<IActionResult> PostAsync(CreateUserModel model)
        {
            using var userService = new UserService(_connectionString);

            var id = await userService.CreateAsync(model.Name, model.Password, model.Project, model.Pattern, model.AccountId, true);
            var user = await userService.FindByIdAsync(id);

            var request = HttpContext.Request;
            string baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            string resourceUrl = $"{baseUrl}/api/users/{user.Id}";

            return Created(resourceUrl, ReadUserModel.Convert(user));
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> PutByIdAsync(long id, UpdateUserModel model)
        {
            using var userService = new UserService(_connectionString);

            var result = await userService.UpdateByIdAsync(id, model.Name, model.Password, model.Project, model.Pattern, model.AccountId, model.IsActive);
            var user = await userService.FindByIdAsync(id);

            return Ok(ReadUserModel.Convert(user));
        }
    }
}