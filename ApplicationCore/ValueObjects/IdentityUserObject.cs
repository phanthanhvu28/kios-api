namespace ApplicationCore.ValueObjects;

public record IdentityUserObject
{
    public string Email { get; init; }
    public string Roles { get; init; }

    public string Name { get; init; }

    public int ApprovalLevel { get; set; }
    public bool IsSubmit { get; set; }
    public bool IsApproval { get; set; }
    public bool IsView { get; set; }
}