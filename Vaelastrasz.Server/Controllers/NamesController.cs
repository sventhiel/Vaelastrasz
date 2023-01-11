using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NameParser;

namespace Vaelastrasz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NamesController : ControllerBase
    {
        [HttpGet]
        public HumanName GetName(string name)
        {
            return new HumanName(name);
        }
    }
}
