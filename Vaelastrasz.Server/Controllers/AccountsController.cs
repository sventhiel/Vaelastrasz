using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaelastrasz.Server.Configuration;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Route("api"), Authorize(Roles = "admin")]
    public class AccountsController : ControllerBase
    {
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;
        private List<Admin> _admins;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(ILogger<AccountsController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
        }

        [HttpPost("account")]
        public IActionResult Post(CreateAccountModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //
                    // Create an instance of the user service to create a new user to the database.
                    var accountService = new AccountService(_connectionString);

                    //
                    // Call the user service with necessary properties to create a new user.
                    var id = accountService.Create(model.Name, model.Password, model.Host, model.Prefix);

                    var account = accountService.FindById(id);

                    //
                    // After creation of the new user, redirect to the table of all users.
                    return Ok(account);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
            }
        }

        [HttpGet("account/{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                var accountService = new AccountService(_connectionString);

                var result = accountService.FindById(id);

                if (result == null)
                    return BadRequest();

                return Ok(ReadAccountModel.Convert(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            finally { }
        }

        [HttpGet("account")]
        public IActionResult Get()
        {
            var accountService = new AccountService(_connectionString);

            var result = accountService.Find();

            if (result == null)
                return BadRequest("something went wrong...");

            return Ok(new List<ReadAccountModel>(result.Select(a => ReadAccountModel.Convert(a))));
        }

        [HttpPut("account/{id}")]
        public IActionResult Put(long id, UpdateAccountModel model)
        {
            var accountService = new AccountService(_connectionString);

            var account = accountService.FindById(id);

            if (account == null)
                return BadRequest($"something went wrong...");

            var result = accountService.Update(id, model.Name, model.Password, model.Host, model.Prefix);

            if (result)
            {
                account = accountService.FindById(id);
                return Ok(account);
            }

            return BadRequest($"something went wrong...");
        }
    }
}