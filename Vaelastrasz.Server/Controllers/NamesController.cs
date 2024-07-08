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
            return Ok(new HumanName(name));
        }
    }
}