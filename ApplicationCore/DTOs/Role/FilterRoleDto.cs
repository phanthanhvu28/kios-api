using ApplicationCore.Entities.Common;

namespace ApplicationCore.DTOs.Role;
public record FilterRoleDto
{
    public List<AuthenMenu>? Menus { get; set; }
}
