namespace ApplicationCore.Constants;
public struct File
{
    public struct Config
    {
        public const int MaxFile = 15;
        public const int MaxSize = 5242880; // 5MB =  5242880 Bytes                
        public const int MaxFileDeposit = 5;
    }
    public struct InvalidType
    {
        public const string MP3 = ".mp3";
        public const string AU = ".au";
        public const string AIFF = ".aiff";
        public const string WAV = ".wav";
        public const string WMA = ".wma";
        public const string GIF = ".gif";
        public const string MP4 = ".mp4";
        public const string M4V = ".m4v";
        public const string MOV = ".mov";
        public const string WMV = ".wmv";
        public const string ASF = ".asf";
        public const string MPEG = ".mpeg";
    }

    public struct Status
    {
        public const int New = 0;
        public const int Delete = 1;
    }
}
public struct Folder
{
    public const string Temp = "temp";
    public const string Contract = "contract";
    public const string Annex = "annex";
    public struct DepositType
    {
        public const string Deposit = "deposit";
        public const string BankGuarantee = "bankguarantee";
        public const string Prepaid = "prepaid";
    }
}