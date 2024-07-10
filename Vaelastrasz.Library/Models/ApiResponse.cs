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
            return new ApiResponse<T> { IsSuccessful = false, ErrorMessage = errorMessage, Status = status };
        }

        public static ApiResponse<T> Success(T data, HttpStatusCode status)
        {
            return new ApiResponse<T> { IsSuccessful = true, Data = data, Status = status };
        }
    }
}