using LiteDB;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("user"), Authorize(Roles = "admin")]
        public IActionResult Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                //
                // Create an instance of the user service to create a new user to the database.
                var userService = new UserService(_connectionString);

                //
                // Call the user service with necessary properties to create a new user.
                userService.Create(model.Name, model.Password, model.Pattern, model.AccountId);

                //
                // After creation of the new user, redirect to the table of all users.
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("user/{id}"), Authorize(Roles = "admin")]
        public IActionResult Get(long id)
        {
            var userService = new UserService(_connectionString);

            var result = userService.FindById(id);

            if (result == null)
                return BadRequest("something went wrong...");

            return Ok(ReadUserModel.Convert(result));
        }

        [HttpDelete("user/{id}"), Authorize(Roles = "admin")]
        public IActionResult Delete(long id)
        {
            var userService = new UserService(_connectionString);

            var result = userService.Delete(id);

            if (result)
                return Ok($"deletion of user (id:{id}) was successful.");

            return BadRequest($"something went wrong...");
        }

        [HttpPut("user/{id}"), Authorize(Roles = "admin")]
        public IActionResult Put(UpdateUserModel model)
        {
            var userService = new UserService(_connectionString);
            var accountService = new AccountService(_connectionString);

            var user = userService.FindById(model.Id);

            if (user == null)
                return BadRequest($"something went wrong...");

            user.Account = accountService.FindById(model.AccountId);

            var result = userService.Update(user);

            if(result)
                return Ok($"update of user (id:{user.Id}) was successful.");

            return BadRequest($"something went wrong...");
        }

        [HttpPost("login")]
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