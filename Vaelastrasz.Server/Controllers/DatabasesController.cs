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
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", file.FileName);

            // Ensure the folder exists
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { FilePath = filePath });
        }
    }
}
