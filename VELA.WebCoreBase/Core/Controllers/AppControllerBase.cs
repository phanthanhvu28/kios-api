using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace VELA.WebCoreBase.Core.Controllers;

public abstract class AppControllerBase : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

    private static int HttpCodeDictionary(CommonExceptionBase exceptionType)
    {
        return exceptionType switch
        {
            ForbiddenAccessException => (int)HttpStatusCode.Forbidden,
            ForbiddenActionException => (int)HttpStatusCode.BadRequest,
            NotFoundException => (int)HttpStatusCode.BadRequest,
            ProcessFlowException => (int)HttpStatusCode.BadRequest,
            ValidationException => (int)HttpStatusCode.BadRequest,
            PersistenceException => (int)HttpStatusCode.InternalServerError,
            UnhandledException => (int)HttpStatusCode.InternalServerError,
            ImportException => (int)HttpStatusCode.InternalServerError,
            _ => (int)HttpStatusCode.InternalServerError
        };
    }


    [NonAction]
    protected IActionResult ResultResponse(object? result)
    {
        ResultModel<object?> resultModel = ResultModel<object?>.Create(result);
        if (resultModel.Data is bool &&
            Convert.ToBoolean(resultModel.Data))
        {
            return Ok(resultModel);
        }

        if (result is not bool and not null)
        {
            return Ok(resultModel);
        }

        return BadRequest(resultModel);
    }

    [NonAction]
    protected ActionResult<ResultModel<TResult>> ResultResponse<TResult>(ResultModel<TResult> result)
    {
        if (!result.IsError)
        {
            return Ok(result);
        }

        int httpStatusCode = GetHttpStatusCode(result.Exception.GetType());
        return StatusCode(httpStatusCode, result);
    }

    [NonAction]
    protected ActionResult<ListResultModel<TResult>> ResultResponse<TResult>(ListResultModel<TResult> result)
    {
        if (!result.IsError)
        {
            return Ok(result);
        }

        int httpStatusCode = GetHttpStatusCode(result.Exception);
        return StatusCode(httpStatusCode, result);
    }

    [NonAction]
    private int GetHttpStatusCode(object exceptionType)
    {
        if (exceptionType.GetType() == typeof(CommonExceptionBase))
        {
            return HttpCodeDictionary((CommonExceptionBase)exceptionType);
        }

        // default status code
        return (int)HttpStatusCode.BadRequest;
    }
}