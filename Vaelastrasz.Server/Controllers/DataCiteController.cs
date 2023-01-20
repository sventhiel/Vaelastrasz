using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json;
using Vaelastrasz.Library.Extensions;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Library.Models.ORCID;
using Vaelastrasz.Server.Configuration;
using Vaelastrasz.Server.Models;
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

            var request = new RestRequest($"dois/{doi}", Method.Get);
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            return Ok(JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content));
        }

        [HttpPost("datacite")]
        public IActionResult Create(CreateDataCiteModel model)
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

            var json = model.Serialize();

            var request = new RestRequest($"dois", Method.Post).AddJsonBody(json);
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            return Ok(JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content));
        }

        [HttpPut("datacite/{doi}")]
        public IActionResult Put(string doi, UpdateDataCiteModel model)
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

            var request = new RestRequest($"dois/{doi}", Method.Put).AddJsonBody(model.Serialize());
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            return Ok(JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content));
        }
    }
}