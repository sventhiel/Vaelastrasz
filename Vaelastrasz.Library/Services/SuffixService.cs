using NameParser;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Extensions;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Services
{
    public class SuffixService
    {
        private readonly Configuration _config;
        private HttpClient _client;

        public SuffixService(Configuration config)
        {
            _config = config;
            _client = new HttpClient();

            _client.BaseAddress = new Uri(_config.Host);

            if (_config.Username != null && _config.Password != null)
                _client.DefaultRequestHeaders.Authorization = _config.GetBasicAuthenticationHeaderValue();
        }

        public async Task<ApiResponse<string>> CreateAsync(CreateSuffixModel model)
        {
            try
            {
                var response = await _client.PostAsync($"api/suffixes", model.AsJson());

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<string>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);

                return ApiResponse<string>.Success(await response.Content.ReadAsStringAsync(), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }
    }
}