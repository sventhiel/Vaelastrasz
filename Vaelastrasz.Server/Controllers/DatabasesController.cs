using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Vaelastrasz.Library.Exceptions;
using Vaelastrasz.Library.Extensions;
using Vaelastrasz.Server.Models;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Route("api"), Authorize(Roles = "admin")]
    [RequestSizeLimit(32000000)]
    [RequestFormLimits(MultipartBodyLengthLimit = 32000000)]
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
        [SwaggerResponse(201, "Resource created successfully")]
        public async Task<IActionResult> PostAsync(IFormFile file)
        {
            if (file == null)
                throw new BadRequestException("null");

            if (file.Length == 0)
                throw new BadRequestException("0");

            // Validate file extension
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension))
                throw new BadRequestException("empty extension");

            if (extension != ".db")
                throw new BadRequestException("!= .db extension");

            var mimeTypes = new List<string> { "application/x-litedb", "application/octet-stream" };
            if (!mimeTypes.Contains(file.ContentType))
                throw new BadRequestException("mime type");

            string databasePath = new FileInfo(_connectionString.Filename).FullName;

            // Save the file to the server
            using (var stream = new FileStream(databasePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Created();
        }
    }
}