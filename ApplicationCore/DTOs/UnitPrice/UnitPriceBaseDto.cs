namespace ApplicationCore.DTOs.UnitPrice;
public sealed class UnitPriceBaseDto
{
    public string StoreCode { get; set; }
    public string ProductCode { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public decimal UnitPrice { get; set; }
    public string? Status { get; set; }
    public string? CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public string? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
}
