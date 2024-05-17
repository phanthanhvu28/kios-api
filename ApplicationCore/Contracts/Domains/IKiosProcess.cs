using ApplicationCore.Entities.Common;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.Contracts.Domains;
public interface IKiosProcess
{
    public string? CreateBy { get; set; }
    public string? Username { get; set; }
    public string? UpdateBy { get; set; }
    public string? UsernameEdit { get; set; }
    public IList<ActivitiesHistory> ActivitiesHistory { get; set; }
    public OneOf<bool, CommonExceptionBase> ProcessStep(IWorkflowProcess workflowProcess);
    public void GenerateCode();
}
