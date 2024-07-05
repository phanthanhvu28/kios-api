using ApplicationCore.ValueObjects;

namespace ApplicationCore.DTOs.Company;
public record FilterCompanyDto
{
    public List<ValueFilterObject>? Company { get; set; }
}
