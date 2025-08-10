using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace RealEstate.Database.Converters;
public class UrlListToStringConverter : ValueConverter<List<string>, string>
{
    public UrlListToStringConverter()
        : base(
            urls => string.Join(";", urls ?? new List<string>()),
            str => string.IsNullOrWhiteSpace(str)
                ? new List<string>()
                : str.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList()
        )
    {
    }
}