using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<ApiResponse<T>> GetResult<T>(this HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadGateway)
            {
                return ApiResponse<T>.Failure("The server is currently not available.", HttpStatusCode.BadGateway);
            }

            if (!response.IsSuccessStatusCode)
            {
                return ApiResponse<T>.Failure(await response.Content.ReadAsStringAsync(), response.StatusCode);
            }

            return ApiResponse<T>.Success(JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()), response.StatusCode);
        }
    }
}
