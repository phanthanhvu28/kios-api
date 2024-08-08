using ApplicationCore.ValueObjects;

namespace ApplicationCore.DTOs.Table;
public record FilterTableDto
{
    public List<ValueFilterObject>? Store { get; set; }
    public List<ValueFilterObject>? Area { get; set; }
    public List<ValueFilterObject>? TypeSale { get; set; }
    public List<ValueFilterObject>? TypeBida { get; set; }
}
