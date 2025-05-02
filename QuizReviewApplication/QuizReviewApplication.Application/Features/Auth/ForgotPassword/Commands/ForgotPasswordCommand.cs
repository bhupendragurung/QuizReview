using MediatR;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Auth.ForgotPassword.Commands
{
    public class ForgotPasswordCommand:IRequest<ApiResponse<string>>
    {
        public ForgotPasswordDto forgotPasswordDto { get; set; }
    }
    
}
