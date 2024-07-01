namespace ApplicationCore.DTOs.Store;
public record StoreBaseDto
{
    public string? Code { get; set; }
    public string? CompanyCode { get; set; }
    public string? Status { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public string? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
}
