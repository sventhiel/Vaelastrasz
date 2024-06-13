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
    public class DataCiteService
    {
        private HttpClient _client;
        private readonly Configuration _config;

        public DataCiteService(Configuration config)
        {
            _config = config;
            _client = new HttpClient();

            if (_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Remove("Authorization");

            _client.DefaultRequestHeaders.Add("Authorization", _config.GetBasicAuthorizationHeader());
        }

        public async Task<ReadDataCiteModel> CreateAsync(CreateDataCiteModel model)
        {
            try
            {
                var v = JsonConvert.SerializeObject(model);
                HttpResponseMessage response = await _client.PostAsync($"{_config.Host}/api/datacite", new StringContent(v, Encoding.UTF8, "application/json"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;

                return JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string doi)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"{_config.Host}/api/datacite/{doi}");

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    return false;

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ReadDataCiteModel>> FindAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{_config.Host}/api/datacite/");

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;

                return JsonConvert.DeserializeObject<List<ReadDataCiteModel>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ReadDataCiteModel> FindByDoiAsync(string doi)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{_config.Host}/api/datacite/{doi}");

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;

                return JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ReadDataCiteModel> UpdateAsync(string doi, UpdateDataCiteModel model)
        {
            try
            {
                HttpResponseMessage response = await _client.PutAsync($"{_config.Host}/api/datacite/{doi}", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;

                return JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}