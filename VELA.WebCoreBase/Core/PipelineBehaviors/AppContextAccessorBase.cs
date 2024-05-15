using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace VELA.WebCoreBase.Core.PipelineBehaviors;

public interface IAppContextAccessor
{
    HttpContext? HttpContext { get; }
    Activity? ActivityContext { get; }
    string TraceId { get; }


    object? IdentityUser { get; set; }

    bool IsAuthenticated { get; }
}

public abstract class AppContextAccessorBase : IAppContextAccessor
{
    protected static readonly JwtSecurityTokenHandler JwtSecurityTokenHandler = new();
    protected readonly IHttpContextAccessor _httpContextAccessor;

    public AppContextAccessorBase(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        GetIdentityClaims();
    }

    public HttpContext? HttpContext => _httpContextAccessor.HttpContext;

    public Activity? ActivityContext => Activity.Current;
    public object? IdentityUser { get; set; }
    public string TraceId => ActivityContext?.Id;

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User?.Identities?.FirstOrDefault()?.IsAuthenticated ?? false;

    protected virtual void GetIdentityClaims()
    {
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
            Roles = claims!.GetValueOrDefault("role", default)!,
            Name = claims!.GetValueOrDefault(JwtRegisteredClaimNames.Name, default)!
        };
    }
}