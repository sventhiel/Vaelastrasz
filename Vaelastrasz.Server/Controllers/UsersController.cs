using Exceptionless;
using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NameParser;
using RestSharp;
using System.Net;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Route("api"), Authorize(Roles = "admin")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private List<Admin> _admins;
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;

        public UsersController(ILogger<UsersController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>()!;
            _admins = configuration.GetSection("Admins").Get<List<Admin>>()!;
            _logger = logger;
        }

        [HttpDelete("users/{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                using (var userService = new UserService(_connectionString))
                {
                    var response = userService.Delete(id);

                    // Regardless of the return value (true, false), give back the same http status and/or message.
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ex.ToExceptionless().Submit();
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("users")]
        public IActionResult Get()
        {
            try
            {
                using (var userService = new UserService(_connectionString))
                {
                    var users = userService.Find();

                    return Ok(new List<ReadUserModel>(users.Select(u => ReadUserModel.Convert(u))));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ex.ToExceptionless().Submit();
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("users/{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                using (var userService = new UserService(_connectionString))
                {
                    var user = userService.FindById(id);

                    if (user == null)
                        return Ok();

                    return Ok(ReadUserModel.Convert(user));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ex.ToExceptionless().Submit();
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("users")]
        public IActionResult Post(CreateUserModel model)
        {
            try
            {
                using (var userService = new UserService(_connectionString))
                {
                    if (ModelState.IsValid)
                    {
                        var id = userService.Create(model.Name, model.Password, model.Project, model.Pattern, model.AccountId);
                        var user = userService.FindById(id);

                        // TODO: This needs to be revised, in order to return proper status and message.
                        if (user == null)
                            return StatusCode((int)HttpStatusCode.InternalServerError);

                        StatusCode((int)HttpStatusCode.Created, ReadUserModel.Convert(user));
                    }

                    var errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    return BadRequest(errorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ex.ToExceptionless().Submit();
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("users/{id}")]
        public IActionResult Put(long id, UpdateUserModel model)
        {
            try
            {
                using (var userService = new UserService(_connectionString))
                {
                    if (ModelState.IsValid)
                    {
                        var result = userService.Update(id, model);
                        var user = userService.FindById(id);

                        // TODO: This needs to be revised, in order to return proper status and message.
                        if (user == null)
                            return StatusCode((int)HttpStatusCode.InternalServerError);

                        return Ok(ReadUserModel.Convert(user));
                    }

                    var errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    return BadRequest(errorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ex.ToExceptionless().Submit();
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}