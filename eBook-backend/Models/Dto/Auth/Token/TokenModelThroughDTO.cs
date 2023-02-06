using System.ComponentModel.DataAnnotations;

namespace eBook_backend.Models.Dto.Auth.Token
{
    /// <summary>
    /// Модель для работы с JWT Token
    /// </summary>
    public class TokenModelThroughDto
    {
        /// <summary>
        /// Собственно сам токен
        /// </summary>
        [Required(ErrorMessage = "AccessToken обязателен.")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Токен обновления
        /// </summary>
        [Required(ErrorMessage = "RefreshToken обязателен.")]
        public string RefreshToken { get; set; }
    }
}
