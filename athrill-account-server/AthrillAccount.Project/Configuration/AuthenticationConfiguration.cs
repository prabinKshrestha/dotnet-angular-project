using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AthrillAccount.Project.Configuration
{
    public static class AuthenticationConfiguration
    {
        public static void AuthenticationOptionsConfiguration(this AuthenticationOptions opt)
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
        public static void JwtBearerOptionsConfiguration(this JwtBearerOptions opt, IConfiguration configuration)
        {
            var authSecretKey = Encoding.ASCII.GetBytes(configuration.GetValue<string>("ATAuthentication:ATAuthTokenSecretKey"));

            opt.RequireHttpsMetadata = false;
            opt.SaveToken = true;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(authSecretKey),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }
    }
}
