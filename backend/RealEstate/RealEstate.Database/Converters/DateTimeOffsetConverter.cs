using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace RealEstate.Database.Converters;
public sealed class DateTimeOffsetConverter : ValueConverter<DateTimeOffset, DateTimeOffset>
{
    public DateTimeOffsetConverter()
        : base(d => d.ToUniversalTime(),
            d => d.ToUniversalTime())
    {
    }
}