using ApplicationCore.DTOs.AuthenUser;
using ApplicationCore.UseCases.AuthenUser.Commands;
using Microsoft.AspNetCore.Mvc;
using VELA.WebCoreBase.Core.Controllers;
using VELA.WebCoreBase.Core.Models;

namespace WebApi.Controllers.V1;
/// <summary>
/// API Area
/// </summary>
/// 
[Route("api/v1/authen")]
[ApiController]
public class AuthenUserController : AppControllerBase
{
    /// <summary>
    /// Login
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<ResultModel<LoginDto>>> Login(
        [FromBody] Login command,
        CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<LoginDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
    /// <summary>
    /// Create user
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<ActionResult<ResultModel<CreateUserDto>>> Create(
       [FromBody] CreateUser command,
       CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<CreateUserDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
}
