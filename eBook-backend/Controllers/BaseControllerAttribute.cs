using Microsoft.AspNetCore.Mvc;
using eBook_backend.Models.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace eBook_backend.Controllers
{
    /// <summary>
    /// Базвоый метод, добавляющий аттрибуты в конструкторы
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerResponse(200, description: "Выводит результат выполнения метода.")]
    [SwaggerResponse(400, description: "Данные с body не прошли внешнюю/внутреннюю валидацию, либо в методе было исключение.")]
    [ProducesResponseType(typeof(BadResultDto), 400)]
    [SwaggerResponse(401, description: "Пользователь не авторизирован или срок действия токена истек.")]
    [ProducesResponseType(typeof(BadResultDto), 401)]
    [SwaggerResponse(402, description: "Недостаточно прав.")]
    [ProducesResponseType(typeof(BadResultDto), 402)]
    public class BaseControllerAttribute : ControllerBase { }
}
