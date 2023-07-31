using Exceptionless;
using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Server.Helpers;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

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
        public async Task<IActionResult> Post(CreateSuffixModel model)
        {
            try
            {
                if (User?.Identity?.Name == null)
                    return Unauthorized();

                var username = User.Identity.Name;

                var userService = new UserService(_connectionString);
                var user = userService.FindByName(username);

                if (user == null)
                    return Unauthorized();

                // Suffix
                var suffix = SuffixHelper.Create(user.Pattern, model.Placeholders);

                // Validation
                var placeholderService = new PlaceholderService(_connectionString);

                if (SuffixHelper.Validate(suffix, user.Pattern, new Dictionary<string, string>(placeholderService.FindByUserId(user.Id).Select(p => new KeyValuePair<string, string>(p.Expression, p.RegularExpression)))))
                {
                    return Ok(suffix);
                }

                return BadRequest("Something went wrong.");
            }
            catch (Exception e)
            {
                e.ToExceptionless().Submit();
                return BadRequest(e.Message);
            }
        }
    }
}
