﻿using Microsoft.AspNetCore.Mvc;
using NameParser;
using System.Net;

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
        public async Task<IActionResult> PostAsync([FromBody] string name)
        {
            return StatusCode((int)HttpStatusCode.OK, (new HumanName(name)));
        }
    }
}