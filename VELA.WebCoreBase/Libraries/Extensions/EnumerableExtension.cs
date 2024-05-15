namespace VELA.WebCoreBase.Libraries.Extensions;

public static class EnumerableExtension
{
    public static bool SetValue<TValue>(
        this IDictionary<string, TValue> dictionary,
        string key,
        TValue value)
    {
        if (dictionary.TryGetValue(key, out _))
        {
            dictionary[key] = value;
            return true;
        }

        return dictionary.TryAdd(key, value);
    }
}