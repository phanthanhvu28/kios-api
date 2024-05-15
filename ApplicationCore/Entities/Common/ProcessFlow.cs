namespace ApplicationCore.Entities.Common;
public class ProcessFlow
{
    public int Level { get; init; } = 0;

    public string RoleDisplayName { get; init; }

    public string Email { get; init; }
    public string? Username { get; init; }

    public string GeneralStatus { get; set; }
    public string? SelfStatus { get; set; }

    public DateTime? ProcessActionAt { get; set; }
}
