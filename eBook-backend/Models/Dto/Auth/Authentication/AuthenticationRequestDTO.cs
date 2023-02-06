using System.ComponentModel.DataAnnotations;

namespace eBook_backend.Models.Dto.Auth.Authentication
{
    /// <summary>
    /// DTO для запроса авторизации
    /// </summary>
    public class AuthenticationRequestDto
    {
        /// <summary>
        /// E-mail пользователя, должен быть в домене @surfis.ru
        /// </summary>
        [Required(ErrorMessage = "Поле email обязательно для заполнения.")]
        [EmailAddress(ErrorMessage = "Email должен быть корректным адресом электронной почты.")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Required(ErrorMessage = "Поле password обязательно для заполнения.")]
        public string Password { get; set; }

        /// <summary>
        /// Поле отвечающее, сколько продлится сессия пользователя. True = 30 дней, False = 1 сеанс ( закрытие вкладки ), либо 1 день открытой вкладки
        /// </summary>
        [Required(ErrorMessage = "Поле isPersist обязательно.")]
        public bool IsPersist { get; set; }
    }
}
