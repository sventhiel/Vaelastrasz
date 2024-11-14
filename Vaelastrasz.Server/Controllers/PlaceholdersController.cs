using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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

        public PlaceholdersController(ILogger<PlaceholdersController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _admins = configuration.GetSection("Admins").Get<List<Admin>>()!;
            _logger = logger;
        }

        [HttpDelete("placeholders/{id}")]
        public async Task<IActionResult> DeleteByIdAsync(long id)
        {
            using var userService = new UserService(_connectionString);
            var user = await userService.FindByNameAsync(User.Identity!.Name!);

            using var placeholderService = new PlaceholderService(_connectionString);
            var placeholder = await placeholderService.FindByIdAsync(id);

            if (placeholder.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action.");

            var response = await placeholderService.DeleteByIdAsync(id);
            return Ok(response);
        }

        [HttpGet("placeholders")]
        public async Task<IActionResult> GetAsync()
        {
            using var userService = new UserService(_connectionString);
            var user = await userService.FindByNameAsync(User?.Identity?.Name);

            var placeholderService = new PlaceholderService(_connectionString);
            var placeholders = (await placeholderService.FindByUserIdAsync(user.Id)).Select(p => ReadPlaceholderModel.Convert(p));

            return Ok(placeholders);
        }

        [HttpGet("placeholders/{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            using var userService = new UserService(_connectionString);
            var user = await userService.FindByNameAsync(User?.Identity?.Name);

            var placeholderService = new PlaceholderService(_connectionString);
            var placeholder = await placeholderService.FindByIdAsync(id);

            return Ok(ReadPlaceholderModel.Convert(placeholder));
        }

        [HttpPost("placeholders")]
        [SwaggerResponse(201, "Resource created successfully", typeof(ReadPlaceholderModel))]
        public async Task<IActionResult> PostAsync(CreatePlaceholderModel model)
        {
            using var userService = new UserService(_connectionString);
            var user = await userService.FindByNameAsync(User?.Identity?.Name);

            using var placeholderService = new PlaceholderService(_connectionString);
            var id = await placeholderService.CreateAsync(model.Expression, model.RegularExpression, user.Id);
            var placeholder = await placeholderService.FindByIdAsync(id);

            var request = HttpContext.Request;
            string baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            string resourceUrl = $"{baseUrl}/api/placeholders/{placeholder.Id}";

            return Created(resourceUrl, ReadPlaceholderModel.Convert(placeholder));
        }

        [HttpPut("placeholders/{id}")]
        public async Task<IActionResult> PutByIdAsync(long id, UpdatePlaceholderModel model)
        {
            using var userService = new UserService(_connectionString);
            var user = await userService.FindByNameAsync(User?.Identity?.Name);

            using var placeholderService = new PlaceholderService(_connectionString);
            var placeholder = await placeholderService.FindByIdAsync(id);

            if (placeholder.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action..");

            var result = await placeholderService.UpdateByIdAsync(id, model.Expression, model.RegularExpression, model.UserId);
            placeholder = await placeholderService.FindByIdAsync(id);

            return Ok(ReadPlaceholderModel.Convert(placeholder));
        }
    }
}