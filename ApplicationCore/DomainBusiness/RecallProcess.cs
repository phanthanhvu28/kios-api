using ApplicationCore.Constants;
using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.DomainBusiness;

public class RecallProcess : ProcessBase, IWorkflowProcess
{

    public RecallProcess(IdentityUserObject? identityUser) : base(identityUser)
    {

    }

    public override string[] ValidStatus { get; set; } = { Contract.Status.Waiting };

    public override OneOf<bool, CommonExceptionBase> Execute(IKiosProcess process)
    {

        return true;
    }
}