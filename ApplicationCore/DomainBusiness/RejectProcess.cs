using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.DomainBusiness;
public class RejectProcess : ProcessBase, IWorkflowProcess
{
    public RejectProcess(IdentityUserObject? identityUser) : base(identityUser)
    {

    }

    public override string[] ValidStatus { get; set; } =
    {
        Constants.Contract.Status.Waiting
    };

    public override OneOf<bool, CommonExceptionBase> Execute(IKiosProcess process)
    {

        if (IdentityUser is null || !IdentityUser.IsApproval)
        {
            return new ForbiddenActionException(100006, "reject");
        }


        return true;
    }
    private List<string> AppendPin(List<string> pins, string? username)
    {
        pins.Add(username!);
        return pins;
    }
}
