namespace ApplicationCore.DTOs.Company;
public record CompanyBaseDto
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}
