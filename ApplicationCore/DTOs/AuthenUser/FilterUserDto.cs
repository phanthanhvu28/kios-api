using ApplicationCore.Entities.Common;
using ApplicationCore.ValueObjects;

namespace ApplicationCore.DTOs.AuthenUser;
public record FilterUserDto
{
    public List<ValueFilterObject>? Store { get; set; }
    public List<AuthenMenu>? Menus { get; set; }
    public List<RoleDto> Roles { get; set; }
}
