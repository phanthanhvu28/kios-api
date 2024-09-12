namespace ApplicationCore.DTOs.Order;
public record OrderBaseDto
{
    public string? Code { get; set; }
    public string? StoreCode { get; set; }
    public string? StoreName { get; set; }
    public string? TableCode { get; set; }
    public string? TableName { get; set; }
    public decimal TotalCost { get; set; }
    public string? Status { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? StaffCode { get; set; }
    public string? CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public string? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
}
