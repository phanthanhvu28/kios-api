using ApplicationCore.DTOs.Order;
using ApplicationCore.DTOs.OrderDetail;
using ApplicationCore.UseCases.Order.Commands;
using ApplicationCore.UseCases.Order.Queries;
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
    public async Task<ActionResult<ResultModel<CreateOrderDto>>> Create(
    [FromBody] CreateOrder command,
    CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<CreateOrderDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }

    /// <summary>
    /// Detail
    /// </summary>
    /// <param name="orderCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{orderCode}")]
    public async Task<ActionResult<ResultModel<OrderDetailDto>>> Detail(
        [FromRoute] string orderCode,
        CancellationToken cancellationToken)
    {
        DetailOrder query = new()
        {
            OrderCode = orderCode,
        };
        ActionResult<ResultModel<OrderDetailDto>> response = ResultResponse(await Mediator.Send(query, cancellationToken));
        return response;
    }
    /// <summary>
    /// Delete Item
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<ActionResult<ResultModel<string>>> Delete(
       [FromBody] DeleteOrderItem command,
       CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<string>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
}
