using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizReviewApplication.Application.Services;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Infrastructure.Services
{
    public class TokenService : ITokenService
    {

        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(IConfiguration config,UserManager<ApplicationUser> userManager )
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokenkey"]));
            _userManager = userManager;
        }
        public async Task<string> CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = cred
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        }

        public async Task<RefreshToken>  GenerateRefreshToken(string ipAddress)
        {
            return  new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }
        public async Task<RefreshToken> RotateRefreshToken(ApplicationUser user, string currentRefreshToken, string ipAddress)
        {
            // Find the current refresh token
            var refreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == currentRefreshToken);

            if (refreshToken != null && refreshToken.IsActive)
            {
                // Revoke the old token
                refreshToken.Revoke(ipAddress);

                // Create a new refresh token and add it to the user's list
                var newRefreshToken = GenerateRefreshToken(ipAddress).Result;
                user.RefreshTokens.Add(newRefreshToken);

                await _userManager.UpdateAsync(user); // Save changes to the database

                return newRefreshToken;
            }

            return null; // Token is invalid or expired
        }
    }
}
