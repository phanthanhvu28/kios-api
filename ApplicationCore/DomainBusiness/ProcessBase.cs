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
    public virtual OneOf<bool, CommonExceptionBase> Execute(IKiosProcess process)
    {
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
