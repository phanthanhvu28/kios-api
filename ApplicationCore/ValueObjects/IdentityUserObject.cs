namespace ApplicationCore.ValueObjects;

public record IdentityUserObject
{
    public string Email { get; init; }
    public string Roles { get; init; }

    public string FullName { get; init; }
    public string CompanyCode { get; init; }
    public string StaffCode { get; init; }
    public string StoreCode { get; init; }
    public string Username { get; init; }//username
    public string Menus { get; init; }

    public int ApprovalLevel { get; set; }
    public bool IsSubmit { get; set; }
    public bool IsApproval { get; set; }
    public bool IsView { get; set; }
}