namespace VELA.WebCoreBase.Libraries.Extensions;

public static class ReflectionExtension
{
    public static object? GetPropertyValue(
        this object? response,
        Type? targetType,
        string propertyName)
    {
        if (response is null || targetType is null)
        {
            return default;
        }

        if (response.GetType() == targetType)
        {
            return targetType.GetProperty(propertyName)?.GetValue(response);
        }

        return default;
    }
}