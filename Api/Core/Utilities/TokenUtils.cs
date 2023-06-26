using Api.Core.Entities;
using Api.Infrastructure.IService;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Api.Core.Utilities
{
    public static class TokenUtils
    {
        public static string Authenticate(this User user, IList<string> roles, string issuer, string audience, string key, ICurrentTime time)
        {
            var tokenClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim(ClaimTypes.Name, user.UserName ?? ""),
        };
            foreach (var role in roles)
                tokenClaims.Add(new Claim(ClaimTypes.Role, role));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var tokenCredentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(tokenClaims),
                Issuer = issuer,
                Audience = audience,
                Expires = time.GetCurrentTime().AddMinutes(10),
                SigningCredentials = tokenCredentials,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        // For MVC
        public static ClaimsPrincipal Validate(this string token, string issuer, string audience, string key)
        {
            IdentityModelEventSource.ShowPII = true;
            TokenValidationParameters validationParameters = new()
            {
                ValidateLifetime = true,
                ValidAudience = audience,
                ValidIssuer = issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };

            var principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);

            return principal;
        }
        public static DateTime GetExpireDate(this string token, ICurrentTime currentTime)
        {
            JwtSecurityToken jwt = new(token);
            if (string.IsNullOrEmpty(token) is false)
                return currentTime.GetCurrentTime();
            return jwt.ValidTo.ToUniversalTime();
        }
    }
}