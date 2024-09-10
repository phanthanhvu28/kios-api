namespace ApplicationCore.UseCases.Order.Models;
public class CreateOrderModel
{
    public string? OrderCode { get; set; }
    public string? StoreCode { get; set; }
    public string? TableCode { get; set; }
    public string? StaffCode { get; set; }

    //public string? ProductCode { get; set; }
    //public DateTime? OrderDate { get; set; }
    //public DateTime StartTime { get; set; }
    //public DateTime EndTime { get; set; }
    public CreateOrderDetailModel? OrderDetail { get; set; }
}

