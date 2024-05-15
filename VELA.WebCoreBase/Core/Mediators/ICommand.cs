using VELA.WebCoreBase.Core.Models;

namespace VELA.WebCoreBase.Core.Mediators;

public interface ICommand<TResponse> : Mediator.ICommand<ResultModel<TResponse>> where TResponse : notnull
{
}