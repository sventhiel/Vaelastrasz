using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Extensions;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Services
{
    public class DataCiteService
    {
        private readonly Configuration _config;
        private RestClient _client;

        public DataCiteService(Configuration config)
        {
            _config = config;

            var options = new RestClientOptions(_config.Host);

            if (_config.Username != null && _config.Password != null)
            {
                options.Authenticator = new HttpBasicAuthenticator(_config.Username, _config.Password);
            };

            _client = new RestClient(options);
        }

        public async Task<ApiResponse<ReadDataCiteModel>> CreateAsync(CreateDataCiteModel model)
        {
            try
            {
                var request = new RestRequest($"api/datacite").AddJsonBody(model);
                var response = await _client.PostAsync(request);

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<ReadDataCiteModel>.Failure(response.Content, response.StatusCode);

                return ApiResponse<ReadDataCiteModel>.Success(JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDataCiteModel>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(string doi)
        {
            try
            {
                var request = new RestRequest($"api/datacite/{doi}");
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

        public async Task<ApiResponse<List<ReadDataCiteModel>>> FindAsync()
        {
            try
            {
                var request = new RestRequest($"api/datacite");
                var response = await _client.GetAsync(request);

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<List<ReadDataCiteModel>>.Failure(response.Content, response.StatusCode);

                return ApiResponse<List<ReadDataCiteModel>>.Success(JsonConvert.DeserializeObject<List<ReadDataCiteModel>>(response.Content), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ReadDataCiteModel>>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<ReadDataCiteModel>> FindByDoiAsync(string doi)
        {
            try
            {
                var request = new RestRequest($"api/datacite/{doi}");
                var response = await _client.GetAsync(request);

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<ReadDataCiteModel>.Failure(response.Content, response.StatusCode);

                return ApiResponse<ReadDataCiteModel>.Success(JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDataCiteModel>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> GetCitationStyleByDoiAsync(string doi, DataCiteCitationStyleType citationStyle)
        {
            try
            {
                var request = new RestRequest($"api/datacite/{doi}/metadata");
                request.AddHeader("X-Citation-Style", EnumExtensions.GetEnumMemberValue(citationStyle));

                var response = await _client.GetAsync(request);

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<string>.Failure(response.Content, response.StatusCode);

                return ApiResponse<string>.Success(response.Content, response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> GetMetadataFormatByDoiAsync(string doi, DataCiteMetadataFormatType metadataFormat)
        {
            try
            {
                var request = new RestRequest($"api/datacite/{doi}/metadata");
                request.AddHeader("X-Metadata-Format", EnumExtensions.GetEnumMemberValue(metadataFormat));

                var response = await _client.GetAsync(request);

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<string>.Failure(response.Content, response.StatusCode);

                return ApiResponse<string>.Success(response.Content, response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<ReadDataCiteModel>> UpdateAsync(string doi, UpdateDataCiteModel model)
        {
            try
            {
                var request = new RestRequest($"/api/datacite/{doi}").AddJsonBody(model);
                var response = await _client.PutAsync(request);

                if (!response.IsSuccessStatusCode)
                    return ApiResponse<ReadDataCiteModel>.Failure(response.Content, response.StatusCode);

                return ApiResponse<ReadDataCiteModel>.Success(JsonConvert.DeserializeObject<ReadDataCiteModel>(response.Content), response.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponse<ReadDataCiteModel>.Failure(JsonConvert.SerializeObject(ex), HttpStatusCode.InternalServerError);
            }
        }
    }
}