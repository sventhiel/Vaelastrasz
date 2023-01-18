using Fare;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vaelastrasz.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class SuffixesController : ControllerBase
    {
        [HttpGet("suffix/{regex}")]
        public string GetSuffixByRegex(string regex)
        {
            Xeger xeger = new Xeger($"{regex}", new Random());
            return xeger.Generate();
        }

        [HttpGet("suffix"), Authorize]
        public string GetSuffixByUser()
        {
            var username = User.Identity.Name;

            Xeger xeger = new Xeger($"[a-z]{{1,4}}", new Random());
            return xeger.Generate();
        }
    }
}