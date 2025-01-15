using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NameParser;
using Newtonsoft.Json;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Route("api")]
    public class ConceptsController : ControllerBase
    {
        private readonly ILogger<ConceptsController> _logger;
        private readonly IWebHostEnvironment _env;

        public ConceptsController(IWebHostEnvironment env, ILogger<ConceptsController> logger)
        {
            _env = env;
            _logger = logger;
        }

        [HttpGet("concepts")]
        public async Task<IActionResult> GetAsync()
        {
            string filePath = Path.Combine(_env.WebRootPath, "concepts", "datacite.json");

            // Lesen Sie den Inhalt der Datei
            string jsonData = await System.IO.File.ReadAllTextAsync(filePath);

            return Ok(JsonConvert.DeserializeObject<ConceptModel>(jsonData));
        }
    }
}
