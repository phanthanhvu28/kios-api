using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Company;
public class CompanyByCodeSpec : SpecificationBase<Entities.Companies>
{
    public CompanyByCodeSpec(string code)
    {
        ApplyFilter(entity => entity.Code == code);
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
