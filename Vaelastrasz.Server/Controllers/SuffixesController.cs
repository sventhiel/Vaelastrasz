using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net;
using Vaelastrasz.Library.Entities;
using Vaelastrasz.Library.Exceptions;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Server.Helpers;
using Vaelastrasz.Server.Services;

//ry
namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Authorize(Roles = "user"), Route("api")]
    public class SuffixesController : ControllerBase
    {
        private ConnectionString _connectionString;

        public SuffixesController(IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        [HttpPost("suffixes")]
        public async Task<IActionResult> PostAsync(CreateSuffixModel model)
        {
            using var userService = new UserService(_connectionString);
            var user = userService.FindByName(User.Identity?.Name);

            // Suffix
            var suffix = SuffixHelper.Create(user.Pattern, model.Placeholders);

            // Validation
            var placeholderService = new PlaceholderService(_connectionString);

            if (SuffixHelper.Validate(suffix, user.Pattern, new Dictionary<string, string>(placeholderService.FindByUserId(user.Id).Select(p => new KeyValuePair<string, string>(p.Expression, p.RegularExpression)))))
                return Ok(suffix);

            throw new BadRequestException($"The value of suffix ({suffix}) is invalid.");
        }
    }
}