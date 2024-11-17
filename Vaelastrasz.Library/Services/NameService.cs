using NameParser;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Extensions;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Library.Settings;

namespace Vaelastrasz.Library.Services
{
    public class NameService
    {
        private readonly Configuration _config;
        private HttpClient _client;

        public NameService(Configuration config)
        {
            _config = config;
            _client = new HttpClient();

            _client.BaseAddress = new Uri(_config.Host);

            if (_config.Username != null && _config.Password != null)
                _client.DefaultRequestHeaders.Authorization = _config.GetBasicAuthenticationHeaderValue();

            if (_config.IgnoreNull)
                JsonConvert.DefaultSettings = () => VaelastraszJsonSerializerSettings.Settings;
        }

        public async Task<ApiResponse<HumanName>> GetByNameAsync(string name)
        {
            try
            {
                var response = await _client.PostAsync($"api/names", name.AsJson());

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<HumanName>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);

                return ApiResponse<HumanName>.Success(JsonConvert.DeserializeObject<HumanName>(await response.Content.ReadAsStringAsync()), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<HumanName>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }
    }
}