namespace eBook_backend.Models.Entites
{
    /// <summary>
    /// Базовая сущность ( отслеживаемая )
    /// </summary>
    public class BaseTrackEntity : BaseEntity
    {
        /// <summary>
        /// Дата создания модели
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Дата обновления модели
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
