using System.ComponentModel.DataAnnotations;

namespace eBook_backend.Models.Dto.Library
{
    /// <summary>
    /// DTO для обновления автора
    /// </summary>
    public class EditAuthorRequestDTO
    {
        /// <summary>
        /// Id сущности
        /// </summary>
        [Required(ErrorMessage = "Id обязателен.")]
        public Guid Id { get; set; }

        /// <summary>
        /// ФИО автора
        /// </summary>
        [Required(ErrorMessage = "AuthorCreditnails обязателен.")]
        public string AuthorCreditnails { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [Required(ErrorMessage = "Born обязателен.")]
        public DateOnly Born { get; set; }

        /// <summary>
        /// Дата смерти ( если есть )
        /// </summary>
        public DateOnly? Died { get; set; }

        /// <summary>
        /// Краткая биография
        /// </summary>
        [Required(ErrorMessage = "Biography обязателен.")]
        public string Biography { get; set; }

        /// <summary>
        /// Портрет ( ссылка )
        /// </summary>
        [Required(ErrorMessage = "ImageUrl обязателен.")]
        public string ImageUrl { get; set; }

    }
}
