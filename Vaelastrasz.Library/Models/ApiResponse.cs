using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Vaelastrasz.Library.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccessful { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T> { IsSuccessful = true, Data = data };
        }

        public static ApiResponse<T> Failure(string errorMessage)
        {
            return new ApiResponse<T> { IsSuccessful = false, ErrorMessage = errorMessage };
        }
    }
}
