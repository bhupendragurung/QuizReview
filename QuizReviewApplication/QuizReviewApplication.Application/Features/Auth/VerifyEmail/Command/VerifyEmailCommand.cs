using MediatR;
using QuizReviewApplication.Application.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Auth.VerifyEmail.Command
{
    public class VerifyEmailCommand:IRequest<ApiResponse<string>>
    {
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}
