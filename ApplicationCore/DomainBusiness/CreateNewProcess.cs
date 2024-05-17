﻿using ApplicationCore.Contracts.Domains;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.DomainBusiness;
public class CreateNewProcess : ProcessBase, IWorkflowProcess
{

    public CreateNewProcess(
        IdentityUserObject? identityUser) : base(identityUser)
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
            return new ForbiddenActionException(100003, "create new");
        }

        process.ActivitiesHistory.Add(
            new Entities.Common.ActivitiesHistory
            {
                DateAction = DateTime.UtcNow,
                UserAction = IdentityUser!.Username,
                Action = "New"
            });
        process.Username = IdentityUser!.Username;
        process.CreateBy = IdentityUser!.Name;
        process.GenerateCode();

        return true;
    }

    private List<string> AppendPin(List<string> pins, string? username)
    {
        pins.Add(username!);
        return pins;
    }
}
