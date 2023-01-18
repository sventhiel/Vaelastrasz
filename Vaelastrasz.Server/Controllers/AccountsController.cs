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
    public class AccountsController : ControllerBase
    {
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;
        private List<Admin> _admins;

        public AccountsController(IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
        }

        [HttpPost("account"), Authorize(Roles = "admin")]
        public IActionResult Create(CreateAccountModel model)
        {
            if (ModelState.IsValid)
            {
                //
                // Create an instance of the user service to create a new user to the database.
                var accountService = new AccountService(_connectionString);

                //
                // Call the user service with necessary properties to create a new user.
                accountService.Create(model.Name, model.Password, model.Host, model.Prefix
                    );

                //
                // After creation of the new user, redirect to the table of all users.
                return Ok();
            }

            return BadRequest();
        }
    }
}