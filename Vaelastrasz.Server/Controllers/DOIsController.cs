using Exceptionless;
using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NameParser;
using System.Net;
using System.Security.Authentication;
using System.Security.Principal;
using Vaelastrasz.Library.Entities;
using Vaelastrasz.Library.Exceptions;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Helpers;
using Vaelastrasz.Server.Models;
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
            using var userService = new UserService(_connectionString);
            var user = userService.FindByName(User?.Identity?.Name);

            using var doiService = new DOIService(_connectionString);

            var result = doiService.FindByPrefixAndSuffix(prefix, suffix);

            if (result.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action..");

            var response = doiService.DeleteById(result.Id);
            return Ok(response);
        }

        // GET
        [HttpGet("dois")]
        public async Task<IActionResult> GetAsync()
        {
            using var userService = new UserService(_connectionString);
            var user = userService.FindByName(User?.Identity?.Name);

            using var doiService = new DOIService(_connectionString);
            var dois = doiService.FindByUserId(user.Id).Select(d => ReadDOIModel.Convert(d));

            return Ok(dois);
        }

        [HttpGet("dois/{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            using var userService = new UserService(_connectionString);
            var user = userService.FindByName(User?.Identity?.Name);

            using var doiService = new DOIService(_connectionString);
            var doi = doiService.FindById(id);

            if(doi.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action.");

            return Ok(ReadDOIModel.Convert(doi));
        }

        [HttpGet("dois/{prefix}/{suffix}")]
        public async Task<IActionResult> GetByPrefixAndSuffixAsync(string prefix, string suffix)
        {
            using var userService = new UserService(_connectionString);
            var user = userService.FindByName(User?.Identity?.Name);

            using var doiService = new DOIService(_connectionString);
            var result = doiService.FindByPrefixAndSuffix(prefix, suffix);

            if (result.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action..");

            return Ok(ReadDOIModel.Convert(result));
        }

        // POST
        [HttpPost("dois")]
        public async Task<IActionResult> PostAsync(CreateDOIModel model)
        {
            using var userService = new UserService(_connectionString);
            var user = userService.FindByName(User.Identity!.Name!);

            if (user.Account == null)
                throw new NotFoundException($"The account of user (id: {user.Id}) does not exist.");

            using var placeholderService = new PlaceholderService(_connectionString);
            var placeholders = placeholderService.FindByUserId(user.Id);

            if (!DOIHelper.Validate($"{model.Prefix}/{model.Suffix}", user.Account.Prefix, user.Pattern, new Dictionary<string, string>(placeholders.Select(p => new KeyValuePair<string, string>(p.Expression, p.RegularExpression)))))
                throw new ForbidException($"The doi (prefix: {model.Prefix}, suffix: {model.Suffix}) is invalid.");

            using var doiService = new DOIService(_connectionString);
            var id = doiService.Create(model.Prefix, model.Suffix, DOIStateType.Draft, user.Id, "");
            var doi = doiService.FindById(id);

            var request = HttpContext.Request;
            string baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            string resourceUrl = $"{baseUrl}/api/dois/{doi.Id}";

            return Created(resourceUrl, ReadDOIModel.Convert(doi));
        }

        // PUT
        [HttpPut("dois/{prefix}/{suffix}")]
        public async Task<IActionResult> PutByPrefixAndSuffixAsync(string prefix, string suffix, UpdateDOIModel model)
        {
            using var userService = new UserService(_connectionString);
            var user = userService.FindByName(User?.Identity?.Name);

            using var doiService = new DOIService(_connectionString);
            var doi = doiService.FindByPrefixAndSuffix(prefix, suffix);

            if (doi.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action.");

            var result = doiService.UpdateByPrefixAndSuffix(prefix, suffix, model.State, model.Value);
            doi = doiService.FindByPrefixAndSuffix(prefix, suffix);
            return Ok(ReadDOIModel.Convert(doi));
        }

        [HttpPut("dois/{doi}")]
        public async Task<IActionResult> PutByDOIAsync(string doi, UpdateDOIModel model)
        {
            using var userService = new UserService(_connectionString);
            var user = userService.FindByName(User.Identity!.Name!);

            using var doiService = new DOIService(_connectionString);
            var _doi = doiService.FindByDOI(doi);

            if (_doi.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action.");

            var result = doiService.UpdateById(_doi.Id, model.State, model.Value);
            _doi = doiService.FindByDOI(doi);
            return Ok(ReadDOIModel.Convert(_doi));
        }

        [HttpPut("dois/{id}")]
        public async Task<IActionResult> PutByIdAsync(long id, UpdateDOIModel model)
        {
            using var doiService = new DOIService(_connectionString);

            var result = doiService.UpdateById(id, model.State, "");
            var doi = doiService.FindById(id);

            return Ok(ReadDOIModel.Convert(doi));
        }
    }
}