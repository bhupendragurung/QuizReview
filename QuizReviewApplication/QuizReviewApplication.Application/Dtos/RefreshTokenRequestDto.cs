using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Dtos
{
    public class RefreshTokenRequestDto
    {
        public string UserId { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
