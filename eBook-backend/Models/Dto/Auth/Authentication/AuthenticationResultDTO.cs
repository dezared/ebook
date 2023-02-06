using eBook_backend.Models.Enums;

namespace eBook_backend.Models.Dto.Auth.Authentication
{
    /// <summary>
    /// DTO для ответа авторизации
    /// </summary>
    public class AuthenticationResultDto
    {
        /// <summary>
        /// ID пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// AccessToken пользователя
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// RefreshToken пользователя
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public Role Roles { get; set; }

        /// <summary>
        /// Время жизни рефреш-токена
        /// </summary>
        public long ExpirationRefreshTokenMs { get; set; }

        /// <summary>
        /// Запомнить пользователя?
        /// </summary>
        public bool IsPersist { get; set; }
    }
}
