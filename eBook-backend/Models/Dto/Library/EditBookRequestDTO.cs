using System.ComponentModel.DataAnnotations;

namespace eBook_backend.Models.Dto.Library
{
    /// <summary>
    /// DTO для обновления книги
    /// </summary>
    public class EditBookRequestDTO
    {
        /// <summary>
        /// Id книги
        /// </summary>
        [Required(ErrorMessage = "Id обязателен.")]
        public Guid Id { get; set; }

        /// <summary>
        /// Название книги
        /// </summary>
        [Required(ErrorMessage = "Name обязателен.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Год выпуска
        /// </summary>
        [Required(ErrorMessage = "Year обязателен.")]
        public int Year { get; set; }

        // Решено не выносить жанры в Enums, т.к жанров слишком много
        /// <summary>
        /// Жанр произведения
        /// </summary>
        [Required(ErrorMessage = "Genre обязателен.")]
        public string Genre { get; set; } = null!;

        /// <summary>
        /// Большое описание книги
        /// </summary>
        public string Information { get; set; } = null!;

        /// <summary>
        /// ID автора книги
        /// </summary>
        [Required(ErrorMessage = "AuthorId обязателен.")]
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Постер к книге
        /// </summary>
        [Required(ErrorMessage = "AuthorId обязателен.")]
        public string ImageUrl { get; set; } = null!;

        /// <summary>
        /// Рейтинг книги
        /// </summary>
        [Required(ErrorMessage = "AuthorId обязателен.")]
        [Range(0.0, 10.0, ErrorMessage = "Рейтинг должен быть в диапазоне от 0 до 10")]
        public decimal Raiting { get; set; }
    }
}
