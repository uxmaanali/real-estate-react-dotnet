namespace RealEstate.Shared.Extensions;

using System;

public static class DateTimeExtensions
{
    public static string ToHumanReadable(this DateTime datetime)
    {
        return datetime.ToString("MMMM dd yyyy");
    }

    public static string ToHumanReadable(this DateTimeOffset datetime)
    {
        return datetime.ToString("MMMM dd yyyy");
    }

    public static string ToDate(this DateTime datetime)
    {
        return datetime.ToString("dd/MM/yyyy");
    }

    public static string Date(this DateTimeOffset datetime)
    {
        return datetime.ToString("dd/MM/yyyy");
    }

    public static string ToDateTime(this DateTime datetime)
    {
        return datetime.ToString("dd/MM/yyyy hh:mm tt");
    }

    public static string DateTime(this DateTimeOffset datetime)
    {
        return datetime.ToString("dd/MM/yyyy hh:mm tt");
    }
}
