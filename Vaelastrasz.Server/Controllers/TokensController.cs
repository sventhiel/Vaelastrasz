using Exceptionless;
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
        public IActionResult Get()
        {
            try
            {
                //if (User?.Identity?.Name == null)
                //    return StatusCode((int)HttpStatusCode.Unauthorized);

                //if (string.IsNullOrEmpty(_jwtConfiguration.IssuerSigningKey))
                //    return StatusCode((int)HttpStatusCode.InternalServerError, );


                // TODO: Null checks might not be necessary due to "try"-"catch" block.
                var username = User.Identity!.Name;
                if(username == null)
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
                ex.ToExceptionless().Submit();
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
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
                            new Claim(ClaimTypes.Role, _admins.Any(a => a.Name.Equals(model.Username, StringComparison.InvariantCultureIgnoreCase)) ? "admin" : "user")
                        }),
                        Expires = DateTime.Now.AddHours(_jwtConfiguration.ValidLifetime),
                        Issuer = _jwtConfiguration.ValidIssuer,
                        Audience = _jwtConfiguration.ValidAudience,
                        SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512Signature)
                    };

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