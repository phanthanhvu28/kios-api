using ApplicationCore.DTOs.TypeBida;
using ApplicationCore.UseCases.TypeBida.Commands;
using ApplicationCore.UseCases.TypeBida.Queries;
using Microsoft.AspNetCore.Mvc;
using VELA.WebCoreBase.Core.Controllers;
using VELA.WebCoreBase.Core.Models;

namespace WebApi.Controllers.V1;
/// <summary>
/// API TypeBida
/// </summary>
/// 
[Route("api/v1/type-bida")]
[ApiController]
public class TypeBidaController : AppControllerBase
{
    /// <summary>
    /// List
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ListResultModel<TypeBidaBaseDto>>> GetList(
      [FromBody] GetTypeBida query,
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
    public async Task<ActionResult<ResultModel<CreateTypeBidaDto>>> Create(
     [FromBody] CreateTypeBida command,
     CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<CreateTypeBidaDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("update")]
    public async Task<ActionResult<ResultModel<UpdateTypeBidaDto>>> Update(
     [FromBody] UpdateTypeBida command,
     CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<UpdateTypeBidaDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
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
    [FromBody] DeleteTypeBida command,
    CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<string>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
}
