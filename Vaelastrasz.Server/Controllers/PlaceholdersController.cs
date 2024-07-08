using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaelastrasz.Library.Exceptions;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

//rdy
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
            using var userService = new UserService(_connectionString);
            var user = userService.FindByName(User?.Identity?.Name);

            using var placeholderService = new PlaceholderService(_connectionString);
            var placeholder = placeholderService.FindById(id);

            if (placeholder.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action.");

            var response = placeholderService.DeleteById(id);
            return Ok(response);
        }

        [HttpGet("placeholders")]
        public async Task<IActionResult> GetAsync()
        {
            using var userService = new UserService(_connectionString);
            var user = userService.FindByName(User?.Identity?.Name);

            var placeholderService = new PlaceholderService(_connectionString);
            var placeholders = placeholderService.FindByUserId(user.Id).Select(p => ReadPlaceholderModel.Convert(p));

            return Ok(placeholders);
        }

        [HttpGet("placeholders/{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            using var userService = new UserService(_connectionString);
            var user = userService.FindByName(User?.Identity?.Name);

            var placeholderService = new PlaceholderService(_connectionString);
            var placeholder = placeholderService.FindById(id);

            return Ok(ReadPlaceholderModel.Convert(placeholder));
        }

        [HttpPost("placeholders")]
        public async Task<IActionResult> PostAsync(CreatePlaceholderModel model)
        {
            using var userService = new UserService(_connectionString);
            var user = userService.FindByName(User?.Identity?.Name);

            using var placeholderService = new PlaceholderService(_connectionString);
            var id = placeholderService.Create(model.Expression, model.RegularExpression, user.Id);
            var placeholder = placeholderService.FindById(id);

            var request = HttpContext.Request;
            string baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            string resourceUrl = $"{baseUrl}/api/placeholders/{placeholder.Id}";

            return Created(resourceUrl, ReadPlaceholderModel.Convert(placeholder));
        }

        [HttpPut("placeholders/{id}")]
        public IActionResult PutByIdAsync(long id, UpdatePlaceholderModel model)
        {
            using var userService = new UserService(_connectionString);
            var user = userService.FindByName(User?.Identity?.Name);

            using var placeholderService = new PlaceholderService(_connectionString);
            var placeholder = placeholderService.FindById(id);

            if (placeholder.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action..");

            var result = placeholderService.UpdateById(id, model.Expression, model.RegularExpression, model.UserId);
            placeholder = placeholderService.FindById(id);

            return Ok(ReadPlaceholderModel.Convert(placeholder));
        }
    }
}