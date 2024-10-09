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
    public class SuffixService
    {
        private readonly Configuration _config;
        private RestClient _client;

        public SuffixService(Configuration config)
        {
            _config = config;

            var options = new RestClientOptions(_config.Host);

            if (_config.Username != null && _config.Password != null)
            {
                options.Authenticator = new HttpBasicAuthenticator(_config.Username, _config.Password);
            };

            _client = new RestClient(options);
        }

        public async Task<ApiResponse<string>> CreateAsync(CreateSuffixModel model)
        {
            try
            {
                var request = new RestRequest($"api/suffixes").AddJsonBody(model);
                var response = await _client.PostAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return ApiResponse<string>.Success(response.Content, response.StatusCode);
                }
                else
                {
                    return ApiResponse<string>.Failure(response.Content, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }
    }
}