using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;

namespace eBook_backend.Utils.Swagger
{
    /// <summary>
    /// Enum для тэгов в Swagger
    /// </summary>
    [Flags]
    public enum ToolAttributes
    {
        /// <summary>
        /// Метод не работает
        /// </summary>
        [Description("NonWork")] NonWork = 1,

        /// <summary>
        /// Тэг. Только для менеджеров
        /// </summary>
        [Description("Manager")] ForManagerAttribute = 2,

        /// <summary>
        /// Тэг. Только для админов
        /// </summary>
        [Description("Admin")] ForAdminAttribute = 4,

        /// <summary>
        /// Тэг. Метод для дебага и тестирования
        /// </summary>
        [Description("Debug")] DebugAttribute = 8,

        /// <summary>
        /// Тэг. Для админ-панеле
        /// </summary>
        [Description("APanel")] AdminPanelTag = 16,

        /// <summary>
        /// Тэг. Авторизация
        /// </summary>
        [Description("Auth")] AuthorizationTag = 32,

        /// <summary>
        /// Тэг. Работа с авторизованными пользователями
        /// </summary>
        [Description("User")] UserTag = 64
    }

    /// <summary>
    /// Атрибут для добавления Swagger тэгов
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class BadgesSwaggerAttribute : Attribute
    {
        /// <summary>
        /// Тип тэга
        /// </summary>
        public readonly ToolAttributes Badge;

        /// <inheritdoc />
        public BadgesSwaggerAttribute(ToolAttributes badge)
        {
            Badge = badge;
        }
    }

    /// <summary>
    /// Технический преобразующий фильтр для swagger
    /// </summary>
    public class SwaggerOperationFilter : IOperationFilter
    {
        private static string ToDescriptionString(ToolAttributes val)
        {
            var attributes = (DescriptionAttribute[])val
                .GetType()
                .GetField(val.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        private static IEnumerable<Enum> GetFlags(Enum input) =>
            Enum.GetValues(input.GetType()).Cast<Enum>().Where(input.HasFlag);

        /// <inheritdoc />
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var find = context.ApiDescription.CustomAttributes().FirstOrDefault(x => x is BadgesSwaggerAttribute);

            if (find == null)
                return;

            var flags = GetFlags((find as BadgesSwaggerAttribute).Badge);

            var badgeList = flags
                .Select(flag => flag is ToolAttributes attributes ? attributes : ToolAttributes.NonWork)
                .Select(s => "[#" + s.ToString() + ": " + ToDescriptionString(s) + "]")
                .ToList();

            operation.Summary = $"{string.Join(" ", badgeList)} {operation.Summary}";
        }
    }
}