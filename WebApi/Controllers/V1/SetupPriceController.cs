using ApplicationCore.DTOs.UnitPrice;
using ApplicationCore.UseCases.UnitPrice.Queries;
using Microsoft.AspNetCore.Mvc;
using VELA.WebCoreBase.Core.Controllers;
using VELA.WebCoreBase.Core.Models;

namespace WebApi.Controllers.V1;
/// <summary>
/// API Setup Price
/// </summary>
/// 
[Route("api/v1/price")]
[ApiController]
public class SetupPriceController : AppControllerBase
{
    /// <summary>
    /// Get Price
    /// </summary>
    /// <param name="storeCode"></param>
    /// <param name="productCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{storeCode}/{productCode}")]
    public async Task<ActionResult<ResultModel<UnitPriceBaseDto>>> GetPrice(
        [FromRoute] string storeCode,
        [FromRoute] string productCode,
        CancellationToken cancellationToken)
    {
        GetPrice query = new()
        {
            StoreCode = storeCode,
            ProductCode = productCode,
        };
        ActionResult<ResultModel<UnitPriceBaseDto>> response = ResultResponse(await Mediator.Send(query, cancellationToken));
        return response;
    }
}
