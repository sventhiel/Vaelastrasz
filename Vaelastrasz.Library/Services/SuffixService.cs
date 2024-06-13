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
        private HttpClient _client;
        private readonly Configuration _config;

        public SuffixService(Configuration config)
        {
            _config = config;
            _client = new HttpClient();

            if (_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Remove("Authorization");

            _client.DefaultRequestHeaders.Add("Authorization", _config.GetBasicAuthorizationHeader());
        }

        public async Task<string> CreateAsync(CreateSuffixModel model)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsync($"{_config.Host}/api/suffixes", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;

                return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}