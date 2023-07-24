using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Route("api"), Authorize(Roles = "admin")]
    public class AccountsController : ControllerBase
    {
        private ConnectionString _connectionString;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(ILogger<AccountsController> logger, ConnectionString connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        [HttpPost("accounts")]
        public IActionResult Post(CreateAccountModel model)
        {
            try
            {
                using (var accountService = new AccountService(_connectionString))
                {
                    if (ModelState.IsValid)
                    {
                        var id = accountService.Create(model.Name, model.Password, model.Host, model.Prefix);
                        var account = accountService.FindById(id);
                        return Ok(account);
                    }

                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("accounts/{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                using (var accountService = new AccountService(_connectionString))
                {
                    var result = accountService.FindById(id);

                    if (result == null)
                        return BadRequest();

                    return Ok(ReadAccountModel.Convert(result));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("accounts")]
        public IActionResult Get()
        {
            try
            {
                using (var accountService = new AccountService(_connectionString))
                {
                    var result = accountService.Find();

                    if (result == null)
                    {
                        _logger.LogInformation("accountService.Find() returned null.");
                        return BadRequest("something went wrong...");
                    }

                    return Ok(new List<ReadAccountModel>(result.Select(a => ReadAccountModel.Convert(a))));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("accounts/{id}")]
        public IActionResult Put(long id, UpdateAccountModel model)
        {
            try
            {
                using (var accountService = new AccountService(_connectionString))
                {
                    var account = accountService.FindById(id);

                    if (account == null)
                        return BadRequest();

                    if (ModelState.IsValid)
                    {
                        var result = accountService.Update(id, model.Name, model.Password, model.Host, model.Prefix);

                        if (result)
                        {
                            account = accountService.FindById(id);
                            return Ok(account);
                        }
                    }

                    return BadRequest();
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