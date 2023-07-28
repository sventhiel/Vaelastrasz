using Newtonsoft.Json;
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

        public async Task<ReadDataCiteModel> Create(CreateDataCiteDataModel model)
        {
            HttpResponseMessage response = await client.PostAsync($"{_config.Host}/api/datacite", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> Delete(string doi)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{_config.Host}/api/datacite/{doi}");

            if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                return false;

            return true;
        }

        public async Task<ReadDataCiteModel> FindByDoiAsync(string doi)
        {
            HttpResponseMessage response = await client.GetAsync($"{_config.Host}/api/datacite/{doi}");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<ReadDataCiteModel>> Find()
        {
            HttpResponseMessage response = await client.GetAsync($"{_config.Host}/api/datacite/");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<List<ReadDataCiteModel>>(await response.Content.ReadAsStringAsync());

        }

        public async Task<ReadDataCiteModel> Update(string doi, UpdateDataCiteModel model)
        {
            HttpResponseMessage response = await client.PutAsync($"{_config.Host}/api/datacite/{doi}", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync());
        }
    }
}