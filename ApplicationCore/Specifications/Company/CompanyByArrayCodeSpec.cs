using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Company;
public class CompanyByArrayCodeSpec : SpecificationBase<Entities.Companies>
{
    public CompanyByArrayCodeSpec(string[] codes)
    {
        ApplyFilter(entity => codes.Any(e => e == entity.Code));
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
