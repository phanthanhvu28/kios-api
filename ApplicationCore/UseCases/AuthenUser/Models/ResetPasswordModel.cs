namespace ApplicationCore.UseCases.AuthenUser.Models;
public class ResetPasswordModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmedPass { get; set; }
}
