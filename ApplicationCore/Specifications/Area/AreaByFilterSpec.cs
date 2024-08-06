using ApplicationCore.Contracts.SpecificationBase;
using VELA.WebCoreBase.Core.Models;

namespace ApplicationCore.Specifications.Area;
public class AreaByFilterSpec : SpecificationBase<Entities.Areas>
{
    public AreaByFilterSpec(IList<FilterModel> filters = default!,
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
