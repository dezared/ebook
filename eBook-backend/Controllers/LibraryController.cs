using eBook_backend.Models.Dto.Auth.Authentication;
using eBook_backend.Models.Dto;
using eBook_backend.Models.Entites;
using eBook_backend.Services;
using eBook_backend.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using eBook_backend.Models.Dto.Auth.Token;
using eBook_backend.Models.Dto.Library;

namespace eBook_backend.Controllers
{
    /// <summary>
    /// Библиотечный модуль
    /// </summary>
    [Tags("Библиотечный модуль")]
    public class LibraryController : BaseControllerAttribute
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;

        /// <inheritdoc />
        public LibraryController(IAuthorService authorService, IBookService bookService)
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        /// <summary>
        /// Получить все книги
        /// </summary>
        [ProducesResponseType(typeof(List<Book>), 200)]
        [HttpGet("getAllBooks")]
        public async Task<ActionResult<List<Book>>> GetAllBooks()
        {
            var check = await _bookService.GetAllBooks();

            if (!check.IsOk()) return BadRequest(check as BadResultDto);
            var checkObject = check as OkResultDto<List<Book>>;
            return Ok(checkObject.Data);
        }

        // Получить одну книгу всю инфу

        /// <summary>
        /// Получить книгу
        /// </summary>
        [ProducesResponseType(typeof(Book), 200)]
        [HttpGet("getBook")]
        public async Task<ActionResult<Book>> GetBook(Guid id)
        {
            var check = await _bookService.GetSingleBook(id);

            if (!check.IsOk()) return BadRequest(check as BadResultDto);
            var checkObject = check as OkResultDto<Book>;
            return Ok(checkObject.Data);
        }

        // Получить одного автора

        /// <summary>
        /// Получить автора
        /// </summary>
        [ProducesResponseType(typeof(Author), 200)]
        [HttpGet("getAuthor")]
        public async Task<ActionResult<Author>> GetAuthor(Guid id)
        {
            var check = await _authorService.GetSingleAuthor(id);

            if (!check.IsOk()) return BadRequest(check as BadResultDto);
            var checkObject = check as OkResultDto<Author>;
            return Ok(checkObject.Data);
        }

        // Получить всех авторов

        /// <summary>
        /// Получить списка авторов
        /// </summary>
        [ProducesResponseType(typeof(List<Author>), 200)]
        [HttpGet("getAllAuthors")]
        public async Task<ActionResult<List<Author>>> GetAllAuthors()
        {
            var check = await _authorService.GetAllAuthors();

            if (!check.IsOk()) return BadRequest(check as BadResultDto);
            var checkObject = check as OkResultDto<List<Author>>;
            return Ok(checkObject.Data);
        }

        // Создать книгу

        /// <summary>
        /// Создать книгу
        /// </summary>
        [ProducesResponseType(typeof(Guid), 200)]
        [HttpPut("createBook")]
        public async Task<ActionResult<Guid>> CreateBook(AddBookRequestDTO model)
        {
            var check = await _bookService.AddBook(model);

            if (!check.IsOk()) return BadRequest(check as BadResultDto);
            var checkObject = check as OkResultDto<Guid>;
            return Ok(checkObject.Data);
        }

        // Создать автора

        /// <summary>
        /// Создать автора
        /// </summary>
        [ProducesResponseType(typeof(Guid), 200)]
        [HttpPut("createAuthor")]
        public async Task<ActionResult<Guid>> CreateAuthor(AddAuthorRequestDTO model)
        {
            var check = await _authorService.AddAuthor(model);

            if (!check.IsOk()) return BadRequest(check as BadResultDto);
            var checkObject = check as OkResultDto<Guid>;
            return Ok(checkObject.Data);
        }

        // Редактировать автора

        /// <summary>
        /// Изменить автора
        /// </summary>
        [ProducesResponseType(typeof(Author), 200)]
        [HttpPatch("editAuthor")]
        public async Task<ActionResult<Author>> EditAuthor(EditAuthorRequestDTO model)
        {
            var check = await _authorService.UpdateAuthor(model);

            if (!check.IsOk()) return BadRequest(check as BadResultDto);
            var checkObject = check as OkResultDto<Author>;
            return Ok(checkObject.Data);
        }

        // Редактировать книгу

        /// <summary>
        /// Изменить книгу
        /// </summary>
        [ProducesResponseType(typeof(Book), 200)]
        [HttpPatch("editBook")]
        public async Task<ActionResult<Book>> EditBook(EditBookRequestDTO model)
        {
            var check = await _bookService.UpdateBook(model);

            if (!check.IsOk()) return BadRequest(check as BadResultDto);
            var checkObject = check as OkResultDto<Book>;
            return Ok(checkObject.Data);
        }

        // Удалить книгу

        /// <summary>
        /// Удалить книгу
        /// </summary>
        [ProducesResponseType(typeof(Guid), 200)]
        [HttpDelete("removeBook")]
        public async Task<ActionResult<Guid>> RemoveBook(Guid id)
        {
            var check = await _bookService.RemoveBook(id);

            if (!check.IsOk()) return BadRequest(check as BadResultDto);
            var checkObject = check as OkResultDto<Guid>;
            return Ok(checkObject.Data);
        }

        // Удалить автора

        /// <summary>
        /// Удалить автора
        /// </summary>
        [ProducesResponseType(typeof(Guid), 200)]
        [HttpDelete("removeAuthor")]
        public async Task<ActionResult<Guid>> RemoveAuthor(Guid id)
        {
            var check = await _authorService.RemoveAuthor(id);

            if (!check.IsOk()) return BadRequest(check as BadResultDto);
            var checkObject = check as OkResultDto<Guid>;
            return Ok(checkObject.Data);
        }
    }
}
