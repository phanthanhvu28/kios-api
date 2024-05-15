using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.DomainBusiness;
public class RejectProcess : ProcessBase, IWorkflowProcess
{

    private readonly string Action = "Reject";
    private string Type { get; set; }
    private string Reason { get; set; }

    public RejectProcess(
        IdentityUserObject? identityUser, string reason, string type) : base(identityUser)
    {
        Reason = reason;
        Type = type;
    }

    public override string[] ValidStatus { get; set; } =
    {
        Constants.Contract.Status.Waiting
    };

    public override OneOf<bool, CommonExceptionBase> Execute(IContractProcess process)
    {

        if (IdentityUser is null || !IdentityUser.IsApproval)
        {
            return new ForbiddenActionException(100006, "reject");
        }

        if (!ValidStatus.Contains(process.Status))
        {
            return new ProcessFlowException(100028, "Reject", Type, process.Status);
        }

        process.RejectAt = DateTime.UtcNow;
        process.RejectBy = IdentityUser!.Name;
        process.RejectStep = process.Step;

        process.MyPins = AppendPin(process.MyPins, IdentityUser!.Email);

        process.ChangeStatus(
            IdentityUser?.Email!,
            IdentityUser?.Name!,
            Action,
            Constants.Contract.Status.Amending, Reason);

        return true;
    }
    private List<string> AppendPin(List<string> pins, string? username)
    {
        pins.Add(username!);
        return pins;
    }
}
