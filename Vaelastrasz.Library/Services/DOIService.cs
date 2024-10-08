using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Services
{
    public class DOIService
    {
        private readonly Configuration _config;
        private HttpClient _client;

        public DOIService(Configuration config)
        {
            _config = config;
            _client = new HttpClient();

            if (_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Remove("Authorization");

            _client.DefaultRequestHeaders.Add("Authorization", _config.GetBasicAuthorizationHeader());
        }

        public async Task<ApiResponse<ReadDOIModel>> CreateAsync(CreateDOIModel model)
        {
            try
            {
                var v = JsonConvert.SerializeObject(model);
                HttpResponseMessage response = await _client.PostAsync($"{_config.Host}/api/dois", new StringContent(v, Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<ReadDOIModel>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);

                return ApiResponse<ReadDOIModel>.Success(JsonConvert.DeserializeObject<ReadDOIModel>(await response.Content.ReadAsStringAsync()), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDOIModel>.Failure(ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<bool>> DeleteByIdAsync(long id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"{_config.Host}/api/dois/{id}");

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<bool>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);

                return ApiResponse<bool>.Success(JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync()), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Failure(ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<List<ReadDOIModel>>> Find()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{_config.Host}/api/dois");

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<List<ReadDOIModel>>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);

                return ApiResponse<List<ReadDOIModel>>.Success(JsonConvert.DeserializeObject<List<ReadDOIModel>>(await response.Content.ReadAsStringAsync()), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ReadDOIModel>>.Failure(ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<ReadDOIModel>> FindById(long id)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{_config.Host}/api/dois/{id}");

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<ReadDOIModel>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);

                return ApiResponse<ReadDOIModel>.Success(JsonConvert.DeserializeObject<ReadDOIModel>(await response.Content.ReadAsStringAsync()), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDOIModel>.Failure(ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> GenerateAsync(CreateSuffixModel model)
        {
            try
            {
                var response_prefix = await _client.GetAsync($"{_config.Host}/api/prefixes");
                if (!response_prefix.IsSuccessStatusCode)
                    return ApiResponse<string>.Failure(await response_prefix.Content.ReadAsStringAsync(), response_prefix.StatusCode);

                var response_suffix = await _client.PostAsJsonAsync($"{_config.Host}/api/suffixes", model);
                if (!response_suffix.IsSuccessStatusCode)
                    return ApiResponse<string>.Failure(await response_suffix.Content.ReadAsStringAsync(), response_suffix.StatusCode);

                return ApiResponse<string>.Success($"{await response_prefix.Content.ReadAsStringAsync()}/{await response_suffix.Content.ReadAsStringAsync()}", response_suffix.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Failure(ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<ReadDOIModel>> UpdateAsync(long id, UpdateDOIModel model)
        {
            try
            {
                HttpResponseMessage response = await _client.PutAsync($"{_config.Host}/api/datacite/{id}", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<ReadDOIModel>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);

                return ApiResponse<ReadDOIModel>.Success(JsonConvert.DeserializeObject<ReadDOIModel>(await response.Content.ReadAsStringAsync()), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDOIModel>.Failure(ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<ReadDOIModel>> UpdateAsync(string doi, UpdateDOIModel model)
        {
            try
            {
                HttpResponseMessage response = await _client.PutAsync($"{_config.Host}/api/datacite/{doi}", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<ReadDOIModel>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);

                return ApiResponse<ReadDOIModel>.Success(JsonConvert.DeserializeObject<ReadDOIModel>(await response.Content.ReadAsStringAsync()), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDOIModel>.Failure(ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<ReadDOIModel>> UpdateAsync(string prefix, string suffix, UpdateDOIModel model)
        {
            try
            {
                HttpResponseMessage response = await _client.PutAsync($"{_config.Host}/api/datacite/{prefix}/{suffix}", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<ReadDOIModel>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);

                return ApiResponse<ReadDOIModel>.Success(JsonConvert.DeserializeObject<ReadDOIModel>(await response.Content.ReadAsStringAsync()), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDOIModel>.Failure(ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}