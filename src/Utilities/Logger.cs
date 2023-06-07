namespace Nocturnal.src.Utilities;

public class Logger
{
    public static string GetFormattedTimestamp()
    {
        DateTime localDate = DateTime.Now;
        return Convert.ToString(localDate);
    }

    public static string GetFormattedUtcTimestamp()
    {
        DateTime localDate = DateTime.UtcNow;
        return localDate.ToString();
    }
}
