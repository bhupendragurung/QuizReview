using MediatR;
using QuizReviewApplication.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Users.Queries
{
    public class GetTokenHandler 
    {
        private readonly ITokenService _tokenService;

        public GetTokenHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
       
    }
}