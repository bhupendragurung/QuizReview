﻿using QuizReviewApplication.Application.Helper;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace QuizReviewApplication.WebApi.Extensions
{
    public static class HttpExtension
    {
        public static void AddPaginationHeader(this HttpResponse response,PaginationHeader header)
        {
            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            response.Headers.Add("Pagination", JsonSerializer.Serialize(header, jsonOptions));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
