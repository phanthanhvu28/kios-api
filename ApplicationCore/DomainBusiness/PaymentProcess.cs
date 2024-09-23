using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.DomainBusiness;
public class PaymentProcess : ProcessBase, IWorkflowProcess
{

    public PaymentProcess(
        IdentityUserObject? identityUser) : base(identityUser)
    {
    }

    public override string[] ValidStatus { get; set; } =
    {
        Constants.Order.Status.New,
        Constants.Order.Status.Partial,
    };

    public override OneOf<bool, CommonExceptionBase> Execute(IKiosProcess process)
    {
        if (IdentityUser is null)
        {
            return new ForbiddenActionException(100006, "create new");
        }

        //if (!ValidStatus.Contains(process.Status))
        //{
        //    return new ProcessFlowException(100036, "Payment");
        //}
        process.Status = Constants.Order.Status.Finish;

        process.ActivitiesHistory.Add(
            new Entities.Common.ActivitiesHistory
            {
                DateAction = DateTime.UtcNow,
                UserAction = IdentityUser!.Username,
                Action = "Payment"
            });
        process.Username = IdentityUser!.Username;
        process.CreateBy = IdentityUser!.FullName;
        process.GenerateCode();

        return true;
    }
}
