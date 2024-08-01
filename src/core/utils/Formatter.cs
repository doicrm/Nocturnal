namespace Nocturnal.src.core.utils
{
    public static class TimeFormatter
    {
        public static string GetFormattedDate()
        {
            DateTime localDate = DateTime.Now;
            return localDate.ToString("dd-MM-yyyy");
        }

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
}
