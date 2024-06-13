using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Services
{
    public class DOIService
    {
        private HttpClient _client;
        private readonly Configuration _config;

        public DOIService(Configuration config)
        {
            _config = config;
            _client = new HttpClient();

            if (_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Remove("Authorization");

            _client.DefaultRequestHeaders.Add("Authorization", _config.GetBasicAuthorizationHeader());
        }

        public async Task<ReadDOIModel> CreateAsync(CreateDOIModel model)
        {
            try
            {
                var v = JsonConvert.SerializeObject(model);
                HttpResponseMessage response = await _client.PostAsync($"{_config.Host}/api/dois", new StringContent(v, Encoding.UTF8, "application/json"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;

                return JsonConvert.DeserializeObject<ReadDOIModel>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteByIdAsync(long id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"{_config.Host}/api/dois/{id}");

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    return false;

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ReadDOIModel>> Find()
        {
            return null;
        }

        public async Task<ReadDOIModel> FindById(long id)
        {
            return null;
        }

        public async Task<string> GenerateAsync(CreateSuffixModel model)
        {
            try
            {
                var response_prefix = await _client.GetAsync($"{_config.Host}/api/prefixes");
                var response_suffix = await _client.PostAsync($"{_config.Host}/api/suffixes", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                if (response_prefix.IsSuccessStatusCode && response_suffix.IsSuccessStatusCode)
                {
                    return $"{await response_prefix.Content.ReadAsStringAsync()}/{await response_suffix.Content.ReadAsStringAsync()}";
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<ReadDOIModel> UpdateAsync(long id, UpdateDOIModel model)
        {
            return null;
        }
    }
}