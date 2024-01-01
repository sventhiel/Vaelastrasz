using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        public async Task<IActionResult> DeleteByDOIAsync(string doi)
        {
            try
            {
                using var userService = new UserService(_connectionString);
                var user = userService.FindByName(User?.Identity?.Name);

                if (user == null)
                    return Unauthorized();

                if (user.Account == null)
                    return Forbid();

                var clientOptions = new RestClientOptions($"{user.Account.Host}")
                {
                    Authenticator = new HttpBasicAuthenticator(user.Account.Name, user.Account.Password)
                };

                var client = new RestClient(clientOptions);

                var request = new RestRequest($"dois/{doi}", Method.Delete);
                request.AddHeader("Accept", "application/json");

                var response = client.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK || response.Content == null)
                    return BadRequest();

                return Ok(JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content));
            }
            catch (Exception ex)
            {
                // TODO: exception handling
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("datacite")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                using var userService = new UserService(_connectionString);
                var user = userService.FindByName(User?.Identity?.Name);

                if (user == null)
                    return Unauthorized(); ;

                if (user.Account == null)
                    return Forbid();

                var doiService = new DOIService(_connectionString);
                var dois = doiService.FindByUserId(user.Id);

                var result = new List<ReadDataCiteModel>();

                if (dois.Count == 0)
                    return Ok(result);

                var clientOptions = new RestClientOptions($"{user.Account.Host}")
                {
                    Authenticator = new HttpBasicAuthenticator(user.Account.Name, user.Account.Password)
                };

                var client = new RestClient(clientOptions);

                foreach (var doi in dois)
                {
                    var request = new RestRequest($"dois/{doi}", Method.Get);
                    request.AddHeader("Accept", "application/json");

                    var response = client.Execute(request);

                    if (response.StatusCode != System.Net.HttpStatusCode.OK || response.Content == null)
                        continue;

                    var item = JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content);

                    if (item != null)
                        result.Add(item);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // TODO: exception handling
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("datacite/{prefix}/{suffix}")]
        public async Task<IActionResult> GetByDOIAsync(string prefix, string suffix)
        {
            try
            {
                using var userService = new UserService(_connectionString);
                var user = userService.FindByName(User?.Identity?.Name);

                if (user == null)
                    return Unauthorized(); ;

                if (user.Account == null)
                    return Forbid();

                using var doiService = new DOIService(_connectionString);
                //if (doiService.FindByDOI(prefix, suffix)?.User.Id != user.Id)
                //    return Forbid();

                var clientOptions = new RestClientOptions($"{user.Account.Host}")
                {
                    Authenticator = new HttpBasicAuthenticator(user.Account.Name, user.Account.Password)
                };

                var client = new RestClient(clientOptions);

                var request = new RestRequest($"dois/{prefix}/{suffix}", Method.Get);
                request.AddHeader("Accept", "application/json");

                var response = client.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK || response.Content == null)
                    return BadRequest();

                return Ok(JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content));
            }
            catch (Exception ex)
            {
                // TODO: exception handling
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("datacite")]
        public async Task<IActionResult> PostAsync(CreateDataCiteModel model)
        {
            try
            {
                using var userService = new UserService(_connectionString);
                var user = userService.FindByName(User?.Identity?.Name);

                if (user == null)
                    return Unauthorized();

                if (user.Account == null)
                    return BadRequest();

                // DOI Check
                var placeholderService = new PlaceholderService(_connectionString);
                var placeholders = placeholderService.FindByUserId(user.Id);

                if (!DOIHelper.Validate(model.Data.Attributes.Doi, user.Account.Prefix, user.Pattern, new Dictionary<string, string>(placeholders.Select(p => new KeyValuePair<string, string>(p.Expression, p.RegularExpression)))))
                    return BadRequest("The DOI does not match the user pattern.");

                var clientOptions = new RestClientOptions($"{user.Account.Host}")
                {
                    Authenticator = new HttpBasicAuthenticator(user.Account.Name, user.Account.Password)
                };

                var client = new RestClient(clientOptions);

                var request = new RestRequest($"dois", Method.Post).AddJsonBody(JsonConvert.SerializeObject(model));
                request.AddHeader("Accept", "application/json");

                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content != null)
                {
                    var result = System.Text.Json.JsonSerializer.Deserialize<ReadDataCiteModel>(response.Content);

                    var prefix = result.Data.Attributes.Doi.Split('/')[0];
                    var suffix = result.Data.Attributes.Doi.Split('/')[1];
                    var state = result.Data.Attributes.State;

                    var doiService = new DOIService(_connectionString);
                    doiService.Create(prefix, suffix, user.Id, (DOIStateType)state);
                }

                return StatusCode((int)response.StatusCode, response.Content);
            }
            catch (Exception ex)
            {
                // TODO: exception handling
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("datacite/{doi}")]
        public async Task<IActionResult> PutByDOIAsync(string doi, UpdateDataCiteModel model)
        {
            try
            {
                using var userService = new UserService(_connectionString);
                var user = userService.FindByName(User?.Identity?.Name);

                if (user == null)
                    return Unauthorized();

                if (user.Account == null)
                    return BadRequest();

                var clientOptions = new RestClientOptions($"{user.Account.Host}")
                {
                    Authenticator = new HttpBasicAuthenticator(user.Account.Name, user.Account.Password)
                };

                var client = new RestClient(clientOptions);

                var request = new RestRequest($"dois/{doi}", Method.Put);
                request.AddHeader("Accept", "application/json");

                var response = client.Execute(request);

                return StatusCode((int)response.StatusCode, response.Content);
            }
            catch (Exception ex)
            {
                // TODO: exception handling
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}