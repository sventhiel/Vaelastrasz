using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NameParser;
using Vaelastrasz.Server.Configuration;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Route("api")]
    public class NamesController : ControllerBase
    {
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;
        private List<Admin> _admins;
        private readonly ILogger<NamesController> _logger;

        public NamesController(ILogger<NamesController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
        }

        [HttpGet("name"), AllowAnonymous]
        public HumanName GetName(string name)
        {
            _logger.LogInformation("blublablubb");
            return new HumanName(name);
        }
    }
}