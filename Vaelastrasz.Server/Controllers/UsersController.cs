using LiteDB;
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
    public class UsersController : ControllerBase
    {
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;
        private List<Admin> _admins;

        public UsersController(IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
        }

        //[Authorize(Roles = "admin")]
        //[HttpPost]
        //public IActionResult Create(CreateUserModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //
        //        // Create an instance of the user service to create a new user to the database.
        //        var userService = new UserService(_connectionString);

        //        //
        //        // Call the user service with necessary properties to create a new user.
        //        userService.Create(model.Name, model.Password, model.Pattern, model.AccountId);

        //        //
        //        // After creation of the new user, redirect to the table of all users.
        //        return Ok();
        //    }

        //    return BadRequest();
        //}

        [HttpPost]
        public IActionResult Login(LoginUserModel model)
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

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }

            return BadRequest();
        }
    }
}