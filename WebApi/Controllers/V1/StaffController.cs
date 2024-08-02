using ApplicationCore.DTOs.Staff;
using ApplicationCore.UseCases.Staff.Commands;
using ApplicationCore.UseCases.Staff.Queries;
using Microsoft.AspNetCore.Mvc;
using VELA.WebCoreBase.Core.Controllers;
using VELA.WebCoreBase.Core.Models;

namespace WebApi.Controllers.V1;
/// <summary>
/// API Staff
/// </summary>
/// 
[Route("api/v1/staff")]
[ApiController]
public class StaffController : AppControllerBase
{
    /// <summary>
    /// List
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ListResultModel<StaffBaseDto>>> GetList(
       [FromBody] GetStaff query,
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
    public async Task<ActionResult<ResultModel<CreateStaffDto>>> Create(
      [FromBody] CreateStaff command,
      CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<CreateStaffDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("update")]
    public async Task<ActionResult<ResultModel<UpdateStaffDto>>> Update(
     [FromBody] UpdateStaff command,
     CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<UpdateStaffDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("delete")]
    public async Task<ActionResult<ResultModel<string>>> Delete(
     [FromBody] DeleteStaff command,
     CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<string>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
}
