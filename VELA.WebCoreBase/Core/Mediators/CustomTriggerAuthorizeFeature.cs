namespace VELA.WebCoreBase.Core.Mediators;
public class CustomTriggerAuthorizeFeature
{
    /// <summary>
    ///     Have to add this custom override role because DevOps background HAVE NO admin role authorize headers
    /// </summary>
    /// <returns></returns>
    public object GetRoleSystemAdminAuthorize()
    {
        return new { Roles = "Admin", Name = "SYSTEM", Email = "system@vela.com.vn" };
    }
}
