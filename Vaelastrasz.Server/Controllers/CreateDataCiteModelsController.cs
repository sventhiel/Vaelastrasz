using LiteDB;
using MethodTimer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Vaelastrasz.Library.Extensions;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Server.Configurations;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Authorize(Roles = "user-datacite"), Route("api"), Time]
    public class CreateDataCiteModelsController : ControllerBase
    {
        private readonly ILogger<CreateDataCiteModelsController> _logger;
        private UpdateProperties _updateProperties;
        private ConnectionString _connectionString;

        public CreateDataCiteModelsController(ILogger<CreateDataCiteModelsController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _updateProperties = configuration.GetSection("UpdateProperties").Get<UpdateProperties>()!;
            _logger = logger;
        }

        [HttpPost("createdatacitemodels/prepare/{property}")]
        [SwaggerResponse(201, "Resource created successfully", typeof(CreateDataCiteModel))]
        public async Task<IActionResult> PostAsync(string property, CreateDataCiteModel model)
        {
            if (!User.IsInRole("user-datacite") || User?.Identity?.Name == null)
                return Forbid();

            return Ok(model.Update(property));
        }
    }
}
