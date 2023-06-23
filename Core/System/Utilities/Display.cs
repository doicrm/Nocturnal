using System.ComponentModel.Design;

namespace Nocturnal.Core.System.Utilities;

public class Display
{
    public static void Write(string text, int speed = 50)
    {
        foreach (char letter in text)
        {
            Console.Write(letter);
            Console.Out.Flush();
            Thread.Sleep(speed);
        }
    }

    public static void WriteNarration(string text, int speed = 50)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Write(text, speed);
        Console.ResetColor();
    }

    public static void WriteDialogue(string text, int speed = 50)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Write(text, speed);
        Console.ResetColor();
    }

    public static string GetJsonString(string stringName)
    {
        if (stringName == null) return "";
        return Globals.JsonReader![stringName].ToString();
    }
}
