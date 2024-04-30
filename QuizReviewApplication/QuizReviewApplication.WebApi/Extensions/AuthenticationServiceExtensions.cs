using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace QuizReviewApplication.WebApi.Extensions
{
    public  static class AuthenticationServiceExtensions
    {
        public static IServiceCollection TokenAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokenkey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                
                });
            return services;

        }
    }
}
