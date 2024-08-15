using ApplicationCore.Contracts.SpecificationBase;
using VELA.WebCoreBase.Core.Models;

namespace ApplicationCore.Specifications.Role;
public class RoleByFilterSpec : SpecificationBase<Entities.Role>
{
    public RoleByFilterSpec(IList<FilterModel> filters = default!,
      IList<string> sorts = default!,
      int page = 1,
      int pageSize = 10)
    {
        sorts.Add("CreateDateDesc");
        sorts.Add("Id");

        ApplyFilter(entity => entity.IsDelete == false);
        ApplySortList(sorts);
        ApplyPaging(page, pageSize);
        ApplyFilterList(filters);
    }
}
