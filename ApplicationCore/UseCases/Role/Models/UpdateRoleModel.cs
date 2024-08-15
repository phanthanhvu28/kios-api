using ApplicationCore.Entities.Common;

namespace ApplicationCore.UseCases.Role.Models;
public class UpdateRoleModel
{
    public string Code { get; set; }
    public string Name { get; set; }
    public List<AuthenMenu> Menus { get; set; } = new List<AuthenMenu>();
}
