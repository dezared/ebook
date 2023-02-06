using eBook_backend.Models.Dto.Auth.Authentication;
using eBook_backend.Models.Dto.Auth.Token;
using eBook_backend.Services;
using eBook_backend.Utils.IdentifyAttributes;
using eBook_backend.Utils.Swagger;
using Microsoft.AspNetCore.Mvc;
using eBook_backend.Models.Dto;
using eBook_backend.Services.Auth;

namespace eBook_backend.Controllers
{
    /// <summary>
    /// Аутентификационный модуль
    /// </summary>
    [Tags("Аутентификационный модуль")]
    public class AuthenticationController : BaseControllerAttribute
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;

        /// <inheritdoc />
        public AuthenticationController(IAuthenticationService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <remarks>На вход подается e-mail пользователя и пароль, после чего система отдает пару токенов
        /// (refresh и acess ), время жизни refresh токена, роль пользователя, и параметр isPersist, означающий, на сколько
        /// длительно будет сессия пользователя.
        /// </remarks>
        [ProducesResponseType(typeof(AuthenticationResultDto), 200)]
        [BadgesSwagger(ToolAttributes.AuthorizationTag)]
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResultDto>> Login([FromBody] AuthenticationRequestDto model)
        {
            var check = await _authService.CheckUserBeforeAuthenticate(model);

            if (check.IsOk())
                return await _authService.Authenticate(model);

            return BadRequest(check as BadResultDto);
        }

        /// <summary>
        /// Обновить jwt-token ( refresh-token )
        /// </summary>
        /// <remarks>Обновить пару устаревших токенов, метод отдаст новую пару токенов.</remarks>
        [HttpPost("refresh-token")]
        [BadgesSwagger(ToolAttributes.AuthorizationTag | ToolAttributes.UserTag)]
        public async Task<ActionResult<TokenModelThroughDto>> RefreshToken([FromBody] TokenModelThroughDto model)
        {
            var check = await _authService.RefreshToken(model);

            if (!check.IsOk()) return BadRequest(check as BadResultDto);
            var checkObject = check as OkResultDto<TokenModelThroughDto>;
            return Ok(checkObject.Data);
        }

        /// <summary>
        /// Выйти на стороне сервиса
        /// </summary>
        /// <remarks>Метод, который в ручную истекает рефреш токен. На стороне клиента токен должен быть уничтожен из localStorage.</remarks>
        [HttpPost("logout")]
        [BadgesSwagger(ToolAttributes.AuthorizationTag | ToolAttributes.UserTag)]
        [IdentifyAuth]
        public async Task<ActionResult<OkResultDto<Guid>>> LogOut([FromBody] TokenModelThroughDto model)
        {
            var user = await _userService.GetUser();

            await _authService.LogOut(user);

            return Ok(new OkResultDto<Guid>(user.Id, "Спот был успешно инициализирован."));
        }
    }
}
