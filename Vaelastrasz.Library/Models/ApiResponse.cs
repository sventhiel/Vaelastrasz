using System.Net;

namespace Vaelastrasz.Library.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccessful { get; set; }
        public HttpStatusCode Status { get; set; }

        public static ApiResponse<T> Failure(string errorMessage, HttpStatusCode status)
        {
            // handling of specific status codes 
            switch(status)
            {
                case HttpStatusCode.BadGateway:
                    errorMessage = "Equivalent to HTTP status 502. System.Net.HttpStatusCode.BadGateway indicates that an intermediate proxy server received a bad response from another proxy or the origin server.";
                    break;

                default:
                    break;
            }

            return new ApiResponse<T> { IsSuccessful = false, ErrorMessage = errorMessage, Status = status };
        }

        public static ApiResponse<T> Success(T data, HttpStatusCode status)
        {
            return new ApiResponse<T> { IsSuccessful = true, Data = data, Status = status };
        }
    }
}