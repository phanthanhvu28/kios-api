﻿using ApplicationCore.Constants;
using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;
using VELA.WebCoreBase.Libraries.Extensions;
using ProcessFlow = ApplicationCore.Entities.Common.ProcessFlow;

namespace ApplicationCore.DomainBusiness;

public class ActiveProcess : ProcessBase, IWorkflowProcess
{

    public override string[] ValidStatus { get; set; } = { Contract.Status.Approve };
    public ActiveProcess(IdentityUserObject? identityUser) : base(identityUser)
    {
    }

    public override OneOf<bool, CommonExceptionBase> Execute(IKiosProcess process)
    {
        if (IdentityUser is null)
        {
            return new ForbiddenActionException(100003, "active");
        }


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