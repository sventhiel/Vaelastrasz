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

        public async Task<ApiResponse<ReadDOIModel>> CreateAsync(CreateDOIModel model)
        {
            try
            {
                var v = JsonConvert.SerializeObject(model);
                HttpResponseMessage response = await _client.PostAsync($"{_config.Host}/api/dois", new StringContent(v, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return ApiResponse<ReadDOIModel>.Success(JsonConvert.DeserializeObject<ReadDOIModel>(await response.Content.ReadAsStringAsync()));
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    return ApiResponse<ReadDOIModel>.Failure($"Error: {response.StatusCode}. {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDOIModel>.Failure($"Exception: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> DeleteByIdAsync(long id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"{_config.Host}/api/dois/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return ApiResponse<bool>.Success(true);
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    return ApiResponse<bool>.Failure($"Error: {response.StatusCode}. {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Failure($"Exception: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<ReadDOIModel>>> Find()
        {
            return null;
        }

        public async Task<ApiResponse<ReadDOIModel>> FindById(long id)
        {
            return null;
        }

        public async Task<ApiResponse<string>> GenerateAsync(CreateSuffixModel model)
        {
            try
            {
                var response_prefix = await _client.GetAsync($"{_config.Host}/api/prefixes");
                var response_suffix = await _client.PostAsync($"{_config.Host}/api/suffixes", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                if (response_prefix.IsSuccessStatusCode && response_suffix.IsSuccessStatusCode)
                {
                    return ApiResponse<string>.Success($"{await response_prefix.Content.ReadAsStringAsync()}/{ await response_suffix.Content.ReadAsStringAsync()}");
                }
                else
                {
                    if(!response_prefix.IsSuccessStatusCode)
                    {
                        string errorResponse_prefix = await response_prefix.Content.ReadAsStringAsync();
                        return ApiResponse<string>.Failure($"Error: {response_prefix.StatusCode}. {errorResponse_prefix}");
                    }
                    else
                    {
                        string errorResponse_suffix = await response_suffix.Content.ReadAsStringAsync();
                        return ApiResponse<string>.Failure($"Error: {response_suffix.StatusCode}. {errorResponse_suffix}");
                    }

                }
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Failure($"Exception: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ReadDOIModel>> UpdateAsync(long id, UpdateDOIModel model)
        {
            return null;
        }
    }
}