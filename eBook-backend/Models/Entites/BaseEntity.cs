using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBook_backend.Models.Entites
{
    /// <summary>
    /// Базовая модель для всех моделей
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Id сущности
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}
