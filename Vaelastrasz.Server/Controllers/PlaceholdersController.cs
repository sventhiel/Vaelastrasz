using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaelastrasz.Server.Configuration;
using Vaelastrasz.Server.Models;

namespace Vaelastrasz.Server.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class PlaceholdersController : ControllerBase
    {
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;
        private List<Admin> _admins;

        public PlaceholdersController(IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
        }

        [HttpPost("placeholder")]
        public IActionResult Post(CreatePlaceholderModel model)
        {
            return BadRequest();
        }

        [HttpGet("placeholder")]
        public IActionResult GetById(UpdatePlaceholderModel model)
        {
            return BadRequest();
        }

        [HttpPut("placeholder")]
        public IActionResult Put(UpdatePlaceholderModel model)
        {
            return BadRequest();
        }

        [HttpDelete("placeholder")]
        public IActionResult Delete(long id)
        {
            return BadRequest();
        }
    }
}