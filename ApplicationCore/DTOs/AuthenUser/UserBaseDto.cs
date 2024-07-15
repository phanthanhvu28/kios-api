using ApplicationCore.Entities.Common;

namespace ApplicationCore.DTOs.AuthenUser;
public record UserBaseDto
{
    public string? StoreCode { get; set; }
    public string? StoreName { get; set; }
    public string? Username { get; set; }
    public string? Fullname { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public string? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public List<AuthenMenu> Menus { get; set; } = new List<AuthenMenu>();
}
