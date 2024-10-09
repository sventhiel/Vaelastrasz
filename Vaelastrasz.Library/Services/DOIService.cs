using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Services
{
    public class DOIService
    {
        private readonly Configuration _config;
        private RestClient _client;

        public DOIService(Configuration config)
        {
            _config = config;

            var options = new RestClientOptions(_config.Host);

            if (_config.Username != null && _config.Password != null)
            {
                options.Authenticator = new HttpBasicAuthenticator(_config.Username, _config.Password);
            };

            _client = new RestClient(options);
        }

        public async Task<ApiResponse<ReadDOIModel>> CreateAsync(CreateDOIModel model)
        {
            try
            {
                var request = new RestRequest($"api/dois").AddJsonBody(model);
                var response = await _client.PostAsync(request);

                if (!response.IsSuccessful)
                    return ApiResponse<ReadDOIModel>.Failure(response.Content, response.StatusCode);

                return ApiResponse<ReadDOIModel>.Success(JsonConvert.DeserializeObject<ReadDOIModel>(response.Content), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDOIModel>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<bool>> DeleteByIdAsync(long id)
        {
            try
            {
                var request = new RestRequest($"api/dois/{id}");
                var response = await _client.DeleteAsync(request);

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<bool>.Failure(response.Content, response.StatusCode);

                return ApiResponse<bool>.Success(JsonConvert.DeserializeObject<bool>(response.Content), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<List<ReadDOIModel>>> FindAsync()
        {
            try
            {
                var request = new RestRequest($"api/dois");
                var response = await _client.GetAsync(request);

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<List<ReadDOIModel>>.Failure(response.Content, response.StatusCode);

                return ApiResponse<List<ReadDOIModel>>.Success(JsonConvert.DeserializeObject<List<ReadDOIModel>>(response.Content), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ReadDOIModel>>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<ReadDOIModel>> FindByIdAsync(long id)
        {
            try
            {
                var request = new RestRequest($"/api/dois/{id}");
                var response = await _client.GetAsync(request);

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<ReadDOIModel>.Failure(response.Content, response.StatusCode);

                return ApiResponse<ReadDOIModel>.Success(JsonConvert.DeserializeObject<ReadDOIModel>(response.Content), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDOIModel>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> GenerateAsync(CreateSuffixModel model)
        {
            try
            {
                var request_prefix = new RestRequest($"/api/prefixes");
                var response_prefix = await _client.GetAsync(request_prefix);
                if (!response_prefix.IsSuccessStatusCode)
                    return ApiResponse<string>.Failure(response_prefix.Content, response_prefix.StatusCode);

                var request_suffix = new RestRequest("/api/suffixes").AddJsonBody(model);
                var response_suffix = await _client.PostAsync(request_suffix);
                if (!response_suffix.IsSuccessStatusCode)
                    return ApiResponse<string>.Failure(response_suffix.Content, response_suffix.StatusCode);

                return ApiResponse<string>.Success($"{response_prefix.Content}/{response_suffix.Content}", response_suffix.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<ReadDOIModel>> UpdateAsync(long id, UpdateDOIModel model)
        {
            try
            {
                var request = new RestRequest($"/api/datacite/{id}").AddJsonBody(model);
                var response = await _client.PutAsync(request);

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<ReadDOIModel>.Failure(response.Content, response.StatusCode);

                return ApiResponse<ReadDOIModel>.Success(JsonConvert.DeserializeObject<ReadDOIModel>(response.Content), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDOIModel>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<ReadDOIModel>> UpdateAsync(string doi, UpdateDOIModel model)
        {
            try
            {
                var request = new RestRequest($"/api/datacite/{doi}").AddJsonBody(model);
                var response = await _client.PutAsync(request);

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<ReadDOIModel>.Failure(response.Content, response.StatusCode);

                return ApiResponse<ReadDOIModel>.Success(JsonConvert.DeserializeObject<ReadDOIModel>(response.Content), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDOIModel>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<ReadDOIModel>> UpdateAsync(string prefix, string suffix, UpdateDOIModel model)
        {
            try
            {
                var request = new RestRequest($"/api/datacite/{prefix}/{suffix}").AddJsonBody(model);
                var response = await _client.PutAsync(request);

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<ReadDOIModel>.Failure(response.Content, response.StatusCode);

                return ApiResponse<ReadDOIModel>.Success(JsonConvert.DeserializeObject<ReadDOIModel>(response.Content), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDOIModel>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }
    }
}