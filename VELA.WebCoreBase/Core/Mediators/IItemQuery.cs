using Mediator;
using VELA.WebCoreBase.Core.Models;

namespace VELA.WebCoreBase.Core.Mediators;

public interface IItemQuery<TResponse> : IQuery<ResultModel<TResponse>>
    where TResponse : notnull
{
}