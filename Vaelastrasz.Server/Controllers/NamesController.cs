using Microsoft.AspNetCore.Mvc;
using NameParser;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Route("api")]
    public class NamesController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public NamesController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpPost("names")]
        public async Task<IActionResult> PostAsync([FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var humanName = new HumanName(name);

            if (humanName.IsUnparsable)
                return BadRequest();

            return Ok(humanName);
        }
    }
}