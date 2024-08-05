using ApplicationCore.ValueObjects;

namespace ApplicationCore.DTOs.Staff;
public record FilterStaffDto
{
    public List<ValueFilterObject>? Store { get; set; }
}
