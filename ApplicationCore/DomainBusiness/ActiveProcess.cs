using ApplicationCore.Constants;
using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;
using VELA.WebCoreBase.Libraries.Extensions;
using ProcessFlow = ApplicationCore.Entities.Common.ProcessFlow;

namespace ApplicationCore.DomainBusiness;

public class ActiveProcess : ProcessBase, IWorkflowProcess
{
    private readonly string Action = "Active";
    private string Type { get; set; }
    private bool IsTrigger { get; set; }
    public override string[] ValidStatus { get; set; } = { Contract.Status.Approve };
    public ActiveProcess(IdentityUserObject? identityUser, string type, bool isTrigger = false) : base(identityUser)
    {
        Type = type;
        IsTrigger = isTrigger;
    }

    public override OneOf<bool, CommonExceptionBase> Execute(IContractProcess process)
    {
        if (IdentityUser is null)
        {
            return new ForbiddenActionException(100006, "active");
        }
        if (!IsTrigger)
        {
            if (!ValidStatus.Contains(process.Status))
            {
                return new ValidationException(100028, "Active", Type, process.Status);
            }

            if (!ValidDuration(process.ValidFrom.Date))
            {
                return new ValidationException(100003, process.ValidFrom.ToString("dd/MM/yyyy"), process.ValidTo.ToString("dd/MM/yyyy"));
            }
        }

        process.ChangeStatus(
          IdentityUser?.Email!,
          IdentityUser?.Name!,
          Action,
          Constants.Contract.Status.Active, null);

        return true;
    }

    public override bool InvalidPermission(ProcessFlow? flow)
    {
        return false;
    }
    private bool ValidDuration(DateTime? fromDate)
    {
        return fromDate <= DateTime.UtcNow.Date.ConvertUtcToGtm7();
    }
}