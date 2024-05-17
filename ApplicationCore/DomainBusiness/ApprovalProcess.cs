using ApplicationCore.Constants;
using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.DomainBusiness;

public class ApprovalProcess : ProcessBase, IWorkflowProcess
{

    public ApprovalProcess(IdentityUserObject? identityUser) : base(identityUser)
    {

    }

    public override string[] ValidStatus { get; set; } = { Contract.Status.Waiting };

    public override OneOf<bool, CommonExceptionBase> Execute(IKiosProcess process)
    {

        if (IdentityUser is null || !IdentityUser.IsApproval)
        {
            return new ForbiddenActionException(100003, "approve");
        }

        return true;
    }
    private bool InvalidDuration(DateTime? toDate)
    {
        return toDate < DateTime.UtcNow.Date;
    }
}