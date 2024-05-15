using ApplicationCore.Constants;
using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;
using VELA.WebCoreBase.Libraries.Extensions;
using ProcessFlow = ApplicationCore.Entities.Common.ProcessFlow;

namespace ApplicationCore.DomainBusiness;

public class InactiveProcess : ProcessBase, IWorkflowProcess
{
    private const string Action = "Inactive";
    private bool IsTrigger { get; set; }
    private string Type { get; set; }
    public override string[] ValidStatus { get; set; } = { Contract.Status.Active };

    public InactiveProcess(IdentityUserObject? identityUser, string type, bool isTrigger = false) : base(identityUser)
    {
        Type = type;
        IsTrigger = isTrigger;
    }

    public override OneOf<bool, CommonExceptionBase> Execute(IContractProcess process)
    {
        if (IdentityUser is null)
        {
            return new ForbiddenActionException(100006, "Inactive");
        }
        if (!IsTrigger)
        {
            if (!ValidStatus.Contains(process.Status))
            {
                return new ValidationException(100028, "Inactive", Type, process.Status);
            }

            if (!ValidDuration(process.ValidTo.Date))
            {
                return new ValidationException(100017, process.ValidTo);
            }
        }

        process.ChangeStatus(
           IdentityUser?.Email!,
           IdentityUser?.Name!,
           Action,
           Constants.Contract.Status.Inactive, null);

        return true;
    }
    public override bool InvalidPermission(ProcessFlow? flow)
    {
        return false;
    }
    private bool ValidDuration(DateTime? toDate)
    {
        return toDate < DateTime.UtcNow.Date.ConvertUtcToGtm7();
    }
}