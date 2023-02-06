using System.Security.Cryptography;
using System.Text;

namespace eBook_backend.Utils.Cryptography
{
    /// <summary>
    /// Класс который хэширует и проверяет пароли
    /// </summary>
    public static class PasswrodHashHelper
    {
        /// <summary>
        /// Получить захэшированные данные о пароле
        /// </summary>
        public static HashWithSaltResult HashWithSalt(string password, int saltLength = 64)
        {
            var hashAlgo = SHA256.Create();

            var saltBytes = Rng.GenerateRandomCryptographicBytes(saltLength);
            var passwordAsBytes = Encoding.UTF8.GetBytes(password);

            var passwordWithSaltBytes = new List<byte>();

            passwordWithSaltBytes.AddRange(passwordAsBytes);
            passwordWithSaltBytes.AddRange(saltBytes);

            var digestBytes = hashAlgo.ComputeHash(passwordWithSaltBytes.ToArray());
            return new HashWithSaltResult(Convert.ToBase64String(saltBytes), Convert.ToBase64String(digestBytes));
        }

        /// <summary>
        /// Захэшировать пароль с солью, для проверок
        /// </summary>
        public static string GetHash(string password, string salt)
        {
            var hashAlgo = SHA256.Create();

            var passwordAsBytes = Encoding.UTF8.GetBytes(password);
            var saltBytes = Convert.FromBase64String(salt);
            var passwordWithSaltBytes = new List<byte>();

            passwordWithSaltBytes.AddRange(passwordAsBytes);
            passwordWithSaltBytes.AddRange(saltBytes);

            var digestBytes = hashAlgo.ComputeHash(passwordWithSaltBytes.ToArray());
            return Convert.ToBase64String(digestBytes);
        }
    }

    /// <summary>
    /// Объект хэша пароля с сполью
    /// </summary>
    public class HashWithSaltResult
    {
        /// <summary>
        /// Соль пароля
        /// </summary>
        public string Salt { get; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        public string Hash { get; set; }

        /// <summary>.ctor</summary>
        public HashWithSaltResult(string salt, string hash)
        {
            Salt = salt;
            Hash = hash;
        }
    }
}
