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
        private HttpClient _client;

        public DataCiteService(Configuration config)
        {
            _config = config;
            _client = new HttpClient();

            if (_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Remove("Authorization");

            _client.DefaultRequestHeaders.Add("Authorization", _config.GetBasicAuthorizationHeader());
        }

        public async Task<ApiResponse<ReadDataCiteModel>> CreateAsync(CreateDataCiteModel model)
        {
            try
            {
                var json = JsonConvert.SerializeObject(model);
                HttpResponseMessage response = await _client.PostAsync($"{_config.Host}/api/datacite", new StringContent(json, Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<ReadDataCiteModel>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);

                return ApiResponse<ReadDataCiteModel>.Success(JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync()), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDataCiteModel>.Failure(JsonConvert.SerializeObject(new { exception = ex.Message }), System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(string doi)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"{_config.Host}/api/datacite/{doi}");

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<bool>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);

                return ApiResponse<bool>.Success(JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync()), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Failure(JsonConvert.SerializeObject(new { exception = ex.Message }), System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<List<ReadDataCiteModel>>> FindAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{_config.Host}/api/datacite");

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<List<ReadDataCiteModel>>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);

                return ApiResponse<List<ReadDataCiteModel>>.Success(JsonConvert.DeserializeObject<List<ReadDataCiteModel>>(await response.Content.ReadAsStringAsync()), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ReadDataCiteModel>>.Failure(JsonConvert.SerializeObject(new { exception = ex.Message }), System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<ReadDataCiteModel>> FindByDoiAsync(string doi)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{_config.Host}/api/datacite/{doi}");

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<ReadDataCiteModel>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);

                return ApiResponse<ReadDataCiteModel>.Success(JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync()), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDataCiteModel>.Failure(JsonConvert.SerializeObject(new { exception = ex.Message }), System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<ReadDataCiteModel>> UpdateAsync(string doi, UpdateDataCiteModel model)
        {
            try
            {
                HttpResponseMessage response = await _client.PutAsync($"{_config.Host}/api/datacite/{doi}", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<ReadDataCiteModel>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);

                return ApiResponse<ReadDataCiteModel>.Success(JsonConvert.DeserializeObject<ReadDataCiteModel>(await response.Content.ReadAsStringAsync()), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDataCiteModel>.Failure(JsonConvert.SerializeObject(new { exception = ex.Message }), System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}