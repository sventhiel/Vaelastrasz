using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Services
{
    public class DOIService
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly Configuration _config;

        public DOIService(Configuration config)
        {
            _config = config;
            client.DefaultRequestHeaders.Add("Authorization", _config.GetBasicAuthorizationHeader());
        }

        public async Task<ReadDataCiteModel> CreateAsync(CreateDOIModel model)
        {
            HttpResponseMessage response = await client.PostAsync($"{_config.Host}/api/dois", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> GenerateAsync(CreateSuffixModel model)
        {
            try
            {
                var response_prefix = await client.GetAsync($"{_config.Host}/api/prefixes");
                var response_suffix = await client.PostAsync($"{_config.Host}/api/suffixes", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                if (response_prefix.IsSuccessStatusCode && response_suffix.IsSuccessStatusCode)
                {
                    return $"{response_prefix.Content}/{response_suffix.Content}";
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}