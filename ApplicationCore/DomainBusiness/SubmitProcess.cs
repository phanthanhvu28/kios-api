using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.DomainBusiness;
public class SubmitProcess : ProcessBase, IWorkflowProcess
{
    private readonly string Action = "Submit";
    private string Type { get; set; }

    public SubmitProcess(
        IdentityUserObject? identityUser, string type) : base(identityUser)
    {
        Type = type;
    }

    public override string[] ValidStatus { get; set; } =
    {
        Constants.Contract.Status.New,
        Constants.Contract.Status.Amending
    };

    public override OneOf<bool, CommonExceptionBase> Execute(IContractProcess process)
    {

        if (InvalidEmail(process.CreateByEmail!))
        {
            return new ForbiddenActionException(100006, "submit");
        }

        if (!ValidStatus.Contains(process.Status))
        {
            return new ProcessFlowException(100028, "Submit", Type, process.Status);
        }

        process.SubmitBy = IdentityUser!.Name;
        process.SubmitAt = DateTime.UtcNow;

        process.ChangeStatus(
            IdentityUser?.Email!,
            IdentityUser?.Name!,
            Action,
            Constants.Contract.Status.Waiting, null);

        return true;
    }
}
