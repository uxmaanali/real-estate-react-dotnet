namespace RealEstate.Shared.Extensions;
using System.ComponentModel;

public static class EnumExtensions
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

    public static string GetDescription<T>(this T enumValue)
           where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
            return null;

        var description = enumValue.ToString();
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        if (fieldInfo != null)
        {
            var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attrs != null && attrs.Length > 0)
            {
                description = ((DescriptionAttribute)attrs[0]).Description;
            }
        }

        return description;
    }

    public static string GetValue<T>(this T enumValue)
           where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
            throw new Exception("Enum is not valid");

        var value = enumValue.ToString();
        return value!;
    }
}
