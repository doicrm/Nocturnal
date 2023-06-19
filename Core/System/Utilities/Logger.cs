using System.Diagnostics;
using System.Reflection;

namespace Nocturnal.Core.System.Utilities;

public static class Logger
{
    public static string GetClassAndMethodName()
    {
        StackFrame frame = new(1);
        MethodBase method = frame.GetMethod()!;
        string className = method.DeclaringType!.Name;
        string methodName = method.Name;
        return $"{className}.{methodName}";
    }

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
