﻿using NameParser;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Models;

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

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        }

        public async Task<ApiResponse<HumanName>> GetByNameAsync(string name)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync($"{_config.Host}/api/names", name);

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<HumanName>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);

                return ApiResponse<HumanName>.Success(JsonConvert.DeserializeObject<HumanName>(await response.Content.ReadAsStringAsync()), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<HumanName>.Failure(JsonConvert.SerializeObject(new { exception = ex.Message }), System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}