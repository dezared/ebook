namespace eBook_backend.Models.Entites
{
    /// <summary>
    /// Сущность автора
    /// </summary>
    public class Author : BaseEntity
    {
        /// <summary>
        /// ФИО автора
        /// </summary>
        public string AuthorCreditnails { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateOnly Born { get; set; }

        /// <summary>
        /// Дата смерти ( если есть )
        /// </summary>
        public DateOnly? Died { get; set; }

        /// <summary>
        /// Краткая биография
        /// </summary>
        public string Biography { get; set; }

        /// <summary>
        /// Портрет ( ссылка )
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
