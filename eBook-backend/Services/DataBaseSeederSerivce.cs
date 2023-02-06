using eBook_backend.Models.Entities.Identify;
using eBook_backend.Models.Enums;
using eBook_backend.Repositories;
using eBook_backend.Utils.Cryptography;
using eBook_backend.Utils.JWT;
using surfis_backend.Utils.Time;

namespace eBook_backend.Services
{
    /// <summary>
    /// Сервис для заполнения данными первоначальными 
    /// </summary>
    public interface IDataBaseSeederService
    {
        /// <summary>
        /// Метод, заполняющий таблицу изначальными пользователями
        /// </summary>
        Task SeedDatabaseUserIdentify();
    }

    /// <inheritdoc />
    public class DataBaseSeederService : IDataBaseSeederService
    {
        private readonly IEntityBaseRepository<User> _userRepository;

        /// <summary>.ctor</summary>
        public DataBaseSeederService(IEntityBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc />
        public async Task SeedDatabaseUserIdentify()
        {
            var passwordHash = PasswrodHashHelper.HashWithSalt("root");

            var userListGenesis = new List<User>
            {
                new(email: "admin@ebook.com", name: "Павел", patronymic: "Павлович", surname: "Павлов", passwordSalt: passwordHash.Salt,
                    passwordHash: passwordHash.Hash, phoneNumber: "+79999999999", role: Role.Admin, refreshToken: JwtTokenWorker.GenerateRefreshToken(),
                    refreshTokenExpiryTime: DateTime.Now.AddDays(30).SetKindUtc())
            };

            foreach(var user in userListGenesis)
            {
                var search = await _userRepository.GetSingle(m => m.Email == user.Email);
                if(search == null)  _userRepository.Add(user);
            }

            await _userRepository.Commit();
        }
    }
}
