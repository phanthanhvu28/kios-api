using ApplicationCore.ValueObjects;

namespace ApplicationCore.DTOs.TypeSale;
public record FilterTypeSaleDto
{
    public List<ValueFilterObject>? Store { get; set; }
}
