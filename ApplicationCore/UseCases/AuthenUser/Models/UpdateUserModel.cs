namespace ApplicationCore.UseCases.AuthenUser.Models;
public class UpdateUserModel
{
    public string Username { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string StoreCode { get; set; }
}
