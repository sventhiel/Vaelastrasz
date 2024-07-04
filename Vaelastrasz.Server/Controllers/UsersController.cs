﻿using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Models;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    // rdy
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
        public async Task<IActionResult> DeleteAsync(long id)
        {
            using var userService = new UserService(_connectionString);
            var response = userService.Delete(id);

            return Ok(response);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAsync()
        {
            using var userService = new UserService(_connectionString);
            var users = userService.Find();
            
            return Ok(new List<ReadUserModel>(users.Select(u => ReadUserModel.Convert(u))));
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            using var userService = new UserService(_connectionString);
            var user = userService.FindById(id);
            
            return Ok(ReadUserModel.Convert(user));
        }

        [HttpPost("users")]
        public async Task<IActionResult> PostAsync(CreateUserModel model)
        {
            using var userService = new UserService(_connectionString);

            var id = userService.Create(model.Name, model.Password, model.Project, model.Pattern, model.AccountId, true);
            var user = userService.FindById(id);

            return Created(Url.Action("GetByIdAsync", new { id = user.Id }), ReadUserModel.Convert(user));
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> PutAsync(long id, UpdateUserModel model)
        {
            using var userService = new UserService(_connectionString);

            var result = userService.Update(id, model.Password, model.Project, model.Pattern, model.AccountId, model.IsActive);
            var user = userService.FindById(id);

            return Ok(ReadUserModel.Convert(user));
        }
    }
}