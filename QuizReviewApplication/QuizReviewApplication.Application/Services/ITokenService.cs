using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(ApplicationUser user);
        Task<RefreshToken> GenerateRefreshToken(string ipAddress);
        Task<RefreshToken> RotateRefreshToken(ApplicationUser user, string currentRefreshToken, string ipAddress);
    }
}
