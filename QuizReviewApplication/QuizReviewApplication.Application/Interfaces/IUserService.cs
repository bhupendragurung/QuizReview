using Microsoft.AspNetCore.Identity;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Interfaces
{
    public interface IUserService
    {
        Task<(IdentityResult Result, ApplicationUser User)> CreateUserAsync(RegisterDto registerDto);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<bool> IsLockedOutAsync(ApplicationUser user);
        Task ResetAccessFailedCountAsync(ApplicationUser user);
        Task<IdentityResult> PasswordResetAsync(ApplicationUser user,string token,string newPassword);
       Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
       Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<bool> IsEmailConfirmedAsync(ApplicationUser user);
        Task<ApplicationUser> FindByNameAsync(string username);
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);
        Task<ApplicationUser> FindByIdAsync(string userId);
    }
}
