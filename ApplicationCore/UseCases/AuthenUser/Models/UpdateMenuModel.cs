using ApplicationCore.Entities.Common;

namespace ApplicationCore.UseCases.AuthenUser.Models;
public class UpdateMenuModel
{
    public string Username { get; set; }
    public List<AuthenMenu> Menus { get; set; } = new List<AuthenMenu>();
    public List<string> Roles { get; set; }
}
