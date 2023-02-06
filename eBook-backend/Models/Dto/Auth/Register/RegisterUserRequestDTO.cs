using System.ComponentModel.DataAnnotations;
using eBook_backend.Models.Enums;

namespace eBook_backend.Models.Dto.Auth.Register
{
    /// <summary>
    /// DTO для создания пользователя
    /// </summary>
    public class RegisterUserRequestDto
    {
        /// <summary>
        /// Название пользователя
        /// </summary>
        [EmailAddress(ErrorMessage = "Email должен быть корректным адресом электронной почты.")]
        [Required(ErrorMessage = "Поле email обязательно для заполнения.")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Required(ErrorMessage = "Поле password обязательно для заполнения.")]
        public string Password { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required(ErrorMessage = "Поле name обязательно для заполнения.")]
        public string Name { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [Required(ErrorMessage = "Поле surname обязательно для заполнения.")]
        public string Surname { get; set; }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        [Required(ErrorMessage = "Поле patronymic обязательно для заполнения.")]
        public string Patronymic { get; set; }

        /// <summary>
        /// Телефонный номер пользователя
        /// </summary>
        [Phone(ErrorMessage = "Телефон должен быть корректным номером телефона.")]
        [Required(ErrorMessage = "Поле phoneNumber обязательно для заполнения.")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        [Required(ErrorMessage = "Поле role обязательно для заполнения.")]
        public Role Role { get; set; }
    }
}
