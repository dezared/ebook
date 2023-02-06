using eBook_backend.Models.Enums;
using eBook_backend.Models.Entites;

namespace eBook_backend.Models.Entities.Identify
{
    /// <summary>
    /// Базовый пользователь сайта, основа для осуществления входа\работы с сайтом
    /// </summary>
    public class User : BaseTrackEntity
    {
        /// <inheritdoc />
        public User()
        {
        }

        /// <inheritdoc />
        public User(string email, string name, string patronymic, string surname, string passwordHash,
            string passwordSalt, string phoneNumber, Role role, string refreshToken, DateTime refreshTokenExpiryTime)
        {
            Email = email;
            Name = name;
            Patronymic = patronymic;
            Surname = surname;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            PhoneNumber = phoneNumber;
            Role = role;
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }

        /// <summary>
        /// Электронная почта пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Хэш пароля пользователя
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Соль пароля пользователя
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Имя пользователя 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Телефонный номер пользователя
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Токен обновления пользователя
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Время жизни токена обновления пользователя
        /// </summary>
        public DateTime RefreshTokenExpiryTime { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public Role Role { get; set; }
    }
}
