namespace eBook_backend.Models.Entites
{
    /// <summary>
    /// Сущность книги
    /// </summary>
    public class Book : BaseEntity
    {
        /// <summary>
        /// Название книги
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Год выпуска
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Большое описание книги
        /// </summary>
        public string Information { get; set; } = null!;

        // Решено не выносить жанры в Enums, т.к жанров слишком много
        /// <summary>
        /// Жанр произведения
        /// </summary>
        public string Genre { get; set; } = null!;

        /// <summary>
        /// ID автора книги
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Постер к книге
        /// </summary>
        public string ImageUrl { get; set; } = null!;

        /// <summary>
        /// Рейтинг книги
        /// </summary>
        public decimal Raiting { get; set; }
    }
}
