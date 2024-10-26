using System.Globalization;

namespace Nocturnal.core.utils;

public static class TimeFormatter
{
    public static string GetFormattedDate()
    {
        var localDate = DateTime.Now;
        return localDate.ToString("dd-MM-yyyy");
    }

    public static string GetFormattedTimestamp()
    {
        var localDate = DateTime.Now;
        return Convert.ToString(localDate, CultureInfo.CurrentCulture);
    }

    public static string GetFormattedUtcTimestamp()
    {
        var localDate = DateTime.UtcNow;
        return localDate.ToString(CultureInfo.CurrentCulture);
    }
}