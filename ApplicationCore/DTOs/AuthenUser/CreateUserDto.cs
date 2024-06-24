namespace ApplicationCore.DTOs.AuthenUser;
public record CreateUserDto
{
    public string Username { get; set; }
    public string Fullname { get; set; }
}
