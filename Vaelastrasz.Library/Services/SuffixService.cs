using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vaelastrasz.Library.Configurations;
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

            if (_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Remove("Authorization");

            _client.DefaultRequestHeaders.Add("Authorization", _config.GetBasicAuthorizationHeader());
        }

        public async Task<ApiResponse<string>> CreateAsync(CreateSuffixModel model)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsync($"{_config.Host}/api/suffixes", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return ApiResponse<string>.Success(await response.Content.ReadAsStringAsync(), response.StatusCode);
                }
                else
                {
                    return ApiResponse<string>.Failure(await response.Content.ReadAsStringAsync(), System.Net.HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Failure(JsonConvert.SerializeObject(new { exception = ex.Message }), System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}