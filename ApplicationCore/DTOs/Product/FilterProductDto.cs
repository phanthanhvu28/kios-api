using ApplicationCore.ValueObjects;

namespace ApplicationCore.DTOs.Product;
public record FilterProductDto
{
    public List<ValueFilterObject>? Store { get; set; }
    public List<ValueFilterObject>? TypeSale { get; set; }
    public List<ValueFilterObject>? TypeBida { get; set; }
}
