namespace ApplicationCore.DTOs.Area;
public record AreaBaseDto
{
    public string? Code { get; set; }
    public string? StaffCode { get; set; }
    public string? StoreCode { get; set; }
    public string? StoreName { get; set; }
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
