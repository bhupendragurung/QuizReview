using Microsoft.AspNetCore.Identity;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Interfaces
{
    public interface ISignInService
    {
        Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password);
    }
}
