using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.DomainBusiness;
public class SaveSubmitProcess : ProcessBase, IWorkflowProcess
{

    public SaveSubmitProcess(IdentityUserObject? identityUser) : base(identityUser)
    {
    }
    public override string[] ValidStatus { get; set; } =
    {
        Constants.Contract.Status.New
    };

    public override OneOf<bool, CommonExceptionBase> Execute(IKiosProcess process)
    {
        if (IdentityUser is null || !IdentityUser.IsSubmit)
        {
            return new ForbiddenActionException(100006, "create new");
        }

        process.CreateBy = IdentityUser!.Name;

        return true;
    }

    private List<string> AppendPin(List<string> pins, string? username)
    {
        pins.Add(username!);
        return pins;
    }
}
