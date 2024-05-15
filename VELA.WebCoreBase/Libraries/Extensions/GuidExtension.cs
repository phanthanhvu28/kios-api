namespace VELA.WebCoreBase.Libraries.Extensions;

public static class GuidExtension
{
    public static string CreateGuid()
    {
        byte[] bytes = Guid.NewGuid().ToByteArray();
        return BitConverter.ToInt64(bytes).ToString();
    }
}