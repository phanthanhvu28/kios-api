using ApplicationCore.Contracts.SpecificationBase;
using VELA.WebCoreBase.Core.Models;

namespace ApplicationCore.Specifications.Company;
public class CompanyByFilterSpec : SpecificationBase<Entities.Companies>
{
    public CompanyByFilterSpec(IList<FilterModel> filters = default!,
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
