namespace ApplicationCore.DTOs.Table;
public record TableBaseDto
{
    public string? Code { get; set; }
    public string? StaffCode { get; set; }
    public string? StoreCode { get; set; }
    public string? StoreName { get; set; }
    public string? AreaCode { get; set; }
    public string? AreaName { get; set; }
    public string? TypeSaleCode { get; set; }
    public string? TypeSaleName { get; set; }
    public string? TypeBidaCode { get; set; }
    public string? TypeBidaName { get; set; }
    public string? Status { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public string? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public OrderPick? Order { get; set; }
}
public record OrderPick
{
    public string OrderCode { get; set; }
    public string Status { get; set; }
    public decimal TotalCost { get; set; }

}
