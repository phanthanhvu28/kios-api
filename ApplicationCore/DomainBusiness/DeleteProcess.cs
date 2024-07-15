using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.DomainBusiness;
public class DeleteProcess : ProcessBase, IWorkflowProcess
{

    public DeleteProcess(
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
            return new ForbiddenActionException(100006, "delete");
        }

        process.ActivitiesHistory.Add(
           new Entities.Common.ActivitiesHistory
           {
               DateAction = DateTime.UtcNow,
               UserAction = IdentityUser!.Username,
               Action = "Delete"
           });

        process.IsDelete = true;
        process.UsernameEdit = IdentityUser!.Username;
        process.UpdateBy = IdentityUser!.FullName;

        return true;
    }

    private List<string> AppendPin(List<string> pins, string? username)
    {
        pins.Add(username!);
        return pins;
    }
}
