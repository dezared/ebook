using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eBook_backend.Utils.JWT
{
    /// <summary>
    /// Класс для работы с JWT
    /// </summary>
    public static class JwtTokenWorker
    {
        /// <summary>
        /// Создать JWT Token на основе списка Claim's
        /// </summary>
        public static JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtKey") ?? "_test_string_jwt"));
            _ = int.TryParse(Environment.GetEnvironmentVariable("JwtTokenValidityInMinutes") ?? "1", out var tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("Jwt_ValidIssuer") ?? "localhost",
                audience: Environment.GetEnvironmentVariable("Jwt_ValidAudience") ?? "localhost",
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        /// <summary>
        /// Сгенерировать refresh token
        /// </summary>
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Получить principal данные с токена ( расшифровать на claim )
        /// </summary>
        public static ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtKey") ?? "_test_string_jwt"))
            };

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
            catch (Exception e)
            {
                Console.WriteLine("[Authorization Bearer] " + e.Message);
                return null;
            }
        }
    }
}
