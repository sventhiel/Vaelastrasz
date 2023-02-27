using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaelastrasz.Server.Configuration;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class DOIsController : ControllerBase
    {
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;
        private List<Admin> _admins;

        public DOIsController(IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
        }

        [HttpPost("doi")]
        public IActionResult Post(CreateDOIModel model)
        {
            return BadRequest();
        }

        [HttpGet("doi")]
        public IActionResult GetById(string doi)
        {
            return BadRequest();
        }

        [HttpGet("doi/{prefix}/{suffix}")]
        public IActionResult GetByPrefixAndSuffix(string prefix, string suffix)
        {
            return BadRequest();
        }
    }
}