using ApplicationCore.Entities.Common;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.Contracts.Domains;
public interface IWorkflowProcess
{
    public string[] ValidStatus { get; set; }

    OneOf<bool, CommonExceptionBase> Execute(IContractProcess process);

    //ProcessFlow? GetProcessFlow(IContractProcess process);
    bool InvalidPermission(ProcessFlow flow);
    bool InvalidEmail(string email);
}
