using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Nocturnal.src.core.utilities
{
    public static class Logger
    {
        public static async Task WriteLog(string message,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            string fileName = Path.GetFileName(filePath);
            string logPath = Path.Combine(Directory.GetCurrentDirectory(), "Log.txt");

            string logMessage = $"{DateTime.Now}\t\t{message} <{fileName}#{lineNumber}>";

            await using StreamWriter writer = new(logPath, true);
            await writer.WriteLineAsync(logMessage);
        }

        public static async Task WriteStartFunctionLog([CallerMemberName] string funcName = "")
        {
            await WriteLog($"Function {funcName} started");
        }

        public static string GetClassAndMethodName()
        {
            StackFrame frame = new(1);
            MethodBase method = frame.GetMethod()!;
            string className = method.DeclaringType!.Name;
            string methodName = method.Name;
            return $"{className}.{methodName}";
        }
    }
}
