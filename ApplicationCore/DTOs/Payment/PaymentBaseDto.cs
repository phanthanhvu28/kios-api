namespace ApplicationCore.DTOs.Payment;
public record PaymentBaseDto
{
    public string? Code { get; set; }
    public string? StoreCode { get; set; }
    public string? StoreName { get; set; }
    public string? PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public string? Status { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? StaffCode { get; set; }
    public string? CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public string? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
}
