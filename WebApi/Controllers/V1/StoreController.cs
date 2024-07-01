using ApplicationCore.DTOs.Store;
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

}
