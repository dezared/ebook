namespace eBook_backend.Models.Dto.Auth.Register
{
    /// <summary>
    /// DTO Результат создания пользователя
    /// </summary>
    public class RegisterUserResultDto
    {
        /// <summary>
        /// Емейл зарегистрированного пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль зарегистрированного пользователя
        /// </summary>
        public string Passwrod { get; set; }
    }
}
