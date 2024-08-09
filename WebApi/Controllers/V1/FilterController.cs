using ApplicationCore.DTOs.Area;
using ApplicationCore.DTOs.AuthenUser;
using ApplicationCore.DTOs.Company;
using ApplicationCore.DTOs.Staff;
using ApplicationCore.DTOs.Table;
using ApplicationCore.DTOs.TypeBida;
using ApplicationCore.DTOs.TypeSale;
using ApplicationCore.UseCases.Area.Queries;
using ApplicationCore.UseCases.AuthenUser.Queries;
using ApplicationCore.UseCases.Company.Queries;
using ApplicationCore.UseCases.Staff.Queries;
using ApplicationCore.UseCases.Table.Queries;
using ApplicationCore.UseCases.TypeBida.Queries;
using ApplicationCore.UseCases.TypeSale.Queries;
using Microsoft.AspNetCore.Mvc;

using VELA.WebCoreBase.Core.Controllers;
using VELA.WebCoreBase.Core.Models;

namespace WebApi.Controllers.V1;
/// <summary>
/// API Filter
/// </summary>
/// 
[Route("api/v1/filter")]
[ApiController]
public class FilterController : AppControllerBase
{
    /// <summary>
    /// Filter company
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>

    [HttpPost("company")]
    public async Task<ActionResult<ResultModel<FilterCompanyDto>>> FilterCompany(
       [FromBody] FilterCompany query,
       CancellationToken cancellationToken)
    {
        return ResultResponse(await Mediator.Send(query, cancellationToken));
    }

    /// <summary>
    /// Filter User
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("user")]
    public async Task<ActionResult<ResultModel<FilterUserDto>>> FilterUser(
       [FromBody] FilterUser query,
       CancellationToken cancellationToken)
    {
        return ResultResponse(await Mediator.Send(query, cancellationToken));
    }

    /// <summary>
    /// Filter Staff
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("staff")]
    public async Task<ActionResult<ResultModel<FilterStaffDto>>> FilterStaff(
       [FromBody] FilterStaff query,
       CancellationToken cancellationToken)
    {
        return ResultResponse(await Mediator.Send(query, cancellationToken));
    }

    /// <summary>
    /// Filter Area
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("area")]
    public async Task<ActionResult<ResultModel<FilterAreaDto>>> FilterArea(
      [FromBody] FilterArea query,
      CancellationToken cancellationToken)
    {
        return ResultResponse(await Mediator.Send(query, cancellationToken));
    }

    /// <summary>
    /// Table
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("table")]
    public async Task<ActionResult<ResultModel<FilterTableDto>>> FilterTable(
      [FromBody] FilterTable query,
      CancellationToken cancellationToken)
    {
        return ResultResponse(await Mediator.Send(query, cancellationToken));
    }
    /// <summary>
    /// Type sale
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("type-sale")]
    public async Task<ActionResult<ResultModel<FilterTypeSaleDto>>> FilterTypeSale(
      [FromBody] FilterTypeSale query,
      CancellationToken cancellationToken)
    {
        return ResultResponse(await Mediator.Send(query, cancellationToken));
    }

    /// <summary>
    /// Type bida
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("type-bida")]
    public async Task<ActionResult<ResultModel<FilterTypeBidaDto>>> FilterTypeBida(
      [FromBody] FilterTypeBida query,
      CancellationToken cancellationToken)
    {
        return ResultResponse(await Mediator.Send(query, cancellationToken));
    }


}
