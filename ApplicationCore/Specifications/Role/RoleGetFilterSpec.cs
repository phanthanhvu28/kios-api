using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Role;
public class RoleGetFilterSpec : SpecificationBase<Entities.Role>
{
    public RoleGetFilterSpec()
    {
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
