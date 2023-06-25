using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Nocturnal.Core.System.Utilities
{
    public static class Logger
    {
        public static void WriteLog(string message,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            string fileName = Path.GetFileName(filePath);
            string logPath = $"{Directory.GetCurrentDirectory()}\\Log.txt";
            using StreamWriter writer = new(logPath, true);
            writer.WriteLine($"{DateTime.Now}\t\t{message} <{fileName}#{lineNumber}>");
        }

        public static void WriteStartFunctionLog([CallerMemberName] string funcName = "")
        {
            WriteLog($"Function {funcName} started");
        }

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
}
