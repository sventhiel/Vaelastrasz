using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Route("api")]
    public class TokensController : ControllerBase
    {
        private readonly ILogger<TokensController> _logger;
        private List<Admin> _admins;
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;

        public TokensController(ILogger<TokensController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>()!;
            _admins = configuration.GetSection("Admins").Get<List<Admin>>()!;
            _logger = logger;
        }

        [Authorize(Roles = "user"), HttpGet("tokens")]
        public async Task<IActionResult> GetAsync()
        {
            var username = User.Identity!.Name;
            if (username == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.IssuerSigningKey!));
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, _admins.Any(a => a.Name.Equals(username, StringComparison.InvariantCultureIgnoreCase)) ? "admin" : "user")
                }),
                Expires = DateTime.Now.AddHours(_jwtConfiguration.ValidLifetime),
                Issuer = _jwtConfiguration.ValidIssuer,
                Audience = _jwtConfiguration.ValidAudience,
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512)
            };

            if (_admins.Any(a => a.Name.Equals(username)))
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, "admin"));

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(tokenHandler.WriteToken(token));
        }

        [HttpPost("tokens")]
        public async Task<IActionResult> Post(LoginUserModel model)
        {
            using var userService = new UserService(_connectionString);

            if (!userService.Verify(model.Username, model.Password))
                return StatusCode((int)HttpStatusCode.BadRequest, "");

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.IssuerSigningKey ?? ""));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                            new Claim(ClaimTypes.Name, model.Username),
                            new Claim(ClaimTypes.Role, _admins.Any(a => a.Name.Equals(model.Username, StringComparison.InvariantCultureIgnoreCase)) ? "admin" : "user")
                }),
                Expires = DateTime.Now.AddHours(_jwtConfiguration.ValidLifetime),
                Issuer = _jwtConfiguration.ValidIssuer,
                Audience = _jwtConfiguration.ValidAudience,
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return StatusCode((int)HttpStatusCode.OK, tokenHandler.WriteToken(token));
        }
    }
}