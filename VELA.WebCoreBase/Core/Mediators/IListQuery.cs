using Mediator;
using VELA.WebCoreBase.Core.Models;

namespace VELA.WebCoreBase.Core.Mediators;

public interface IListQuery<TResponse> : IQuery<ListResultModel<TResponse>> where TResponse : notnull
{
    public List<string> Includes { get; init; }

    public List<FilterModel> Filters { get; init; }
    public List<string> Sorts { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}