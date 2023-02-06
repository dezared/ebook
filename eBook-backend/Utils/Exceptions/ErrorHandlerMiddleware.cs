using System.Net;
using System.Text.Json;

namespace eBook_backend.Utils.Exceptions
{
    /// <summary>
    /// Middleware для отлова ошибок в методах
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary> .ctor </summary>
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Метод, обрабатывающий вызов контроллера
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.BadRequest;

                var result = JsonSerializer.Serialize(new { Error = "Ошибка выполнения операции", error.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
