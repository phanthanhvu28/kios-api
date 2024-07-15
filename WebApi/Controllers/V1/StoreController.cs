using ApplicationCore.DTOs.Store;
using ApplicationCore.UseCases.Store.Commands;
using ApplicationCore.UseCases.Store.Queries;
using Microsoft.AspNetCore.Mvc;
using VELA.WebCoreBase.Core.Controllers;
using VELA.WebCoreBase.Core.Models;

namespace WebApi.Controllers.V1;
/// <summary>
/// API Store
/// </summary>
/// 
[Route("api/v1/store")]
[ApiController]
public class StoreController : AppControllerBase
{
    /// <summary>
    /// List
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ListResultModel<StoreBaseDto>>> GetList(
       [FromBody] GetStore query,
       CancellationToken cancellationToken)
    {
        return ResultResponse(await Mediator.Send(query, cancellationToken));
    }

    [HttpPost("create")]
    public async Task<ActionResult<ResultModel<CreateStoreDto>>> Create(
      [FromBody] CreateStore command,
      CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<CreateStoreDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }

    [HttpPost("update")]
    public async Task<ActionResult<ResultModel<UpdateStoreDto>>> Update(
     [FromBody] UpdateStore command,
     CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<UpdateStoreDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }

    [HttpPost("delete")]
    public async Task<ActionResult<ResultModel<string>>> Delete(
     [FromBody] DeleteStore command,
     CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<string>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
}
