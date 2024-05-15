using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.DomainBusiness;
public class SaveSubmitProcess : ProcessBase, IWorkflowProcess
{
    private readonly string ActionSubmit = "Submit";
    private readonly string ActionNew = "Create";

    public SaveSubmitProcess(
        IdentityUserObject? identityUser) : base(identityUser)
    {
    }
    public override string[] ValidStatus { get; set; } =
    {
        Constants.Contract.Status.New
    };

    public override OneOf<bool, CommonExceptionBase> Execute(IContractProcess process)
    {
        if (IdentityUser is null || !IdentityUser.IsSubmit)
        {
            return new ForbiddenActionException(100006, "create new");
        }

        if (!ValidStatus.Contains(process.Status))
        {
            return new ProcessFlowException(100010, "create new");
        }

        process.CreateBy = IdentityUser!.Name;
        process.CreateByEmail = IdentityUser.Email;

        process.SubmitBy = IdentityUser!.Name;
        process.SubmitAt = DateTime.UtcNow;

        process.MyPins = AppendPin(process.MyPins, IdentityUser!.Email);

        process.ChangeStatus(
            IdentityUser?.Email!,
            IdentityUser?.Name!,
            ActionNew,
            Constants.Contract.Status.New, null);

        process.ChangeStatus(
           IdentityUser?.Email!,
           IdentityUser?.Name!,
           ActionSubmit,
           Constants.Contract.Status.Waiting, null);

        return true;
    }

    private List<string> AppendPin(List<string> pins, string? username)
    {
        pins.Add(username!);
        return pins;
    }
}
