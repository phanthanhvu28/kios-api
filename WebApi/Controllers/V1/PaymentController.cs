using ApplicationCore.DTOs.Payment;
using ApplicationCore.UseCases.Payment.Commands;
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
    /// Payment
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
}
