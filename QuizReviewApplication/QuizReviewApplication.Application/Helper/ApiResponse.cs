using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Helper
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse(bool success, string message, T data = default, List<string> errors = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
        }

        // Quick static helpers
        public static ApiResponse<T> SuccessResponse(string message, T data = default)
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> FailureResponse(string message, List<string> errors = null)
        {
            return new ApiResponse<T>(false, message, default, errors);
        }
    }
}
