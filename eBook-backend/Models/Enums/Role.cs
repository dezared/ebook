using System.Runtime.Serialization;

namespace eBook_backend.Models.Enums
{
    /// <summary>
    /// Все роли в системе
    /// </summary>
    [DataContract]
    public enum Role
    {
        /// <summary>
        /// Бариста
        /// </summary>
        [EnumMember]
        User,
        /// <summary>
        /// Администратор
        /// </summary>
        [EnumMember]
        Admin
    }
}
