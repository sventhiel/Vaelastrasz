﻿using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaelastrasz.Server.Configuration;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;
        private List<Admin> _admins;

        public UsersController(IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
        }

        [HttpPost("user"), Authorize(Roles = "admin")]
        public IActionResult Post(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                var userService = new UserService(_connectionString);

                var id = userService.Create(model.Name, model.Password, model.Pattern, model.AccountId);

                var user = userService.FindById(id);

                if (user == null)
                    return BadRequest();

                return Ok(ReadUserModel.Convert(user));
            }

            return BadRequest();
        }

        [HttpGet("user/{id}"), Authorize(Roles = "admin")]
        public IActionResult GetById(long id)
        {
            var userService = new UserService(_connectionString);

            var result = userService.FindById(id);

            if (result == null)
                return BadRequest("something went wrong...");

            return Ok(ReadUserModel.Convert(result));
        }

        [HttpGet("user"), Authorize(Roles = "admin")]
        public IActionResult Get()
        {
            var userService = new UserService(_connectionString);

            var result = userService.Find();

            if (result == null)
                return BadRequest("something went wrong...");

            return Ok(new List<ReadUserModel>(result.Select(u => ReadUserModel.Convert(u))));
        }

        [HttpDelete("user/{id}"), Authorize(Roles = "admin")]
        public IActionResult Delete(long id)
        {
            var userService = new UserService(_connectionString);

            var result = userService.Delete(id);

            if (result)
                return Ok($"deletion of user (id:{id}) was successful.");

            return BadRequest($"something went wrong...");
        }

        [HttpPut("user/{id}"), Authorize(Roles = "admin")]
        public IActionResult Put(long id, UpdateUserModel model)
        {
            var userService = new UserService(_connectionString);

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
}