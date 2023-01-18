using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NameParser;

namespace Vaelastrasz.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class NamesController : ControllerBase
    {
        [HttpGet("name"), AllowAnonymous]
        public HumanName GetName(string name)
        {
            return new HumanName(name);
        }
    }
}