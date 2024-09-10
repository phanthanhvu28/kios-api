using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.DomainBusiness;
public class OrderProcess : ProcessBase, IWorkflowProcess
{

    public OrderProcess(
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
            return new ForbiddenActionException(100006, "create new");
        }
        process.Status = Constants.Contract.Status.New;

        process.ActivitiesHistory.Add(
            new Entities.Common.ActivitiesHistory
            {
                DateAction = DateTime.UtcNow,
                UserAction = IdentityUser!.Username,
                Action = "New"
            });
        process.Username = IdentityUser!.Username;
        process.CreateBy = IdentityUser!.FullName;
        process.GenerateCode();

        return true;
    }

    private List<string> AppendPin(List<string> pins, string? username)
    {
        pins.Add(username!);
        return pins;
    }
}
