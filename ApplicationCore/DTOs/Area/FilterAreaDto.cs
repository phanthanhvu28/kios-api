using ApplicationCore.ValueObjects;

namespace ApplicationCore.DTOs.Area;
public record FilterAreaDto
{
    public List<ValueFilterObject>? Store { get; set; }
}
