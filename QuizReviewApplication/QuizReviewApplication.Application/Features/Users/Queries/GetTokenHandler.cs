using MediatR;
using QuizReviewApplication.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Users.Queries
{
    public class GetTokenHandler : IRequestHandler<GetTokenQuery, string>
    {
        private readonly ITokenService _tokenService;

        public GetTokenHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public Task<string> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            return _tokenService.CreateToken();
        }
    }
}