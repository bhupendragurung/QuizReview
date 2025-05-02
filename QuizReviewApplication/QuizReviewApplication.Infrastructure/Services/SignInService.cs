using Microsoft.AspNetCore.Identity;
using QuizReviewApplication.Application.Interfaces;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Infrastructure.Services
{
    public class SignInService : ISignInService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SignInService(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password)
        {
            return await _signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: true);
        }
    }
}
