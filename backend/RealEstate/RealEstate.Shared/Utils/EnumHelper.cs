namespace RealEstate.Shared.Utils;
public static class EnumHelper
{
    public static TEnum Parse<TEnum>(string value, bool ignoreCase = true) where TEnum : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        if (Enum.TryParse(value, ignoreCase, out TEnum result))
            return result;

        throw new ArgumentException($"Value '{value}' is not valid for enum {typeof(TEnum).Name}");
    }

    public static bool TryParse<TEnum>(string value, out TEnum result, bool ignoreCase = true)
        where TEnum : struct, Enum
    {
        result = default;
        return !string.IsNullOrWhiteSpace(value) &&
               Enum.TryParse(value, ignoreCase, out result);
    }

    public static TEnum? ParseOrDefault<TEnum>(string value, bool ignoreCase = true)
        where TEnum : struct, Enum
    {
        return TryParse<TEnum>(value, out var result, ignoreCase) ? result : null;
    }
}
