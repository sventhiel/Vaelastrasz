using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Route("api"), Authorize(Roles = "admin")]
    public class UsersController : ControllerBase
    {
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;
        private List<Admin> _admins;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
            _logger = logger;
        }

        [HttpPost("user")]
        public IActionResult Post(CreateUserModel model)
        {
            try
            {
                using (var userService = new UserService(_connectionString))
                {
                    if (ModelState.IsValid)
                    {
                        var id = userService.Create(model.Name, model.Password, model.Pattern, model.AccountId);

                        var user = userService.FindById(id);

                        if (user == null)
                            return BadRequest();

                        return Ok(ReadUserModel.Convert(user));
                    }

                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                using (var userService = new UserService(_connectionString))
                {
                    var result = userService.FindById(id);

                    if (result == null)
                        return BadRequest("something went wrong...");

                    return Ok(ReadUserModel.Convert(result));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user")]
        public IActionResult Get()
        {
            try
            {
                using (var userService = new UserService(_connectionString))
                {
                    var result = userService.Find();

                    if (result == null)
                        return BadRequest("something went wrong...");

                    return Ok(new List<ReadUserModel>(result.Select(u => ReadUserModel.Convert(u))));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("user/{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                using (var userService = new UserService(_connectionString))
                {
                    var result = userService.Delete(id);

                    if (result)
                        return Ok($"deletion of user (id:{id}) was successful.");

                    return BadRequest($"something went wrong...");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("user/{id}")]
        public IActionResult Put(long id, UpdateUserModel model)
        {
            try
            {
                using (var userService = new UserService(_connectionString))
                {
                    var user = userService.FindById(id);

                    if (user == null)
                        return BadRequest($"something went wrong...");

                    var result = userService.Update(id, model.Name, model.Password, model.Pattern, model.AccountId);

                    if (result)
                    {
                        user = userService.FindById(id);
                        return Ok(user);
                    }

                    return BadRequest($"something went wrong...");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}