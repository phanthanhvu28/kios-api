using ApplicationCore.DTOs.Company;
using ApplicationCore.UseCases.Company.Commands;
using Microsoft.AspNetCore.Mvc;

using VELA.WebCoreBase.Core.Controllers;
using VELA.WebCoreBase.Core.Models;

namespace WebApi.Controllers.V1;
/// <summary>
/// API Company
/// </summary>
/// 
[Route("api/v1/company")]
[ApiController]
public class CompanyController : AppControllerBase
{
    /// <summary>
    /// Create
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<ActionResult<ResultModel<CreateCompanyDto>>> Create(
        [FromBody] CreateCompany command,
        CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<CreateCompanyDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
}
