using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
            _logger = logger;
        }

        [Authorize(Roles = "user"), HttpGet("tokens")]
        public IActionResult Get()
        {
            try
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
                    new Claim(ClaimTypes.Name, username)
                    }),
                    Expires = DateTime.Now.AddHours(_jwtConfiguration.ValidLifetime),
                    Issuer = _jwtConfiguration.ValidIssuer,
                    Audience = _jwtConfiguration.ValidAudience,
                    SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512Signature)
                };

                if (_admins.Any(a => a.Name.Equals(username)))
                    tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, "admin"));

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(tokenHandler.WriteToken(token));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("tokens")]
        public IActionResult Post(LoginUserModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(_jwtConfiguration.IssuerSigningKey))
                    return BadRequest();

                using (var userService = new UserService(_connectionString))
                {
                    if (!userService.Verify(model.Username, model.Password))
                        return BadRequest();

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.IssuerSigningKey ?? ""));
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}