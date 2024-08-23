namespace ApplicationCore.UseCases.AuthenUser.Models;
public class UpdateUserRoleModel
{
    public string Username { get; set; }
    public List<string> Roles { get; set; }
}
