namespace ApplicationCore.Constants;
public struct Kios
{
    public struct Status
    {
        public const string New = "New";
        public const string Amending = "Amending";
        public const string Confirmed = "Confirmed";//=> Portal = Confirmed
        public const string Reject = "Reject";
        public const string Waiting = "Waiting";
        public const string Approve = "Approved";
        public const string Active = "Active";
        public const string Inactive = "Inactive";
    }

}
