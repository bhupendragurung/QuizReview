﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Common.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
                Success=true;
        }
        public BaseResponse(string message)
        {
            Success = true;
            Message = message;
        }
        public BaseResponse(string message,bool success)
        {
            Success = success;
            Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string>? ValidationErrors { get; set; }
    }
}
