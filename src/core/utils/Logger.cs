using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Nocturnal.core.utils;

public static class Logger
{
    public static async Task WriteLog(string message,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        var logMessage = $"{DateTime.Now}\t\t{message} <{Path.GetFileName(filePath)}#{lineNumber}>";
        var logPath = Path.Combine(Directory.GetCurrentDirectory(), "Log.txt");

        await File.AppendAllTextAsync(logPath, logMessage + Environment.NewLine);
    }

    public static async Task WriteStartFunctionLog([CallerMemberName] string funcName = "")
    {
        await WriteLog($"Function {funcName} started");
    }

    public static string GetClassAndMethodName()
    {
        var frame = new StackFrame(1);
        var method = frame.GetMethod();
        return $"{method?.DeclaringType?.Name}.{method?.Name}";
    }
}