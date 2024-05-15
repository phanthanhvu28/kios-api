using ApplicationCore.Contracts.Domains;
using ApplicationCore.Entities.Common;
using ApplicationCore.ValueObjects;
using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.DomainBusiness;
public abstract class ProcessBase
{
    protected readonly IdentityUserObject? IdentityUser;

    protected ProcessBase()
    {
    }

    protected ProcessBase(IdentityUserObject? identityUser)
    {
        IdentityUser = identityUser;
    }


    public abstract string[] ValidStatus { get; set; }

    /// <summary>
    ///     Base Execute Flow
    /// </summary>
    /// <param name="process"></param>
    /// <returns></returns>
    public virtual OneOf<bool, CommonExceptionBase> Execute(IContractProcess process)
    {
        if (!ValidStatus.Contains(process.Status))
        {
            return new ProcessFlowException(100010, process.Status);
        }

        //ProcessFlow? flow = GetProcessFlow(process);
        //if (InvalidPermission(flow!))
        //{
        //    return new ForbiddenActionException(100006, process.Status);
        //}


        if (InvalidEmail(process.CreateByEmail!))
        {
            return new ForbiddenActionException(100006, process.Status);
        }
        return true;
    }

    /// <summary>
    ///     Valid the Audit User is have permission in Flow
    /// </summary>
    /// <param name="flow"></param>
    /// <returns></returns>
    public virtual bool InvalidPermission(ProcessFlow? flow)
    {
        if (IdentityUser is null || flow is null)
        {
            return true;
        }

        return flow.Email != IdentityUser.Email;
    }

    public virtual bool InvalidEmail(string email)
    {
        if (IdentityUser is null || email is null)
        {
            return true;
        }

        return email != IdentityUser.Email;
    }

    /// <summary>
    ///     Get User info in Flow
    /// </summary>
    /// <param name="process"></param>
    /// <returns></returns>
   // public abstract ProcessFlow? GetProcessFlow(IContractProcess process);
}
