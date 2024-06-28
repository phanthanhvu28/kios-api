namespace ApplicationCore.DTOs.AuthenUser;
public record CreateUserDto
{
    public string Username { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }

}
