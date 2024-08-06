using ApplicationCore.DTOs.Area;
using ApplicationCore.UseCases.Area.Commands;
using ApplicationCore.UseCases.Area.Queries;
using Microsoft.AspNetCore.Mvc;
using VELA.WebCoreBase.Core.Controllers;
using VELA.WebCoreBase.Core.Models;

namespace WebApi.Controllers.V1;
/// <summary>
/// API Area
/// </summary>
/// 
[Route("api/v1/area")]
[ApiController]
public class AreaController : AppControllerBase
{
    /// <summary>
    /// List
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ListResultModel<AreaBaseDto>>> GetList(
       [FromBody] GetArea query,
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
    public async Task<ActionResult<ResultModel<CreateAreaDto>>> Create(
      [FromBody] CreateArea command,
      CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<CreateAreaDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("update")]
    public async Task<ActionResult<ResultModel<UpdateAreaDto>>> Update(
     [FromBody] UpdateArea command,
     CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<UpdateAreaDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
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
     [FromBody] DeleteArea command,
     CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<string>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
}
