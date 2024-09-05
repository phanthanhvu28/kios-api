using ApplicationCore.Entities.Common;

namespace ApplicationCore.DTOs.AuthenUser;
public record CreateUserDto
{
    public string Username { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string StoreCode { get; set; }
    public string StoreName { get; set; }
    public string CompanyCode { get; set; }
    public string CompanyName { get; set; }
    public List<AuthenMenu> Menus { get; set; }
    public List<string> Roles { get; set; }

}
