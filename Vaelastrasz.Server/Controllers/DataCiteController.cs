using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Helpers;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    [ApiController, Authorize(Roles = "user"), Route("api")]
    public class DataCiteController : ControllerBase
    {
        private readonly ILogger<DataCiteController> _logger;
        private List<Admin> _admins;
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;

        public DataCiteController(ILogger<DataCiteController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>()!;
            _admins = configuration.GetSection("Admins").Get<List<Admin>>()!;
            _logger = logger;
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

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, response.ErrorMessage);

                // TODO: Should it return 200?
                return Ok(JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
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
                    return Unauthorized();

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

                    if (!response.IsSuccessStatusCode)
                        continue;

                    var item = JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content);

                    if (item != null)
                        result.Add(item);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
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

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, response.ErrorMessage);

                return Ok(JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
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

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, response.ErrorMessage);

                var result = JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content);

                var prefix = result.Data.Attributes.Doi.Split('/')[0];
                var suffix = result.Data.Attributes.Doi.Split('/')[1];
                var state = result.Data.Attributes.State;

                var doiService = new DOIService(_connectionString);
                doiService.Create(prefix, suffix, user.Id, (DOIStateType)state);

                // TODO:    What happens if the creation of the doi within Vaelastrasz is not working?
                //          In this case, there would be no connection between DataCite and Vaelastrasz at
                //          the beginning. Maybe it is possible to grab the information later on?
                //          Or should it abort the whole function and de-register the doi at DataCite?
                //          BUT what happens, if the DOI is already in state [registered | findable]?

                return Ok(JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
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

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, response.ErrorMessage);

                var doiService = new DOIService(_connectionString);
                doiService.Update("", "", 1);

                return Ok(JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}