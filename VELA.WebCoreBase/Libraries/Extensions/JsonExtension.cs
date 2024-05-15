using Newtonsoft.Json;

namespace VELA.WebCoreBase.Libraries.Extensions;

public static class JsonExtension
{
    public static string EncapsulateObject(this object jsonObject)
    {
        return JsonConvert.SerializeObject(jsonObject, new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        });
    }
}