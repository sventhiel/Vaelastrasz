using LiteDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Vaelastrasz.Server.Configuration;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private ConnectionString _connectionString;
        private List<Admin> _admins;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration configuration,
            ConnectionString connectionString) : base(options, logger, encoder, clock)
        {
            _connectionString = connectionString;
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring("Basic ".Length).Trim();
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split(':');

                //
                // Admin
                var admin = _admins.Find(a => a.Name.Equals(credentials[0]));

                if (admin != null && admin.Password.Equals(credentials[1]))
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, credentials[0]),
                        new Claim(ClaimTypes.Role, "admin"),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var identity = new ClaimsIdentity(claims, "Basic");
                    var claimsPrincipal = new ClaimsPrincipal(identity);
                    return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
                }

                var userService = new UserService(_connectionString);

                if (userService.Verify(credentials[0], credentials[1]))
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, credentials[0]) };
                    var identity = new ClaimsIdentity(claims, "Basic");
                    var claimsPrincipal = new ClaimsPrincipal(identity);
                    return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
                }

                Response.StatusCode = 401;
                Response.Headers.Add("www-authenticate", "Basic realm=\"dotnetthoughts.net\"");
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
            else
            {
                Response.StatusCode = 401;
                Response.Headers.Add("www-authenticate", "Basic realm=\"dotnetthoughts.net\"");
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
        }
    }
}