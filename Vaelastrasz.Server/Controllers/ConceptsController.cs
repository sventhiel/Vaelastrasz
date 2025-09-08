using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vaelastrasz.Library.Models;

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

        /// <summary>
        /// Gets the current DataCite metadata schema as JSON.
        /// </summary>
        /// <returns>The current DataCite schema (v4.6) in JSON format.</returns>
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