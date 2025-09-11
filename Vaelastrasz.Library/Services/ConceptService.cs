using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Services
{
    public class ConceptService
    {
        private readonly Configuration _config;
        private HttpClient _client;

        public ConceptService(Configuration config)
        {
            _config = config;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_config.Host)
            };

            _client.DefaultRequestHeaders.Add("Vaelastrasz.Library", $"{Assembly.GetExecutingAssembly().GetName().Version.ToString()}");

            if (_config.Username != null && _config.Password != null)
                _client.DefaultRequestHeaders.Authorization = _config.GetBasicAuthenticationHeaderValue();
        }

        public async Task<ApiResponse<ConceptModel>> GetConceptModelAsync()
        {
            try
            {
                var response = await _client.GetAsync($"api/concepts");

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<ConceptModel>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);

                return ApiResponse<ConceptModel>.Success(JsonConvert.DeserializeObject<ConceptModel>(await response.Content.ReadAsStringAsync()), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ConceptModel>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }
    }
}