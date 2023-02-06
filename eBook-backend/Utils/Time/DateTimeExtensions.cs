namespace surfis_backend.Utils.Time
{
    /// <summary>
    /// Класс, для работы со временем
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Добавить в DateTime UTC аттрибут
        /// </summary>
        public static DateTime? SetKindUtc(this DateTime? dateTime) => dateTime?.SetKindUtc();

        /// <summary>
        /// Добавить в DateTime UTC аттрибут
        /// </summary>
        public static DateTime SetKindUtc(this DateTime dateTime) => dateTime.Kind == DateTimeKind.Utc
            ? dateTime
            : DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }
}