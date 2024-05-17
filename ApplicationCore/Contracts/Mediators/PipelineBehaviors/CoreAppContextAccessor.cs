using ApplicationCore.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.Contracts.Mediators.PipelineBehaviors;

public class CoreAppContextAccessor : AppContextAccessorBase
{
    public CoreAppContextAccessor(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
    }

    protected override void GetIdentityClaims()
    {
        if (UseRegisteredCustomContextAccessor())
        {
            return;
        }
        string? authHeader = HttpContext?.Request.Headers["Authorization"];
        if (authHeader is null)
        {
            return;
        }

        authHeader = authHeader.Replace("Bearer ", string.Empty);

        JwtSecurityToken? jwtSecurityToken = JwtSecurityTokenHandler.ReadToken(authHeader) as JwtSecurityToken;

        Dictionary<string, string>? claims = jwtSecurityToken?.Claims
            .GroupBy(e => e.Type)
            .Select(group => new
            {
                group.Key,
                Value = string.Join(',', group.Select(e => e.Value))
            })
            .ToDictionary(e => e.Key, e => e.Value);

        if (claims is null)
        {
            return;
        }

        IdentityUser = new
        {
            Email = claims!.GetValueOrDefault(JwtRegisteredClaimNames.Email, default)!,
            CompanyCode = claims!.GetValueOrDefault(AccessToken.CompanyCode, default)!,
            StoreCode = claims!.GetValueOrDefault(AccessToken.StoreCode, default)!,
            Username = claims!.GetValueOrDefault(AccessToken.UserName, default)!,
            Roles = claims!.GetValueOrDefault("role", default)!,
            Name = claims!.GetValueOrDefault(JwtRegisteredClaimNames.Name, default)!,
            IsSubmit = Convert.ToBoolean(claims!.GetValueOrDefault(ProcessFlow.PermissionClaim.Submit, default)),
            IsApproval = Convert.ToBoolean(claims!.GetValueOrDefault(ProcessFlow.PermissionClaim.Approval, default)),
            IsView = Convert.ToBoolean(claims!.GetValueOrDefault(ProcessFlow.PermissionClaim.View, default))
        };

    }
    private bool UseRegisteredCustomContextAccessor()
    {
        if (HttpContext?.Request.Path.StartsWithSegments(new PathString("/api/v1/trigger")) is null or false)
        {
            return false;
        }

        CustomTriggerAuthorizeFeature? customFeature =
            HttpContext?.RequestServices.GetService<CustomTriggerAuthorizeFeature>();
        if (customFeature is null)
        {
            return false;
        }

        IdentityUser = customFeature.GetRoleSystemAdminAuthorize();
        return true;
    }
}