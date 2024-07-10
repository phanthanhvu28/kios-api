using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.DomainBusiness;
public class SubmitProcess : ProcessBase, IWorkflowProcess
{

    public SubmitProcess(
        IdentityUserObject? identityUser) : base(identityUser)
    {

    }

    public override string[] ValidStatus { get; set; } =
    {
        Constants.Contract.Status.New,
        Constants.Contract.Status.Amending
    };

    public override OneOf<bool, CommonExceptionBase> Execute(IKiosProcess process)
    {
        process.ActivitiesHistory.Add(
           new Entities.Common.ActivitiesHistory
           {
               DateAction = DateTime.UtcNow,
               UserAction = IdentityUser!.Username,
               Action = "Submit"
           });
        return true;
    }
}
