using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;
using Vaelastrasz.Library.Extensions;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Route("api"), Authorize(Roles = "admin")]
    [RequestSizeLimit(1_000_000)]
    [RequestFormLimits(MultipartBodyLengthLimit = 1_000_000)]
    public class DatabasesController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private ConnectionString _connectionString;

        public DatabasesController(ILogger<AccountsController> logger, ConnectionString connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        [HttpDelete("databases")]
        public async Task<IActionResult> DeleteAsync()
        {
            FileInfo database = new FileInfo(_connectionString.Filename);

            database.Delete();

            return Ok();
        }

        [HttpGet("databases")]
        public IActionResult GetAsync()
        {
            // database
            FileInfo database = new FileInfo(_connectionString.Filename);

            return File(System.IO.File.OpenRead(database.FullName), "application/octet-stream", $"{database.GetFileNameWithoutExtension()}_{DateTimeOffset.UtcNow.ToString("yyyyMMddHHmmss")}{database.GetExtension()}");
        }

        [HttpPost("databases")]
        public async Task<IActionResult> PostAsync(IFormFile file)
        {
            string databasePath = new FileInfo(_connectionString.Filename).FullName;


            // Save the file to the server
            using (var stream = new FileStream(databasePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok();
        }
    }
}
