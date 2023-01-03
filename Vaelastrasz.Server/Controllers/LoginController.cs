using LiteDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vaelastrasz.Server.Configuration;
using Vaelastrasz.Server.Models;

namespace Vaelastrasz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;
        private List<Admin> _admins;

        public LoginController(IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginUserModel model)
        {
            //
            // Check Admin Configuration first
            var admin = _admins.Find(a => a.Name.Equals(model.Username));

            if (admin != null && admin.Password.Equals(model.Password))
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.IssuerSigningKey));
                var token = new JwtSecurityToken(
                    issuer: _jwtConfiguration.ValidIssuer,
                    audience: _jwtConfiguration.ValidAudience,
                    expires: DateTime.Now.AddHours(_jwtConfiguration.ValidLifetime),
                    claims: new[] { new Claim(ClaimTypes.Name, admin.Name) },
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512)
                    );

                var user = new { id = 1, name = "Hans", email = "sven.thiel@uni-jena.de", jwt = new JwtSecurityTokenHandler().WriteToken(token) };

                return Ok(user);
            }

            return BadRequest();
        }
    }
}
