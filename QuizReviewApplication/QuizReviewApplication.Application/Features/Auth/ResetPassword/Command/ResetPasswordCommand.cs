using MediatR;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Auth.ResetPassword.Command
{
    public class ResetPasswordCommand:IRequest<ApiResponse<string>>
    {
        public ResetPasswordDto resetPasswordDto { get; set; }
    }
    
   
}
