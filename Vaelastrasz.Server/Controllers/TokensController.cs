using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vaelastrasz.Server.Configuration;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;
        private List<Admin> _admins;

        public TokensController(IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
        }

        [HttpPost("token")]
        public IActionResult Post(LoginUserModel model)
        {
            if(string.IsNullOrEmpty(_jwtConfiguration.IssuerSigningKey))
                return BadRequest();

            var userService = new UserService(_connectionString);

            if (!userService.Verify(model.Username, model.Password))
                return BadRequest();

            var authSigningKey = new SymmetricSecurityKey(Encoding.Latin1.GetBytes(_jwtConfiguration.IssuerSigningKey));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, model.Username),
                    }),
                Expires = DateTime.Now.AddHours(_jwtConfiguration.ValidLifetime),
                Issuer = _jwtConfiguration.ValidIssuer,
                Audience = _jwtConfiguration.ValidAudience,
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512Signature)
            };

            if (_admins.Any(a => a.Name.Equals(model.Username)))
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, "admin"));

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(tokenHandler.WriteToken(token));
        }

        [HttpGet("token"), Authorize]
        public IActionResult Get()
        {
            if (string.IsNullOrEmpty(_jwtConfiguration.IssuerSigningKey))
                return BadRequest();

            if (User?.Identity?.Name == null)
                return BadRequest();

            var username = User.Identity.Name;

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.IssuerSigningKey));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username), 
                }),
                Expires = DateTime.Now.AddHours(_jwtConfiguration.ValidLifetime),
                Issuer = _jwtConfiguration.ValidIssuer,
                Audience = _jwtConfiguration.ValidAudience,
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512Signature)
            };

            if(_admins.Any(a => a.Name.Equals(username)))
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, "admin"));

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(tokenHandler.WriteToken(token));
        }
    }
}
