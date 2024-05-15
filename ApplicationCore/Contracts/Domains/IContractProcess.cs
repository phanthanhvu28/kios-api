using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.Contracts.Domains;
public interface IContractProcess
{
    public void ChangeStatus(string email, string username, string action, string newStatus, string reason,
        DateTime? validFromAnnex = null,
        DateTime? validToAnnex = null);
    public void RollbackStatus(string email, string username, string action);

    public int? LevelResetRejected { get; set; }

    public string? Status { get; set; }
    public int SortStatus { get; set; }

    public string? ApprovalBy { get; set; }

    public DateTime? ApprovalAt { get; set; }

    public string? SubmitBy { get; set; }
    public DateTime? SubmitAt { get; set; }

    //public bool IsReject { get; set; }
    public string? RejectBy { get; set; }
    public DateTime? RejectAt { get; set; }
    public DateTime CreateDate { get; set; }
    public int? RejectStep { get; set; }

    public string? MyPinned { get; set; }
    public List<string> MyPins { get; set; }

    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }


    public int Step { get; set; }
    public string? CreateBy { get; set; }
    public string? CreateByEmail { get; set; }

    public OneOf<bool, CommonExceptionBase> ProcessStep(IWorkflowProcess workflowProcess);
    //public void InitProcessFlow(IList<ProcessFlow> processFlows);
    public void GenerateCode();
}
