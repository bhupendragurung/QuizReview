using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Interfaces;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Infrastructure.Services
{
    public class UserService:IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
     

        public async Task<(IdentityResult Result, ApplicationUser User)> CreateUserAsync(RegisterDto registerDto)
        {
            var user = _mapper.Map<ApplicationUser>(registerDto);
            var result= await _userManager.CreateAsync(user, registerDto.Password);
            return (result, user);
        }
        public async Task<ApplicationUser> FindByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> IsLockedOutAsync(ApplicationUser user)
        {
            return await _userManager.IsLockedOutAsync(user);
        }

        public async Task ResetAccessFailedCountAsync(ApplicationUser user)
        {
            await _userManager.ResetAccessFailedCountAsync(user);
        }

        public async Task<IdentityResult> PasswordResetAsync(ApplicationUser user, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user,token,newPassword);
        }
        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            //Generate email verification token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
        {
            return await _userManager.IsEmailConfirmedAsync(user);
        }
        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user,string token)
        {
            return await _userManager.ConfirmEmailAsync(user,token);
        }
        public async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<ApplicationUser> FindByNameAsync(string username)
        {
           return await _userManager.FindByNameAsync(username);
        }
    }
    
}
