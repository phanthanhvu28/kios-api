namespace ApplicationCore.UseCases.Order.Models;
public class CreateOrderDetailModel
{
    public string? ProductCode { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
