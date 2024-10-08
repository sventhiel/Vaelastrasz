using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Web;
using Vaelastrasz.Library.Exceptions;
using Vaelastrasz.Library.Extensions;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Server.Attributes;
using Vaelastrasz.Server.Configurations;
using Vaelastrasz.Server.Helpers;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController, Authorize(Roles = "user-datacite"), Route("api")]
    public class DataCiteController : ControllerBase
    {
        private readonly ILogger<DataCiteController> _logger;
        private List<Admin> _admins;
        private ConnectionString _connectionString;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="connectionString"></param>
        public DataCiteController(ILogger<DataCiteController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _admins = configuration.GetSection("Admins").Get<List<Admin>>()!;
            _logger = logger;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="doi"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [HttpDelete("datacite/{doi}")]
        public async Task<IActionResult> DeleteByDOIAsync(string doi)
        {
            using var doiService = new DOIService(_connectionString);
            using var userService = new UserService(_connectionString);
            var user = await userService.FindByNameAsync(User.Identity!.Name!);

            if (user.Account == null)
                throw new NotFoundException($"The account of user (id: {user.Id}) does not exist.");

            var clientOptions = new RestClientOptions($"{user.Account.Host}")
            {
                Authenticator = new HttpBasicAuthenticator(user.Account.Name, user.Account.Password)
            };

            var client = new RestClient(clientOptions);

            var request = new RestRequest($"dois/{doi}", Method.Delete);
            request.AddHeader("Accept", "application/json");

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, response.ErrorMessage);

            await doiService.DeleteByDOIAsync(doi);
            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content!));
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [HttpGet("datacite")]
        public async Task<IActionResult> GetAsync()
        {
            using var userService = new UserService(_connectionString);
            var user = await userService.FindByNameAsync(User.Identity!.Name!);

            if (user.Account == null)
                throw new NotFoundException($"The account of user (id: {user.Id}) does not exist.");

            var doiService = new DOIService(_connectionString);
            var dois = await doiService.FindByUserIdAsync(user.Id);

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
                var request = new RestRequest($"dois/{doi}?publisher=true&affiliation=true", Method.Get);
                request.AddHeader("Accept", "application/json");

                var response = await client.ExecuteAsync(request);

                if (response == null)
                    continue;

                if (!response.IsSuccessStatusCode)
                    continue;

                if (response.Content == null)
                    continue;

                var item = JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content);

                if (item != null)
                    result.Add(item);
            }

            return Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="UnauthorizedException"></exception>
        [HttpGet("datacite/{prefix}/{suffix}")]
        public async Task<IActionResult> GetByPrefixAndSuffixAsync(string prefix, string suffix)
        {
            using var userService = new UserService(_connectionString);
            var user = await userService.FindByNameAsync(User.Identity!.Name!);

            if (user.Account == null)
                throw new NotFoundException($"The account of user (id: {user.Id}) does not exist.");

            using var doiService = new DOIService(_connectionString);
            var doi = await doiService.FindByPrefixAndSuffixAsync(prefix, suffix);

            if (doi.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action.");

            var clientOptions = new RestClientOptions($"{user.Account.Host}")
            {
                Authenticator = new HttpBasicAuthenticator(user.Account.Name, user.Account.Password)
            };

            var client = new RestClient(clientOptions);

            var request = new RestRequest($"dois/{prefix}/{suffix}?publisher=true&affiliation=true", Method.Get);
            request.AddHeader("Accept", "application/json");

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, response.ErrorMessage);

            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content!));
        }

        [HttpGet("datacite/{prefix}/{suffix}/citations")]
        [SwaggerCustomHeader("X-Citation-Style", ["apa", "harvard-cite-them-right", "modern-language-association", "vancouver", "chicago-fullnote-bibliography", "ieee"])]
        public async Task<IActionResult> GetCitationStyleByPrefixAndSuffixAsync(string prefix, string suffix)
        {
            using var userService = new UserService(_connectionString);
            var user = await userService.FindByNameAsync(User.Identity!.Name!);

            if (user.Account == null)
                throw new NotFoundException($"The account of user (id: {user.Id}) does not exist.");

            using var doiService = new DOIService(_connectionString);
            var doi = await doiService.FindByPrefixAndSuffixAsync(prefix, suffix);

            if (doi.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action.");

            var clientOptions = new RestClientOptions($"{user.Account.Host}")
            {
                Authenticator = new HttpBasicAuthenticator(user.Account.Name, user.Account.Password)
            };

            var client = new RestClient(clientOptions);

            var request = new RestRequest($"dois/{prefix}/{suffix}?style={Request.Headers["X-Citation-Style"].ToString()}", Method.Get);

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, response.ErrorMessage);

            return StatusCode((int)response.StatusCode, response.Content!);
        }

        [HttpGet("datacite/{prefix}/{suffix}/metadata")]
        [SwaggerCustomHeader("X-Metadata-Format", ["application/x-research-info-systems", "application/x-bibtex", "application/vnd.jats+xml", "application/vnd.codemeta.ld+json", "application/vnd.citationstyles.csl+json", "application/vnd.schemaorg.ld+json", "application/vnd.datacite.datacite+json", "application/vnd.datacite.datacite+xml"])]
        public async Task<IActionResult> GetMetadataFormatByPrefixAndSuffixAsync(string prefix, string suffix)
        {
            using var userService = new UserService(_connectionString);
            var user = await userService.FindByNameAsync(User.Identity!.Name!);

            if (user.Account == null)
                throw new NotFoundException($"The account of user (id: {user.Id}) does not exist.");

            using var doiService = new DOIService(_connectionString);
            var doi = await doiService.FindByPrefixAndSuffixAsync(prefix, suffix);

            if (doi.User.Id != user.Id)
                throw new UnauthorizedException($"The user (id: {user.Id}) is not allowed to perform the action.");

            var clientOptions = new RestClientOptions($"{user.Account.Host}")
            {
                Authenticator = new HttpBasicAuthenticator(user.Account.Name, user.Account.Password)
            };

            var client = new RestClient(clientOptions);

            var request = new RestRequest($"dois/{prefix}/{suffix}", Method.Get);
            request.AddHeader("Accept", Request.Headers["X-Metadata-Format"].ToString());

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, response.ErrorMessage);

            return StatusCode((int)response.StatusCode, response.Content!);
        }

        [HttpPost("datacite")]
        public async Task<IActionResult> PostAsync(CreateDataCiteModel model)
        {
            using var doiService = new DOIService(_connectionString);
            using var placeholderService = new PlaceholderService(_connectionString);
            using var userService = new UserService(_connectionString);

            var user = await userService.FindByNameAsync(User.Identity!.Name!);
            if (user.Account == null)
                throw new NotFoundException($"The account of user (id: {user.Id}) does not exist.");

            // DOI Check
            var placeholders = await placeholderService.FindByUserIdAsync(user.Id);
            if (!DOIHelper.Validate(model.Data.Attributes.Doi, user.Account.Prefix, user.Pattern, new Dictionary<string, string>(placeholders.Select(p => new KeyValuePair<string, string>(p.Expression, p.RegularExpression)))))
                throw new ForbidException($"The doi (doi: {model.Data.Attributes.Doi}) is invalid.");

            var clientOptions = new RestClientOptions($"https://api.test.datacite.org") //{user.Account.Host}")
            {
                Authenticator = new HttpBasicAuthenticator(user.Account.Name, user.Account.Password)
            };

            var client = new RestClient(clientOptions);

            var request = new RestRequest($"dois?publisher=true&affiliation=true", Method.Post).AddJsonBody(JsonConvert.SerializeObject(model));
            request.AddHeader("Accept", "application/json");

            var doiId = await doiService.CreateAsync(model.Data.Attributes.Doi.GetPrefix(), model.Data.Attributes.Doi.GetSuffix(), (DOIStateType)model.Data.Attributes.Event, user.Id, JsonConvert.SerializeObject(model));

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                await doiService.DeleteByIdAsync(doiId);
                return StatusCode((int)response.StatusCode, response.ErrorMessage);
            }

            var readDataCiteModel = JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content!);
            await doiService.UpdateByPrefixAndSuffixAsync(model.Data.Attributes.Doi.GetPrefix(), model.Data.Attributes.Doi.GetSuffix(), (DOIStateType)readDataCiteModel!.Data.Attributes.State, response.Content!);
            return Created($"{user.Account.Host}/dois/{WebUtility.UrlEncode(model.Data.Attributes.Doi)}", JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content!));
        }

        [HttpPut("datacite/{doi}")]
        public async Task<IActionResult> PutByDOIAsync(string doi, UpdateDataCiteModel model)
        {
            using var userService = new UserService(_connectionString);
            var user = await userService.FindByNameAsync(User.Identity!.Name!);

            if (user.Account == null)
                throw new NotFoundException($"The account of user (id: {user.Id}) does not exist.");

            var clientOptions = new RestClientOptions($"{user.Account.Host}")
            {
                Authenticator = new HttpBasicAuthenticator(user.Account.Name, user.Account.Password)
            };

            var client = new RestClient(clientOptions);

            var request = new RestRequest($"dois/{doi}?publisher=true&affiliation=true", Method.Put);
            request.AddHeader("Accept", "application/json");

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, response.ErrorMessage);

            var doiService = new DOIService(_connectionString);
            await doiService.UpdateByDOIAsync(doi, DOIStateType.Findable, "");

            return Ok(JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content!));
        }
    }
}