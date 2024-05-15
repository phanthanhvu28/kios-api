using ApplicationCore.Constants;
using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.DomainBusiness;

public class ApprovalProcess : ProcessBase, IWorkflowProcess
{
    private readonly string Action = "Approve";
    private string Type { get; set; }
    public ApprovalProcess(
        IdentityUserObject? identityUser, string type) : base(identityUser)
    {
        Type = type;
    }

    public override string[] ValidStatus { get; set; } = { Contract.Status.Waiting };

    public override OneOf<bool, CommonExceptionBase> Execute(IContractProcess process)
    {

        if (IdentityUser is null || !IdentityUser.IsApproval)
        {
            return new ForbiddenActionException(100006, "approve");
        }

        if (!ValidStatus.Contains(process.Status))
        {
            return new ProcessFlowException(100028, "Approve", Type, process.Status);
        }

        if (InvalidDuration(process.ValidTo.Date))
        {
            return new ValidationException(100037, Type);
        }

        process.ApprovalAt = DateTime.UtcNow;
        process.ApprovalBy = IdentityUser!.Name;

        process.MyPins = AppendPin(process.MyPins, IdentityUser!.Email);

        process.ChangeStatus(
           IdentityUser?.Email!,
           IdentityUser?.Name!,
           Action,
           Contract.Status.Approve, null);

        return true;
    }
    private bool InvalidDuration(DateTime? toDate)
    {
        return toDate < DateTime.UtcNow.Date;
    }
    private List<string> AppendPin(List<string> pins, string? username)
    {
        pins.Add(username!);
        return pins;
    }
}