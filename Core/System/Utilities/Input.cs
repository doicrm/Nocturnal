namespace Nocturnal.Core.System.Utilities;

public class Input
{
    public static int GetChoice()
    {
        int choice;
        Display.Write("\n\t> ", 25);
        choice = Convert.ToInt32(Console.ReadLine());
        Console.Out.Flush();
        return choice;
    }

    public static string GetString()
    {
        string text;
        Display.Write("\n\t> ", 25);
        text = Convert.ToString(Console.ReadLine())!;
        Console.Out.Flush();
        return text;
    }
}
