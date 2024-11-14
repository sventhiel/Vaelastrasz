using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
            var result = await accountService.DeleteByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("accounts")]
        public async Task<IActionResult> GetAsync()
        {
            using var accountService = new AccountService(_connectionString);
            var result = await accountService.FindAsync();

            return Ok(new List<ReadAccountModel>(result.Select(a => ReadAccountModel.Convert(a))));
        }

        [HttpGet("accounts/{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            using var accountService = new AccountService(_connectionString);
            var result = await accountService.FindByIdAsync(id);

            return Ok(ReadAccountModel.Convert(result));
        }

        [HttpPost("accounts")]
        [SwaggerResponse(201, "Resource created successfully", typeof(ReadAccountModel))]
        public async Task<IActionResult> PostAsync(CreateAccountModel model)
        {
            using var accountService = new AccountService(_connectionString);

            var id = await accountService.CreateAsync(model.Name, model.Password, model.Host, model.Prefix, model.AccountType);
            var account = await accountService.FindByIdAsync(id);

            var request = HttpContext.Request;
            string baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            string resourceUrl = $"{baseUrl}/api/accounts/{account.Id}";

            return Created(resourceUrl, ReadAccountModel.Convert(account));
        }

        [HttpPut("accounts/{id}")]
        public async Task<IActionResult> PutByIdAsync(long id, UpdateAccountModel model)
        {
            using var accountService = new AccountService(_connectionString);

            var result = await accountService.UpdateByIdAsync(id, model.Name, model.Password, model.Host, model.Prefix);
            var account = await accountService.FindByIdAsync(id);

            return Ok(ReadAccountModel.Convert(account));
        }
    }
}