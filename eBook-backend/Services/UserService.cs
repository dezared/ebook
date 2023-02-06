using eBook_backend.Models.Entities.Identify;
using eBook_backend.Repositories;
using eBook_backend.Utils.Cryptography;
using eBook_backend.Utils.JWT;
using surfis_backend.Utils.Time;
using System.Security.Claims;
using eBook_backend.Models.Dto;
using eBook_backend.Models.Dto.Auth.Register;

namespace eBook_backend.Services
{
    /// <summary>
    /// Сервис для работы с пользователями
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получить пользователя в контроллере
        /// </summary>
        Task<User?> GetUser();

        /// <summary>
        /// Проверяем, не существует ли уже такой пользователь?
        /// </summary>
        Task<ActionResultDto> CheckUserBeforeRegistration(RegisterUserRequestDto model);

        /// <summary>
        /// Регистрируем нового пользователя
        /// </summary>
        Task<ActionResultDto> Register(RegisterUserRequestDto model);
    }

    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly IEntityBaseRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>.ctor</summary>
        public UserService(IEntityBaseRepository<User> userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // Register

        /// <inheritdoc />
        public async Task<ActionResultDto> CheckUserBeforeRegistration(RegisterUserRequestDto model)
        {
            var search = await _userRepository.GetSingle(m => m.Email == model.Email);

            return search != null
                ? new BadResultDto(nameof(model.Email), "Пользователь с таким email уже существует.")
                : new OkResultDto<bool>(true);
        }

        /// <inheritdoc />
        public async Task<ActionResultDto> Register(RegisterUserRequestDto model)
        {
            var hashedPasswordWithSalt = PasswrodHashHelper.HashWithSalt(model.Password);

            var user = new User()
            {
                Email = model.Email,
                Patronymic = model.Patronymic,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Role = model.Role,
                Surname = model.Surname,
                PasswordHash = hashedPasswordWithSalt.Hash,
                PasswordSalt = hashedPasswordWithSalt.Salt
            };

            var refreshToken = JwtTokenWorker.GenerateRefreshToken();

            _ = int.TryParse(Environment.GetEnvironmentVariable("JwtRefreshTokenValidityInDays") ?? "1",
                out var refreshTokenValidityInDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays).SetKindUtc();

            _userRepository.Add(user);
            await _userRepository.Commit();

            return new OkResultDto<RegisterUserResultDto>(new RegisterUserResultDto()
            {
                Email = user.Email,
                Passwrod = model.Password
            });
        }


        // Get User
        /// <inheritdoc />
        public async Task<User?> GetUser()
        {
            try
            {
                var context = _httpContextAccessor.HttpContext;
                //var rawRequestBody = await context.Request.GetRawBodyAsync();
                //var tokenData = JsonConvert.DeserializeObject<TokenAuthorizationJsonModel>(rawRequestBody);

                string accessToken = context.Request.Headers["Authorization"];


                if (accessToken == null || !accessToken.StartsWith("Bearer"))
                    return null;


                accessToken = accessToken.Split(" ")[1];

                var (key, refreshToken) = context.Request.Cookies.FirstOrDefault(m => m.Key == "surfis-refresh-token");

                if ((key, refreshToken) == default)
                    return null;

                // забираем все claims с токена
                var principal = JwtTokenWorker.GetPrincipalFromExpiredToken(accessToken);

                if (principal == null) return null;

                // забираем email с claims ов
                var email = principal.Claims.First(m => m.Type == ClaimTypes.Email).Value;

                var user = await _userRepository.GetSingle(m => m.Email == email);

                if (user == null || user.RefreshToken != refreshToken ||
                    user.RefreshTokenExpiryTime <= DateTime.Now.SetKindUtc())
                    return null;

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
