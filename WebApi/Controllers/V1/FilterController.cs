using ApplicationCore.DTOs.Company;
using ApplicationCore.UseCases.Company.Queries;
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

}
