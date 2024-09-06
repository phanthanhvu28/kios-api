using ApplicationCore.UseCases.Order.Commands;
using Microsoft.AspNetCore.Mvc;
using VELA.WebCoreBase.Core.Controllers;
using VELA.WebCoreBase.Core.Models;

namespace WebApi.Controllers.V1;
/// <summary>
/// API Order
/// </summary>
/// 
[Route("api/v1/order")]
[ApiController]
public class OrderController : AppControllerBase
{
    /// <summary>
    /// Create Order
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<ActionResult<ResultModel<bool>>> Create(
    [FromBody] CreateOrder command,
    CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<bool>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
}
