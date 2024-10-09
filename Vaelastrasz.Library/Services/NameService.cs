using NameParser;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Net;
using System.Threading.Tasks;
using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Services
{
    public class NameService
    {
        private readonly Configuration _config;
        private RestClient _client;

        public NameService(Configuration config)
        {
            _config = config;

            var options = new RestClientOptions(_config.Host);

            if (_config.Username != null && _config.Password != null)
            {
                options.Authenticator = new HttpBasicAuthenticator(_config.Username, _config.Password);
            };

            _client = new RestClient(options);
        }

        public async Task<ApiResponse<HumanName>> GetByNameAsync(string name)
        {
            try
            {
                var request = new RestRequest($"api/names").AddStringBody(name, ContentType.Plain);
                var response = await _client.PostAsync(request);

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<HumanName>.Failure(response.Content, response.StatusCode);

                return ApiResponse<HumanName>.Success(JsonConvert.DeserializeObject<HumanName>(response.Content), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<HumanName>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }
    }
}