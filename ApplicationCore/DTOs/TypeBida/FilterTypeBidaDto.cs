using ApplicationCore.ValueObjects;

namespace ApplicationCore.DTOs.TypeBida;
public record FilterTypeBidaDto
{
    public List<ValueFilterObject>? Store { get; set; }
}
