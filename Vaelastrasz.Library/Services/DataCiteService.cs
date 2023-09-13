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
        private static readonly HttpClient client = new HttpClient();
        private readonly Configuration _config;

        public DataCiteService(Configuration config)
        {
            _config = config;
            client.DefaultRequestHeaders.Add("Authorization", _config.GetBasicAuthorizationHeader());
        }

        public async Task<ReadDataCiteModel> CreateAsync(CreateDataCiteModel model)
        {
            try
            {
                var v = JsonConvert.SerializeObject(model);
                HttpResponseMessage response = await client.PostAsync($"{_config.Host}/api/datacite", new StringContent(v, Encoding.UTF8, "application/json"));

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
                HttpResponseMessage response = await client.DeleteAsync($"{_config.Host}/api/datacite/{doi}");

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
                HttpResponseMessage response = await client.GetAsync($"{_config.Host}/api/datacite/");

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
                HttpResponseMessage response = await client.GetAsync($"{_config.Host}/api/datacite/{doi}");

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
                HttpResponseMessage response = await client.PutAsync($"{_config.Host}/api/datacite/{doi}", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

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