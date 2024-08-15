using ApplicationCore.Entities.Common;

namespace ApplicationCore.DTOs.Role;
public record RoleBaseDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Status { get; set; }
    public string? CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public string? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public List<AuthenMenu> Menus { get; set; } = new List<AuthenMenu>();
}
