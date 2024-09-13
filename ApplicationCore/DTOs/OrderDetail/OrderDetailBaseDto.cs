namespace ApplicationCore.DTOs.OrderDetail;
public record OrderDetailBaseDto
{
    public string? Code { get; set; }
    public string? OrderCode { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Amount { get; set; }
    public string? Status { get; set; }
    public string? StaffCode { get; set; }
    public string? CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public string? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
}
