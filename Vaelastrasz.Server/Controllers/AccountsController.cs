using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Route("api"), Authorize(Roles = "admin")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private ConnectionString _connectionString;

        public AccountsController(ILogger<AccountsController> logger, ConnectionString connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        [HttpDelete("accounts/{id}")]
        public async Task<IActionResult> DeleteByIdAsync(long id)
        {
            using var accountService = new AccountService(_connectionString);
            var result = accountService.Delete(id);

            if (result)
                return StatusCode((int)HttpStatusCode.OK, $"The account (id:{id}) was deleted.");

            return StatusCode((int)HttpStatusCode.BadRequest, $"The account (id:{id}) was deleted.");

        }

        [HttpGet("accounts")]
        public async Task<IActionResult> GetAsync()
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

        [HttpGet("accounts/{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
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

        [HttpPost("accounts")]
        public async Task<IActionResult> PostAsync(CreateAccountModel model)
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

        [HttpPut("accounts/{id}")]
        public async Task<IActionResult> PutByIdAsync(long id, UpdateAccountModel model)
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