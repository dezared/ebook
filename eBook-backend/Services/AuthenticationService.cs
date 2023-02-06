using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using eBook_backend.Models.Dto;
using eBook_backend.Models.Dto.Auth.Authentication;
using eBook_backend.Models.Dto.Auth.Token;
using eBook_backend.Models.Entities.Identify;
using eBook_backend.Repositories;
using eBook_backend.Utils.Cryptography;
using eBook_backend.Utils.JWT;
using surfis_backend.Utils.Time;

namespace eBook_backend.Services.Auth
{
    /// <summary>
    /// Сервис для работы с авторизацией
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Получить данынные авторизации пользователя
        /// </summary>
        Task<AuthenticationResultDto> Authenticate(AuthenticationRequestDto model);

        /// <summary>
        /// Проверяет, существует ли пользователь, а так же проверяет сходство паролей.
        /// </summary>
        Task<ActionResultDto> CheckUserBeforeAuthenticate(AuthenticationRequestDto model);

        /// <summary>
        /// Обновить jwt токены пользователя
        /// </summary>
        Task<ActionResultDto> RefreshToken(TokenModelThroughDto model);

        /// <summary>
        /// Выйти на стороне сервиса
        /// </summary>
        Task<ActionResultDto> LogOut(User user);
    }

    /// <inheritdoc />
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEntityBaseRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>.ctor</summary>
        public AuthenticationService(IEntityBaseRepository<User> userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc />
        public async Task<ActionResultDto> LogOut(User user)
        {
            try
            {
                var search = await _userRepository.GetSingle(m => m.Id == user.Id);

                if (search == null)
                    return new BadResultDto(nameof(user.Id), errorMessage: "Пользователь с таким Id не существует.");

                // Делаем дату жизни токена на день назад
                search.RefreshTokenExpiryTime = DateTime.Now.AddDays(-1).SetKindUtc();
                search.RefreshToken = JwtTokenWorker.GenerateRefreshToken() + "--logout";

                _userRepository.Update(search);

                await _userRepository.Commit();

                return new OkResultDto<bool>(true);
            }
            catch(Exception)
            {
                return new BadResultDto(nameof(user.Id), "Ошибка выход из сервиса.");
            }
        }

        // Refresh-Token
        /// <inheritdoc />
        public async Task<ActionResultDto> RefreshToken(TokenModelThroughDto model)
        {
            try
            {
                // забираем все claims с токена
                var principal = JwtTokenWorker.GetPrincipalFromExpiredToken(model.AccessToken);

                // забираем email с claims ов
                var email = principal.Claims.First(m => m.Type == ClaimTypes.Email).Value;

                var user = await _userRepository.GetSingle(m => m.Email == email);

                if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now.SetKindUtc())
                    return new BadResultDto(model.RefreshToken, "Ошибка обновления токена.");

                var newAccessToken = JwtTokenWorker.CreateToken(principal.Claims.ToList());
                var newRefreshToken = JwtTokenWorker.GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                _userRepository.Update(user);

                await _userRepository.Commit();

                return new OkResultDto<TokenModelThroughDto>(new TokenModelThroughDto() {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    RefreshToken = newRefreshToken 
                });
            }
            catch(Exception)
            {
                return new BadResultDto(model.RefreshToken, "Ошибка при получении данных с токена.");
            }
        }

        // Authenticate
        /// <inheritdoc />
        public async Task<ActionResultDto> CheckUserBeforeAuthenticate(AuthenticationRequestDto model)
        {
            var search = await _userRepository.GetSingle(m => m.Email == model.Email);

            if (search == null)
                return new BadResultDto(nameof(model.Email), "Пользователь с таким email не существует.");

            var hashedPassword = PasswrodHashHelper.GetHash(model.Password, search.PasswordSalt);

            return search.PasswordHash != hashedPassword 
                ? new BadResultDto(nameof(model.Password), "Введённый пароль неверен.") 
                : new OkResultDto<bool>(true);
        }

        /// <inheritdoc />
        public async Task<AuthenticationResultDto> Authenticate(AuthenticationRequestDto model)
        {
            var user = await _userRepository.GetSingle(m => m.Email == model.Email);

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Surname + " " + user.Name + " " + user.Patronymic),
                new(ClaimTypes.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.Role, user.Role.ToString())
            };

            var token = JwtTokenWorker.CreateToken(authClaims);
            var refreshToken = JwtTokenWorker.GenerateRefreshToken();

            int refreshTokenValidityInDays;

            _ = !model.IsPersist ? int.TryParse(Environment.GetEnvironmentVariable("JwtRefreshTokenValidityInDays") ?? "1", out refreshTokenValidityInDays) 
                : int.TryParse(Environment.GetEnvironmentVariable("JwtRefreshTokenValidityInDaysPersist") ?? "30", out refreshTokenValidityInDays);
            

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays).SetKindUtc();

            _userRepository.Update(user);
            await _userRepository.Commit();

            _httpContextAccessor.HttpContext.Response.Cookies.Append("ebook-refresh-token", refreshToken,
                new CookieOptions()
                {
                    SameSite = SameSiteMode.None,
                    Secure = true,
                    Expires = user.RefreshTokenExpiryTime,
                    HttpOnly = true,
                    IsEssential = true,
                    Path = "/",
                    Domain = "localhost"
                });

            return new AuthenticationResultDto()
            {
                Id = user.Id,
                RefreshToken = refreshToken,
                Roles = user.Role,
                ExpirationRefreshTokenMs = refreshTokenValidityInDays * 24L * 60L * 60L * 1000L,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                IsPersist = model.IsPersist
            };
        }
    }
}