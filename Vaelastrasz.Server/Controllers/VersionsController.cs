using LiteDB;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Route("api")]
    public class VersionsController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private ConnectionString _connectionString;

        public VersionsController(ILogger<UsersController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        [HttpGet("versions")]
        public IActionResult GetVersions()
        {
            // Beispiel für Newtonsoft.Json (ersetze dies je nach deinem Paket)
            var assembly = AppDomain.CurrentDomain.GetAssemblies().Single(a => a.GetName().Name.Equals($"Vaelastrasz.Library", StringComparison.OrdinalIgnoreCase));

            if (assembly == null)
                return NotFound("The package 'Vaelastrasz.Library' cannot be found.");

            var version_library = assembly.GetName().Version?.ToString();

            var version_application = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;

            return Ok(new
            {
                Library = version_library,
                Application = version_application
            });
        }
    }
}