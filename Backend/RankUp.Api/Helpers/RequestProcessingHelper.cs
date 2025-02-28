using Microsoft.IdentityModel.Tokens;
using RankUpp.Api.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RankUpp.Api.Helpers
{
    public class RequestProcessingHelper
    {
        public static int? GetIdFromToken(JwtSettings jwtSettings, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.TokenKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);

            var id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);

            if (id == null)
            {
                return null;
            }

            return int.Parse(id.Value);
        }

        public static string? GetAuthTokenFromRequest(HttpRequest httpRequest)
        {
            return httpRequest.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        }
    }
}
