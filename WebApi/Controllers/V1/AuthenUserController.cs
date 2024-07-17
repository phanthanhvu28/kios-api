using ApplicationCore.DTOs.AuthenUser;
using ApplicationCore.UseCases.AuthenUser.Commands;
using ApplicationCore.UseCases.AuthenUser.Queries;
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
    /// Reset Password
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("reset-password")]
    public async Task<ActionResult<ResultModel<bool>>> ResetPassword(
       [FromBody] ResetPassword command,
       CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<bool>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
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

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("update")]
    public async Task<ActionResult<ResultModel<UpdateUserDto>>> Update(
       [FromBody] UpdateUser command,
       CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<UpdateUserDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
    /// <summary>
    /// Delete user
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<ActionResult<ResultModel<string>>> Delete(
     [FromBody] DeleteUser command,
     CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<string>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }

    /// <summary>
    /// Get List
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("user")]
    public async Task<ActionResult<ListResultModel<UserBaseDto>>> GetList(
       [FromBody] GetUser query,
       CancellationToken cancellationToken)
    {
        return ResultResponse(await Mediator.Send(query, cancellationToken));
    }
}
