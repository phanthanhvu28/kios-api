using ApplicationCore.DTOs.Role;
using ApplicationCore.UseCases.Role.Commands;
using ApplicationCore.UseCases.Role.Queries;
using Microsoft.AspNetCore.Mvc;
using VELA.WebCoreBase.Core.Controllers;
using VELA.WebCoreBase.Core.Models;

namespace WebApi.Controllers.V1;
/// <summary>
/// API Staff
/// </summary>
/// 
[Route("api/v1/role")]
[ApiController]
public class RoleController : AppControllerBase
{
    /// <summary>
    /// List
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ListResultModel<RoleBaseDto>>> GetList(
       [FromBody] GetRole query,
       CancellationToken cancellationToken)
    {
        return ResultResponse(await Mediator.Send(query, cancellationToken));
    }

    /// <summary>
    /// Create
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<ActionResult<ResultModel<CreateRoleDto>>> Create(
        [FromBody] CreateRole command,
        CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<CreateRoleDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("update")]
    public async Task<ActionResult<ResultModel<UpdateRoleDto>>> Update(
     [FromBody] UpdateRole command,
     CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<UpdateRoleDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<ActionResult<ResultModel<string>>> Delete(
     [FromBody] DeleteRole command,
     CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<string>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
}
