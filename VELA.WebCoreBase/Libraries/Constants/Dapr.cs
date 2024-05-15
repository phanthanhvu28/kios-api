namespace VELA.WebCoreBase.Libraries.Constants;

public struct Dapr
{
    public const string PubSubName = "Dapr:PubSub";
    public const string DaprHttp = "Dapr:Host";
    public const string DaprGrpc = "Dapr:Grpc";

    public static readonly Dictionary<string, string> DefaultStateStoreMetadata = new()
    {
        { "ttlInSeconds", "6000" }
    };
}