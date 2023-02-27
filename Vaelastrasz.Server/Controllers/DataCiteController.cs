using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Server.Configuration;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class DataCiteController : ControllerBase
    {
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;
        private List<Admin> _admins;

        public DataCiteController(IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
        }

        [HttpGet("datacite/{doi}")]
        public IActionResult GetById(string doi)
        {
            if (User?.Identity?.Name == null)
                return StatusCode(400);

            var username = User.Identity.Name;

            var userService = new UserService(_connectionString);
            var accountService = new AccountService(_connectionString);

            var user = userService.FindByName(username);

            if (user == null)
                return StatusCode(400);

            var account = accountService.FindById(user.Account.Id);

            if (account == null)
                return StatusCode(400);

            var client = new RestClient($"{account.Host}");
            client.Authenticator = new HttpBasicAuthenticator(account.Name, account.Password);

            var request = new RestRequest($"dois/{doi}", Method.Get);
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            return Ok(JsonConvert.DeserializeObject(response.Content));
        }

        [HttpPost("datacite")]
        public IActionResult Create(CreateDataCiteModel model)
        {
            if (User?.Identity?.Name == null)
                return StatusCode(400);

            var username = User.Identity.Name;

            var userService = new UserService(_connectionString);
            var accountService = new AccountService(_connectionString);

            var user = userService.FindByName(username);

            if (user == null)
                return StatusCode(400);

            var account = accountService.FindById(user.Account.Id);

            if (account == null)
                return StatusCode(400);

            var client = new RestClient($"{account.Host}");
            client.Authenticator = new HttpBasicAuthenticator(account.Name, account.Password);

            var request = new RestRequest($"dois", Method.Post).AddJsonBody(System.Text.Json.JsonSerializer.Serialize(model));
            //request.AddHeader("Accept", "application/xml");

            var response = client.Execute(request);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpPut("datacite/{doi}")]
        public IActionResult Put(string doi)
        {
            if (User?.Identity?.Name == null)
                return BadRequest();

            var username = User.Identity.Name;

            var userService = new UserService(_connectionString);
            var accountService = new AccountService(_connectionString);

            var user = userService.FindByName(username);

            if (user == null)
                return BadRequest();

            var account = accountService.FindById(user.Account.Id);

            if (account == null)
                return BadRequest();

            var client = new RestClient($"{account.Host}");
            client.Authenticator = new HttpBasicAuthenticator(account.Name, account.Password);

            var request = new RestRequest($"dois/{doi}", Method.Put);
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            return StatusCode((int)response.StatusCode, response.Content);
        }
    }
}