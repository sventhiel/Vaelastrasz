using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Helpers;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Authorize(Roles = "user"), Route("api")]
    public class DataCiteController : ControllerBase
    {
        private List<Admin> _admins;
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;

        public DataCiteController(IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
        }

        [HttpDelete("datacite/{doi}")]
        public async Task<IActionResult> DeleteByDOI(string doi)
        {
            if (User?.Identity?.Name == null)
                return StatusCode(400);

            var username = User.Identity.Name;

            var userService = new UserService(_connectionString);
            var accountService = new AccountService(_connectionString);

            var user = userService.FindByName(username);

            if (user == null)
                return StatusCode(400);

            if (user.Account == null)
                return StatusCode(400);

            var client = new RestClient($"{user.Account.Host}");
            client.Authenticator = new HttpBasicAuthenticator(user.Account.Name, user.Account.Password);

            var request = new RestRequest($"dois/{doi}", Method.Delete);
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK || response.Content == null)
                return BadRequest();

            return Ok(JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content));
        }

        [HttpGet("datacite")]
        public async Task<IActionResult> Get()
        {
            if (User?.Identity?.Name == null)
                return StatusCode(400);

            var username = User.Identity.Name;

            var userService = new UserService(_connectionString);
            var accountService = new AccountService(_connectionString);

            var user = userService.FindByName(username);

            if (user == null)
                return StatusCode(400);

            if (user.Account == null)
                return StatusCode(400);

            var client = new RestClient($"{user.Account.Host}");
            client.Authenticator = new HttpBasicAuthenticator(user.Account.Name, user.Account.Password);

            var request = new RestRequest($"dois", Method.Get);
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK || response.Content == null)
                return BadRequest();

            return Ok(JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content));
        }

        [HttpGet("datacite/{doi}")]
        public async Task<IActionResult> GetByDOI(string doi)
        {
            if (User?.Identity?.Name == null)
                return StatusCode(400);

            var username = User.Identity.Name;

            var userService = new UserService(_connectionString);
            var accountService = new AccountService(_connectionString);

            var user = userService.FindByName(username);

            if (user == null)
                return StatusCode(400);

            if (user.Account == null)
                return StatusCode(400);

            var client = new RestClient($"{user.Account.Host}");
            client.Authenticator = new HttpBasicAuthenticator(user.Account.Name, user.Account.Password);

            var request = new RestRequest($"dois/{doi}", Method.Get);
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK || response.Content == null)
                return BadRequest();

            return Ok(JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content));
        }

        [HttpPost("datacite")]
        public async Task<IActionResult> Post(CreateDataCiteModel model)
        {
            //
            if (User?.Identity?.Name == null)
                return BadRequest();

            var username = User.Identity.Name;

            //
            var userService = new UserService(_connectionString);
            var user = userService.FindByName(username);

            if (user == null)
                return BadRequest();

            if (user.Account == null)
                return BadRequest();

            // DOI Check
            var placeholderService = new PlaceholderService(_connectionString);
            var placeholders = placeholderService.FindByUserId(user.Id);

            if (!DOIHelper.Validate(model.Data.Attributes.Doi, user.Account.Prefix, user.Pattern, new Dictionary<string, string>(placeholders.Select(p => new KeyValuePair<string, string>(p.Expression, p.RegularExpression)))))
                return BadRequest("The DOI does not match the user pattern.");

            var client = new RestClient($"{user.Account.Host}");
            client.Authenticator = new HttpBasicAuthenticator(user.Account.Name, user.Account.Password);

            var request = new RestRequest($"dois", Method.Post).AddJsonBody(JsonConvert.SerializeObject(model));
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content != null)
            {
                var result = System.Text.Json.JsonSerializer.Deserialize<ReadDataCiteModel>(response.Content);

                var prefix = result.Data.Attributes.Doi.Split('/')[0];
                var suffix = result.Data.Attributes.Doi.Split('/')[1];

                var doiService = new DOIService(_connectionString);
                doiService.Create(prefix, suffix, user.Id);
            }

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

            if (user.Account == null)
                return BadRequest();

            var client = new RestClient($"{user.Account.Host}");
            client.Authenticator = new HttpBasicAuthenticator(user.Account.Name, user.Account.Password);

            var request = new RestRequest($"dois/{doi}", Method.Put);
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            return StatusCode((int)response.StatusCode, response.Content);
        }
    }
}