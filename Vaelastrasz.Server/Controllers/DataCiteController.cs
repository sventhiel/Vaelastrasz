using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
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

            var client = new HttpClient();

            client.BaseAddress = new Uri(user.Account.Host);

            if (user.Account.Name != null && user.Account.Password != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user.Account.Name}:{user.Account.Password}")));

            var response = await client.DeleteAsync($"dois/{doi}");

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            await doiService.DeleteByDOIAsync(doi);
            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync()));
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

            var client = new HttpClient();

            client.BaseAddress = new Uri(user.Account.Host);

            if (user.Account.Name != null && user.Account.Password != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user.Account.Name}:{user.Account.Password}")));

            foreach (var doi in dois)
            {
                var response = await client.GetAsync($"dois/{doi}?publisher=true&affiliation=true");

                if (response == null)
                    continue;

                if (!response.IsSuccessStatusCode)
                    continue;

                if (response.Content == null)
                    continue;

                var item = JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync());

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

            var client = new HttpClient();

            client.BaseAddress = new Uri(user.Account.Host);

            if (user.Account.Name != null && user.Account.Password != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user.Account.Name}:{user.Account.Password}")));

            //var request = new HttpRequestMessage(HttpMethod.Get, $"dois/{prefix}/{suffix}?publisher=true&affiliation=true");
            //request.Headers.Add("Accept", "application/json");

            var response = await client.GetAsync($"dois/{prefix}/{suffix}?publisher=true&affiliation=true");

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync()));
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

            var client = new HttpClient();

            client.BaseAddress = new Uri(user.Account.Host);

            if (user.Account.Name != null && user.Account.Password != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user.Account.Name}:{user.Account.Password}")));

            var response = await client.GetAsync($"dois/{prefix}/{suffix}?style={Request.Headers["X-Citation-Style"].ToString()}");

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
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

            var client = new HttpClient();

            client.BaseAddress = new Uri(user.Account.Host);

            if (user.Account.Name != null && user.Account.Password != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user.Account.Name}:{user.Account.Password}")));

            var request = new HttpRequestMessage(HttpMethod.Get, $"dois/{prefix}/{suffix}");
            request.Headers.Add("Accept", Request.Headers["X-Metadata-Format"].ToString());

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpPost("datacite")]
        [SwaggerResponse(201, "Resource created successfully", typeof(ReadDataCiteModel))]
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
                throw new ForbiddenException($"The doi (doi: {model.Data.Attributes.Doi}) is invalid.");

            var client = new HttpClient();

            client.BaseAddress = new Uri(user.Account.Host);

            if (user.Account.Name != null && user.Account.Password != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user.Account.Name}:{user.Account.Password}")));

            var doiId = await doiService.CreateAsync(model.Data.Attributes.Doi.GetPrefix(), model.Data.Attributes.Doi.GetSuffix(), (DOIStateType)model.Data.Attributes.Event, user.Id, JsonConvert.SerializeObject(model));

            var response = await client.PostAsync($"dois?publisher=true&affiliation=true", model.AsJson());

            if (!response.IsSuccessStatusCode)
            {
                await doiService.DeleteByIdAsync(doiId);
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            var readDataCiteModel = JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync());
            await doiService.UpdateByPrefixAndSuffixAsync(model.Data.Attributes.Doi.GetPrefix(), model.Data.Attributes.Doi.GetSuffix(), (DOIStateType)readDataCiteModel!.Data.Attributes.State, await response.Content.ReadAsStringAsync());
            return Created($"{user.Account.Host}/dois/{WebUtility.UrlEncode(model.Data.Attributes.Doi)}", JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync()));
        }

        [HttpPut("datacite/{doi}")]
        public async Task<IActionResult> PutByDOIAsync(string doi, UpdateDataCiteModel model)
        {
            using var userService = new UserService(_connectionString);
            var user = await userService.FindByNameAsync(User.Identity!.Name!);

            if (user.Account == null)
                throw new NotFoundException($"The account of user (id: {user.Id}) does not exist.");

            var client = new HttpClient();

            client.BaseAddress = new Uri(user.Account.Host);

            if (user.Account.Name != null && user.Account.Password != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user.Account.Name}:{user.Account.Password}")));

            var response = await client.PutAsync($"dois/{doi}?publisher=true&affiliation=true", model.AsJson());

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            var updatedModel = JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync());

            var doiService = new DOIService(_connectionString);
            await doiService.UpdateByDOIAsync(doi, (DOIStateType)updatedModel.Data.Attributes.State, "");

            return Ok(updatedModel);
        }
    }
}