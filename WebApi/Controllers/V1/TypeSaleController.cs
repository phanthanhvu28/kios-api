using ApplicationCore.DTOs.TypeSale;
using ApplicationCore.UseCases.TypeSale.Commands;
using ApplicationCore.UseCases.TypeSale.Queries;
using Microsoft.AspNetCore.Mvc;
using VELA.WebCoreBase.Core.Controllers;
using VELA.WebCoreBase.Core.Models;

namespace WebApi.Controllers.V1;
/// <summary>
/// API TypeBida
/// </summary>
/// 
[Route("api/v1/type-sale")]
[ApiController]
public class TypeSaleController : AppControllerBase
{
    /// <summary>
    /// List
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ListResultModel<TypeSaleBaseDto>>> GetList(
      [FromBody] GetTypeSale query,
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
    public async Task<ActionResult<ResultModel<CreateTypeSaleDto>>> Create(
     [FromBody] CreateTypeSale command,
     CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<CreateTypeSaleDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
    /// <summary>
    /// Update
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("update")]
    public async Task<ActionResult<ResultModel<UpdateTypeSaleDto>>> Update(
     [FromBody] UpdateTypeSale command,
     CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<UpdateTypeSaleDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
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
    [FromBody] DeleteTypeSale command,
    CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<string>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
}
