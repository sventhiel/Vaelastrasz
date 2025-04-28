using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Vaelastrasz.Library.Exceptions;
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

        public DOIsController(ILogger<DataCiteController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _admins = configuration.GetSection("Admins").Get<List<Admin>>()!;
            _logger = logger;
        }

        [HttpDelete("dois/{prefix}/{suffix}")]
        public async Task<IActionResult> DeleteAsync(string prefix, string suffix)
        {
            using var userService = new UserService(_connectionString);

            if (User?.Identity?.Name == null)
                return Forbid("You are not allowed to execute this function.");

            var user = await userService.FindByNameAsync(User.Identity.Name);

            using var doiService = new DOIService(_connectionString);

            var result = await doiService.FindByPrefixAndSuffixAsync(prefix, suffix);

            if (result.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action.");

            var response = await doiService.DeleteByIdAsync(result.Id);
            return Ok(response);
        }

        [HttpGet("dois")]
        public async Task<IActionResult> GetAsync()
        {
            using var userService = new UserService(_connectionString);

            if (User?.Identity?.Name == null)
                return Forbid("You are not allowed to execute this function.");

            var user = await userService.FindByNameAsync(User.Identity.Name);

            using var doiService = new DOIService(_connectionString);
            var dois = (await doiService.FindByUserIdAsync(user.Id)).Select(d => ReadDOIModel.Convert(d));

            return Ok(dois);
        }

        [HttpGet("dois/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            using var userService = new UserService(_connectionString);

            if (User?.Identity?.Name == null)
                return Forbid("You are not allowed to execute this function.");

            var user = await userService.FindByNameAsync(User.Identity.Name);

            using var doiService = new DOIService(_connectionString);
            var doi = await doiService.FindByIdAsync(id);

            if (doi.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action.");

            return Ok(ReadDOIModel.Convert(doi));
        }

        [HttpGet("dois/{prefix}/{suffix}")]
        public async Task<IActionResult> GetByPrefixAndSuffix(string prefix, string suffix)
        {
            using var userService = new UserService(_connectionString);

            if (User?.Identity?.Name == null)
                return Forbid("You are not allowed to execute this function.");

            var user = await userService.FindByNameAsync(User.Identity.Name);

            using var doiService = new DOIService(_connectionString);
            var result = await doiService.FindByPrefixAndSuffixAsync(prefix, suffix);

            if (result.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action..");

            return Ok(ReadDOIModel.Convert(result));
        }

        [HttpPost("dois")]
        [SwaggerResponse(201, "Resource created successfully", typeof(ReadDOIModel))]
        public async Task<IActionResult> Post(CreateDOIModel model)
        {
            using var userService = new UserService(_connectionString);

            if (User?.Identity?.Name == null)
                return Forbid("You are not allowed to execute this function.");

            var user = await userService.FindByNameAsync(User.Identity.Name);

            if (user.Account == null)
                throw new NotFoundException($"The account of user (id: {user.Id}) does not exist.");

            using var placeholderService = new PlaceholderService(_connectionString);
            var placeholders = await placeholderService.FindByUserIdAsync(user.Id);

            if (!DOIHelper.Validate($"{model.Prefix}/{model.Suffix}", user.Account.Prefix, user.Pattern, new Dictionary<string, string>(placeholders.Select(p => new KeyValuePair<string, string>(p.Expression, p.RegularExpression)))))
                throw new ForbiddenException($"The doi (prefix: {model.Prefix}, suffix: {model.Suffix}) is invalid.");

            using var doiService = new DOIService(_connectionString);
            var id = await doiService.CreateAsync(model.Prefix, model.Suffix, DOIStateType.Draft, user.Id, "");
            var doi = await doiService.FindByIdAsync(id);

            var request = HttpContext.Request;
            string baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            string resourceUrl = $"{baseUrl}/api/dois/{doi.Id}";

            return Created(resourceUrl, ReadDOIModel.Convert(doi));
        }

        [HttpPut("dois/{doi}")]
        public async Task<IActionResult> PutByDOI(string doi, UpdateDOIModel model)
        {
            using var userService = new UserService(_connectionString);

            if (User?.Identity?.Name == null)
                return Forbid("You are not allowed to execute this function.");

            var user = await userService.FindByNameAsync(User.Identity.Name);

            using var doiService = new DOIService(_connectionString);
            var _doi = await doiService.FindByDOIAsync(doi);

            if (_doi.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action.");

            var result = await doiService.UpdateByIdAsync(_doi.Id, model.State, model.Value);
            _doi = await doiService.FindByDOIAsync(doi);
            return Ok(ReadDOIModel.Convert(_doi));
        }

        //[HttpPut("dois/{id}")]
        //public async Task<IActionResult> PutById(long id, UpdateDOIModel model)
        //{
        //    using var doiService = new DOIService(_connectionString);

        //    var result = await doiService.UpdateByIdAsync(id, model.State, "");
        //    var doi = await doiService.FindByIdAsync(id);

        //    return Ok(ReadDOIModel.Convert(doi));
        //}

        [HttpPut("dois/{prefix}/{suffix}")]
        public async Task<IActionResult> PutByPrefixAndSuffix(string prefix, string suffix, UpdateDOIModel model)
        {
            using var userService = new UserService(_connectionString);

            if (User?.Identity?.Name == null)
                return Forbid("You are not allowed to execute this function.");

            var user = await userService.FindByNameAsync(User.Identity.Name);

            using var doiService = new DOIService(_connectionString);
            var doi = await doiService.FindByPrefixAndSuffixAsync(prefix, suffix);

            if (doi.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action.");

            var result = await doiService.UpdateByPrefixAndSuffixAsync(prefix, suffix, model.State, model.Value);
            doi = await doiService.FindByPrefixAndSuffixAsync(prefix, suffix);
            return Ok(ReadDOIModel.Convert(doi));
        }
    }
}