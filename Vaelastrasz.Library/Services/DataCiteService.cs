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
        private readonly Configuration _config;
        static readonly HttpClient client = new HttpClient();

        public DataCiteService(Configuration config) 
        { 
            _config = config;
            client.DefaultRequestHeaders.Add("Authorization", _config.GetBasicAuthorizationHeader());
        }

        public CreateDataCiteModel Create()
        {
            return new CreateDataCiteModel();
        }

        public CreateDataCiteModel Create(string s)
        {
            return new CreateDataCiteModel();
        }

        public async Task<ReadDataCiteModel> FindByDoiAsync(string doi)
        {
            HttpResponseMessage response = await client.GetAsync($"{_config.Host}/api/datacite/{doi}");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync());
        }

        //public async Task<List<ReadDataCiteModel>> Find()
        //{
        //    HttpResponseMessage response = await client.GetAsync($"{_config.Host}/api/datacite/");

        //    if (response.StatusCode != System.Net.HttpStatusCode.OK)
        //        return null;

        //    return JsonConvert.DeserializeObject<ReadListDataCiteModel>(await response.Content.ReadAsStringAsync());

        //}

        public void Update()
        { }

        public void Delete()
        { }
    }
}