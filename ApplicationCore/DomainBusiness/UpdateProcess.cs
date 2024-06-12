using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.DomainBusiness;
public class UpdateProcess : ProcessBase, IWorkflowProcess
{

    public UpdateProcess(
        IdentityUserObject? identityUser) : base(identityUser)
    {
    }

    public override string[] ValidStatus { get; set; } =
    {
        Constants.Contract.Status.New
    };

    public override OneOf<bool, CommonExceptionBase> Execute(IKiosProcess process)
    {
        if (IdentityUser is null)
        {
            return new ForbiddenActionException(100006, "update");
        }

        process.ActivitiesHistory.Add(
           new Entities.Common.ActivitiesHistory
           {
               DateAction = DateTime.UtcNow,
               UserAction = IdentityUser!.Username,
               Action = "Update"
           });

        process.UsernameEdit = IdentityUser!.Username;
        process.UpdateBy = IdentityUser!.Name;

        return true;
    }

    private List<string> AppendPin(List<string> pins, string? username)
    {
        pins.Add(username!);
        return pins;
    }
}
