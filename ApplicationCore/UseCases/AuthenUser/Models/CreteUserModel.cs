using ApplicationCore.Entities.Common;

namespace ApplicationCore.UseCases.AuthenUser.Models;
public class CreteUserModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public List<AuthenMenu> Menus { get; set; } = new List<AuthenMenu>();
}
