using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace VELA.WebCoreBase.Core.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private static readonly IDictionary<Type, Action<ExceptionContext>> ExceptionHandlers;

    static ApiExceptionFilterAttribute()
    {
        // Register known exception types and handlers.
        ExceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ValidationException), HandleValidationException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
            { typeof(ForbiddenActionException), HandleForbiddenAccessException },
            { typeof(UnhandledException), HandlerUnhandledException }
        };
    }

    private static void HandlerUnhandledException(ExceptionContext context)
    {
        string errorMessage = context.Exception.Message;
        if (context.Exception.InnerException is { Message: not null } innerException)
        {
            errorMessage = innerException.Message;
        }

        context.Result = new BadRequestObjectResult(
            ResultModel<string>.Create(new UnhandledException(errorMessage)));
        context.ExceptionHandled = true;
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (ExceptionHandlers.TryGetValue(type, out Action<ExceptionContext>? handler))
        {
            handler?.Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        context.Result = new BadRequestObjectResult(
            ResultModel<string>.Create(new UnhandledException(context.Exception.Message)));
        context.ExceptionHandled = true;
    }

    private static void HandleValidationException(ExceptionContext context)
    {
        ValidationException exception = (ValidationException)context.Exception;

        //ValidationProblemDetails details = new(exception.Errors)
        //{
        //    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        //};

        context.Result = new BadRequestObjectResult(ResultModel<string>.Create(exception));
        context.ExceptionHandled = true;
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        ValidationException exception = (ValidationException)context.Exception;
        //ValidationProblemDetails details = new(context.ModelState)
        //{
        //    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        //};

        context.Result = new BadRequestObjectResult(ResultModel<string>.Create(exception));

        context.ExceptionHandled = true;
    }

    private static void HandleNotFoundException(ExceptionContext context)
    {
        NotFoundException exception = (NotFoundException)context.Exception;

        //ProblemDetails details = new()
        //{
        //    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
        //    Title = "The specified resource was not found.",
        //    Detail = exception.Message
        //};

        context.Result = new NotFoundObjectResult(ResultModel<string>.Create(exception));
        context.ExceptionHandled = true;
    }

    private static void HandleUnauthorizedAccessException(ExceptionContext context)
    {
        //ProblemDetails details = new()
        //{
        //    Status = StatusCodes.Status401Unauthorized,
        //    Title = "Unauthorized",
        //    Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        //};

        context.Result = new ObjectResult(ResultModel<string>.Create(new UnauthorizedAccessException(default), 401))
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };

        context.ExceptionHandled = true;
    }

    private static void HandleForbiddenAccessException(ExceptionContext context)
    {
        ProblemDetails details = new()
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Forbidden",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        };

        context.Result = new ObjectResult(ResultModel<string>.Create(new ForbiddenActionException(401)))
        {
            StatusCode = StatusCodes.Status403Forbidden
        };

        context.ExceptionHandled = true;
    }
}