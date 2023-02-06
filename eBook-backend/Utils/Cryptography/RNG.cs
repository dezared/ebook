using System.Security.Cryptography;

namespace eBook_backend.Utils.Cryptography
{
    /// <summary>
    /// Класс, для генерации случайных наборов строк
    /// </summary>
    public static class Rng
    {
        /// <summary>
        /// Сгенерировать случайную строку
        /// </summary>
        public static string GenerateRandomCryptographicKey(int keyLength)
        {
            return Convert.ToBase64String(GenerateRandomCryptographicBytes(keyLength));
        }

        /// <summary>
        /// Сгенерировать случайную строку
        /// </summary>
        public static byte[] GenerateRandomCryptographicBytes(int keyLength)
        {
            var randomNumber = new byte[keyLength];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return randomNumber;
        }
    }
}
