using ApplicationCore.DTOs.Product;
using ApplicationCore.UseCases.Product.Commands;
using ApplicationCore.UseCases.Product.Queries;
using Microsoft.AspNetCore.Mvc;
using VELA.WebCoreBase.Core.Controllers;
using VELA.WebCoreBase.Core.Models;

namespace WebApi.Controllers.V1;
/// <summary>
/// API Product
/// </summary>
/// 
[Route("api/v1/product")]
[ApiController]
public class ProductController : AppControllerBase
{
    /// <summary>
    /// List
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ListResultModel<ProductBaseDto>>> GetList(
       [FromBody] GetProduct query,
       CancellationToken cancellationToken)
    {
        return ResultResponse(await Mediator.Send(query, cancellationToken));
    }
    /// <summary>
    /// Create Product
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<ActionResult<ResultModel<CreateProductDto>>> Create(
      [FromBody] CreateProduct command,
      CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<CreateProductDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
    /// <summary>
    /// Update Product
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("update")]
    public async Task<ActionResult<ResultModel<UpdateProductDto>>> Update(
    [FromBody] UpdateProduct command,
    CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<UpdateProductDto>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
    /// <summary>
    /// Delete Product
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<ActionResult<ResultModel<string>>> Delete(
     [FromBody] DeleteProduct command,
     CancellationToken cancellationToken)
    {
        ActionResult<ResultModel<string>> response = ResultResponse(await Mediator.Send(command, cancellationToken));
        return response;
    }
}
