using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NameParser;
using System.Data;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Route("api")]
    public class NamesController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public NamesController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }


        [HttpPost("names")]
        public async Task<IActionResult> PostAsync([FromBody]string name)
        {
            try
            {
                return Ok(new HumanName(name));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
