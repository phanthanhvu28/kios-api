using Newtonsoft.Json;
using System.Net;
using VELA.WebCoreBase.Libraries.Exceptions;
using VELA.WebCoreBase.Libraries.Responses;

namespace VELA.WebCoreBase.Core.Models;

public record ResultModel<T>
{
    public T Data { get; protected init; }
    public bool IsError { get; protected init; }


    [JsonIgnore]
    public object Exception { get; set; }

    public string? ErrorMessage { get; set; }

    /// <summary>
    ///     TODO: define / update soon
    /// </summary>
    public int? ErrorCode { get; set; }

    public static ResultModel<T> Create(
        Exception exception,
        int? errorCode = (int)HttpStatusCode.BadRequest,
        params object?[] @params)
    {
        return new ResultModel<T>
        {
            IsError = true,
            Exception = exception,
            ErrorMessage = ErrorContent.CombineMessage(exception.Message, @params),
            ErrorCode = errorCode
        };
    }


    public static ResultModel<T> Create(
        CommonExceptionBase exception)
    {
        return new ResultModel<T>
        {
            IsError = true,
            Exception = exception,
            ErrorMessage = ErrorContent.CombineMessage(exception, exception.Params),
            ErrorCode = exception.ErrorCode
        };
    }

    public static ResultModel<T> Create(T data)
    {
        return new ResultModel<T>
        {
            Data = data
        };
    }
}

public record ListResultModel<T> : ResultModel<object>
{
    public List<T> Items { get; private init; } = new();
    public long TotalItems { get; private init; }
    public int Page { get; private init; }
    public int PageSize { get; private init; }

    public static ListResultModel<T> Create(
        List<T> items,
        long totalItems = 0,
        int page = 1,
        int pageSize = 20,
        object? data = default)
    {
        return new ListResultModel<T>
        {
            Items = items,
            Data = data,

            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize
        };
    }

    public new static ListResultModel<T> Create(
        Exception exception,
        int? errorCode,
        params object?[] @params)
    {
        return new ListResultModel<T>
        {
            IsError = true,
            Exception = exception,
            ErrorMessage = ErrorContent.CombineMessage(exception.Message, @params),
            ErrorCode = errorCode
        };
    }

    public new static ListResultModel<T> Create(
        CommonExceptionBase exception)
    {
        return new ListResultModel<T>
        {
            IsError = true,
            Exception = exception,
            ErrorMessage = ErrorContent.CombineMessage(exception, exception.Params),
            ErrorCode = exception.ErrorCode
        };
    }
}