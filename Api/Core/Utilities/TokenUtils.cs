using Api.ApplicationLogic.Interface;
using Api.Core.Entities;
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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim("ID", user.Id.ToString()),
                new Claim("Email", user.Email!),
                new Claim("Phone", user.PhoneNumber!),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
            };
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: time.GetCurrentTime().AddMinutes(10),
                    audience: audience,
                    issuer: issuer,
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        // For MVC
        public static ClaimsPrincipal Validate(this string token, string issuer, string audience, string key)
        {
            IdentityModelEventSource.ShowPII = true;
            TokenValidationParameters validationParameters = new()
            {
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
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