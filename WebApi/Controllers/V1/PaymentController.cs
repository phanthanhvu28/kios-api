using ApplicationCore.DTOs.Payment;
using ApplicationCore.UseCases.Payment.Commands;
using ApplicationCore.UseCases.Payment.Queries;
using Microsoft.AspNetCore.Mvc;
using VELA.WebCoreBase.Core.Controllers;
using VELA.WebCoreBase.Core.Models;

namespace WebApi.Controllers.V1;
/// <summary>
/// API Order
/// </summary>
/// 
[Route("api/v1/payment")]
[ApiController]
public class PaymentController : AppControllerBase
{
    /// <summary>
    /// Create
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ResultModel<CreatePaymentDto>>> Create(
    [FromBody] CreatePayment command,
    CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<CreatePaymentDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
    /// <summary>
    /// Get Payment
    /// </summary>
    /// <param name="orderCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{orderCode}")]
    public async Task<ActionResult<ResultModel<List<GetPaymentDto>>>> Detail(
       [FromRoute] string orderCode,
       CancellationToken cancellationToken)
    {
        GetPayment query = new()
        {
            OrderCode = orderCode,
        };
        ActionResult<ResultModel<List<GetPaymentDto>>> response = ResultResponse(await Mediator.Send(query, cancellationToken));
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
    [FromBody] DeletePayment command,
    CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<string>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
}
