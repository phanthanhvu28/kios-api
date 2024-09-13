using ApplicationCore.DTOs.Order;

namespace ApplicationCore.DTOs.OrderDetail;
public record OrderDetailDto
{
    public OrderBaseDto Order { get; set; }
    public List<OrderDetailBaseDto> Items { get; set; }
}
