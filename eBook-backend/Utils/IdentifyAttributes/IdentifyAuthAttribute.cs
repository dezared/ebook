using eBook_backend.Models.Enums;
using eBook_backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eBook_backend.Utils.IdentifyAttributes
{
    /// <summary>
    /// Кастомный аттрибут авторизации
    /// </summary>
    public class IdentifyAuthAttribute : ActionFilterAttribute
    {
        private Role? Role { get; set; }

        /// <inheritdoc />
        public IdentifyAuthAttribute() { }

        /// <inheritdoc />
        public IdentifyAuthAttribute(Role requiredRole)
        {
            Role = requiredRole;
        }

        /// <inheritdoc />
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userService = context.HttpContext.RequestServices.GetService<IUserService>();

            var user = await userService.GetUser();

            if (user == null)
            {
                context.Result = new BadRequestObjectResult(new { Error = "Вы не авторизованы." });
                return;
            }

            if (Role != null && Role != user.Role)
            {
                context.Result = new ObjectResult("У вас недостаточно прав.") { StatusCode = 403 };
                return;
            }
            
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
