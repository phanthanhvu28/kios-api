using ApplicationCore.Constants;
using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.DomainBusiness;

public class RecallProcess : ProcessBase, IWorkflowProcess
{
    private static readonly string Action = "Recall";
    private string Type { get; set; }
    public RecallProcess(
        IdentityUserObject? identityUser, string type) : base(identityUser)
    {
        Type = type;
    }

    public override string[] ValidStatus { get; set; } = { Contract.Status.Waiting };

    public override OneOf<bool, CommonExceptionBase> Execute(IContractProcess process)
    {
        if (InvalidEmail(process.CreateByEmail!))
        {
            return new ForbiddenActionException(100006, "recall");
        }

        if (!ValidStatus.Contains(process.Status))
        {
            return new ProcessFlowException(100028, "Recall", Type, process.Status);
        }

        process.SubmitBy = IdentityUser!.Name;
        process.SubmitAt = DateTime.UtcNow;

        process.RollbackStatus(IdentityUser?.Email!, IdentityUser?.Name!, Action);

        return true;
    }
}