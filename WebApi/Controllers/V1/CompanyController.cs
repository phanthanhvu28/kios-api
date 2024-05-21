using ApplicationCore.DTOs.Company;
using ApplicationCore.UseCases.Company.Commands;
using ApplicationCore.UseCases.Company.Queries;
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
    /// <summary>
    /// Update
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// 
    [HttpPost("update")]
    public async Task<ActionResult<ResultModel<UpdateCompanyDto>>> Update(
        [FromBody] UpdateCompany command,
        CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<UpdateCompanyDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
    /// <summary>
    /// List
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ListResultModel<CompanyBaseDto>>> GetList(
       [FromBody] GetCompany query,
       CancellationToken cancellationToken)
    {
        return ResultResponse(await Mediator.Send(query, cancellationToken));
    }
}
