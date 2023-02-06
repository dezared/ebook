using eBook_backend.Models.Dto;
using eBook_backend.Models.Dto.Library;
using eBook_backend.Models.Entites;
using eBook_backend.Repositories;

namespace eBook_backend.Services
{
    public interface IBookService
    {
        /// <summary>
        /// Получить одну книгу
        /// </summary>
        Task<ActionResultDto> GetSingleBook(Guid id);

        /// <summary>
        /// Получить все книги
        /// </summary>
        Task<ActionResultDto> GetAllBooks();

        /// <summary>
        /// Обновить книгу
        /// </summary>
        Task<ActionResultDto> UpdateBook(EditBookRequestDTO model);

        /// <summary>
        /// Добавить книгу
        /// </summary>
        Task<ActionResultDto> AddBook(AddBookRequestDTO model);

        /// <summary>
        /// Удалить книгу
        /// </summary>
        Task<ActionResultDto> RemoveBook(Guid id);
    }

    public class BookService : IBookService
    {
        private readonly IEntityBaseRepository<Book> _bookServices;
        private readonly IEntityBaseRepository<Author> _authorServices;

        public BookService(IEntityBaseRepository<Book> bookService, IEntityBaseRepository<Author> authorService)
        {
            _bookServices = bookService;
            _authorServices = authorService;
        }

        public async Task<ActionResultDto> AddBook(AddBookRequestDTO model)
        {
            try
            {
                var book = new Book()
                {
                    AuthorId = model.AuthorId,
                    Genre = model.Genre,
                    ImageUrl = model.ImageUrl,
                    Information = model.Information,
                    Name = model.Name,
                    Raiting = model.Raiting,
                    Year = model.Year
                };

                if (book.Raiting is < 0 or > 10)
                    return new BadResultDto(nameof(model.Raiting), "Неверный ввод: рейтинг");

                var search = await _authorServices.GetSingle(model.AuthorId);

                if (search is null)
                    return new BadResultDto(nameof(model.Raiting), "Автора не существует");

                _bookServices.Add(book);
                await _bookServices.Commit();
                return new OkResultDto<Guid>(book.Id);
            }
            catch (Exception)
            {
                return new BadResultDto(nameof(model.Name), "Ошибка выхода из сервиса.");
            }
        }

        public async Task<ActionResultDto> GetAllBooks()
        {
            try
            {
                var result = await _bookServices.GetAll();
                return new OkResultDto<List<Book>>(result.ToList());
            }
            catch (Exception)
            {
                return new BadResultDto("Ошибка выхода из сервиса.");
            }
        }

        public async Task<ActionResultDto> GetSingleBook(Guid id)
        {
            try
            {
                var result = await _bookServices.GetSingle(id);

                if (result == null)
                    return new BadResultDto(nameof(id), "Этой книги не существует.");

                return new OkResultDto<Book>(result);
            }
            catch (Exception)
            {
                return new BadResultDto("Ошибка выхода из сервиса.");
            }
        }

        public async Task<ActionResultDto> RemoveBook(Guid id)
        {
            try
            {
                var result = await _bookServices.GetSingle(id);

                if (result == null)
                    return new BadResultDto(nameof(id), "Этой книги не существует.");

                _bookServices.Delete(result);
                await _bookServices.Commit();

                return new OkResultDto<Guid>(id);
            }
            catch (Exception)
            {
                return new BadResultDto(nameof(id), "Ошибка выхода из сервиса.");
            }
        }

        public async Task<ActionResultDto> UpdateBook(EditBookRequestDTO model)
        {
            try
            {
                var result = await _bookServices.GetSingle(model.Id);

                if (result == null)
                    return new BadResultDto(nameof(model.Id), "Этой книги не существует.");

                var search = await _authorServices.GetSingle(model.AuthorId);

                if (search is null)
                    return new BadResultDto(nameof(model.Raiting), "Автора не существует");

                result.AuthorId = model.AuthorId;
                result.Genre = model.Genre;
                result.ImageUrl = model.ImageUrl;
                result.Information = model.Information;
                result.Name = model.Name;
                result.Raiting = model.Raiting;
                result.Year = model.Year;

                _bookServices.Update(result);
                await _bookServices.Commit();

                return new OkResultDto<Book>(result);
            }
            catch (Exception)
            {
                return new BadResultDto(nameof(model.Name), "Ошибка выхода из сервиса.");
            }
        }
    }
}
