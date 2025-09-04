using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Extensions;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Library.Settings;

namespace Vaelastrasz.Library.Services
{
    public class SuffixService
    {
        private readonly Configuration _config;
        private HttpClient _client;

        public SuffixService(Configuration config)
        {
            _config = config;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_config.Host),
            };

            _client.DefaultRequestHeaders.Add("Vaelastrasz.Library", $"{Assembly.GetExecutingAssembly().GetName().Version.ToString()}");

            if (_config.Username != null && _config.Password != null)
                _client.DefaultRequestHeaders.Authorization = _config.GetBasicAuthenticationHeaderValue();


            if (_config.IgnoreNull)
                JsonConvert.DefaultSettings = () => VaelastraszJsonSerializerSettings.Settings;
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