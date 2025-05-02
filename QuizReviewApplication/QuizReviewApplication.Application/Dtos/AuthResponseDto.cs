using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Dtos
{
    public  class AuthResponseDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}
