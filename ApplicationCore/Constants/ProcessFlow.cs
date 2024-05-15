namespace ApplicationCore.Constants;
public class ProcessFlow
{
    public struct PermissionClaim
    {
        public const string View = "Vela_Core_ContractSupplier_View";
        public const string Approval = "Vela_Core_ContractSupplier_Approve";
        public const string Submit = "Vela_Core_ContractSupplier_Create";
        public const string ApprovalLevel = "Vela_Core_Contract_ApproveLevel";
    }

    public struct Status
    {
        // Process Status
        public const string New = "New";
        public const string Waiting = "Waiting";
        public const string Amending = "Amending";
        public const string Approve = "Approved";
        public const string Active = "Active";
        public const string Inactive = "Inactive";

        public const string Submit = "Submitted";
    }

    public struct PermissionRole
    {
        public const string System = "SYSTEM";
        public const string Admin = "Admin";
    }
}
