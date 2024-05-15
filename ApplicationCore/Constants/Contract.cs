namespace ApplicationCore.Constants;
public struct Contract
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

    public struct TypeDocument
    {
        public const string PrincipleContract = "PrincipleContract";
        public const string BusinessLicense = "BusinessLicense";
        public const string PowerOfAttorney = "PowerOfAttorney";
    }


    public struct ExchangeRate
    {
        public const string Fix = "Fix";
        public const string Quotation = "Quotation";
        public const string BillingDate = "BillingDate";
    }

    public struct DepositType
    {
        public const string Deposit = "Deposit";
        public const string BankGuarantee = "BankGuarantee";
        public const string Prepaid = "Prepaid";
    }

    public struct PrefixCode
    {
        public const string Contract = "VSCO";
        public const string Annex = "VSAN";
    }

    public struct TypePublishEvent
    {
        public const string Contract = "Contract";
        public const string Annex = "Annex";
    }

    public struct Costing
    {
        public const string Confirmed = "Confirmed";
        public const string Active = "Active";
        public const string Inactive = "Inactive";
    }
    public struct CostingProduct
    {
        public const string SMC = "ServiceMainCharge";
        public const string VAS = "ValueAddedService";
        public const string BLC = "BasicLocalCharge";
        public const string Surcharge = "Surcharge";
    }
    public struct EventCostings
    {
        public const int Quantity = 200;
    }
    public struct EventSleep
    {
        public const int SleepMilliseconds = 100;
    }
}
