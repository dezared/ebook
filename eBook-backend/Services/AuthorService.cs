using eBook_backend.Models.Dto.Library;
using eBook_backend.Models.Dto;
using eBook_backend.Models.Entites;
using eBook_backend.Repositories;

namespace eBook_backend.Services
{
    public interface IAuthorService
    {
        /// <summary>
        /// Получить одну книгу
        /// </summary>
        Task<ActionResultDto> GetSingleAuthor(Guid id);

        /// <summary>
        /// Получить все книги
        /// </summary>
        Task<ActionResultDto> GetAllAuthors();

        /// <summary>
        /// Обновить книгу
        /// </summary>
        Task<ActionResultDto> UpdateAuthor(EditAuthorRequestDTO model);

        /// <summary>
        /// Добавить книгу
        /// </summary>
        Task<ActionResultDto> AddAuthor(AddAuthorRequestDTO model);

        /// <summary>
        /// Удалить книгу
        /// </summary>
        Task<ActionResultDto> RemoveAuthor(Guid id);
    }

    public class AuthorService : IAuthorService
    {
        private readonly IEntityBaseRepository<Author> _authorServices;

        public AuthorService(IEntityBaseRepository<Author> authorServices)
        {
            _authorServices = authorServices;
        }

        public async Task<ActionResultDto> AddAuthor(AddAuthorRequestDTO model)
        {
            try
            {
                var author = new Author
                {
                    AuthorCreditnails = model.AuthorCreditnails,
                    Died = model.Died,
                    Biography = model.Biography,
                    Born = model.Born,
                    ImageUrl = model.ImageUrl
                };

                _authorServices.Add(author);
                await _authorServices.Commit();

                return new OkResultDto<Guid>(author.Id);
            }
            catch (Exception)
            {
                return new BadResultDto(nameof(model.AuthorCreditnails), "Ошибка выхода из сервиса.");
            }
        }

        public async Task<ActionResultDto> GetAllAuthors()
        {
            try
            {
                var result = await _authorServices.GetAll();
                return new OkResultDto<List<Author>>(result.ToList());
            }
            catch (Exception)
            {
                return new BadResultDto("Ошибка выхода из сервиса.");
            }
        }

        public async Task<ActionResultDto> GetSingleAuthor(Guid id)
        {
            try
            {
                var result = await _authorServices.GetSingle(id);

                if (result == null)
                    return new BadResultDto(nameof(id), "Этого автора не существует.");

                return new OkResultDto<Author>(result);
            }
            catch (Exception)
            {
                return new BadResultDto("Ошибка выхода из сервиса.");
            }
        }

        public async Task<ActionResultDto> RemoveAuthor(Guid id)
        {
            try
            {
                var result = await _authorServices.GetSingle(id);

                if (result == null)
                    return new BadResultDto(nameof(id), "Этого автора не существует.");

                _authorServices.Delete(result);
                await _authorServices.Commit();

                return new OkResultDto<Guid>(id);
            }
            catch (Exception)
            {
                return new BadResultDto(nameof(id), "Ошибка выхода из сервиса.");
            }
        }

        public async Task<ActionResultDto> UpdateAuthor(EditAuthorRequestDTO model)
        {
            try
            {
                var result = await _authorServices.GetSingle(model.Id);

                if (result == null)
                    return new BadResultDto(nameof(model.Id), "Этого автора не существует.");

                result.AuthorCreditnails = model.AuthorCreditnails;
                result.Born = model.Born;
                result.Biography = model.Biography;
                result.Died = model.Died;
                result.ImageUrl = model.ImageUrl;

                _authorServices.Update(result);
                await _authorServices.Commit();

                return new OkResultDto<Author>(result);
            }
            catch (Exception)
            {
                return new BadResultDto(nameof(model.Id), "Ошибка выхода из сервиса.");
            }
        }
    }
}
