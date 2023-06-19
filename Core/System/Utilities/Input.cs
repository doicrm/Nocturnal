using System;

namespace Nocturnal.Core.System.Utilities;

public class Input
{
    public static int GetChoice()
    {
        int choice = 0;
        Display.Write("\t> ", 25);

        try
        {
            choice = int.Parse(Console.ReadLine()!);
        }
        catch (FormatException)
        {

        }

        Console.Out.Flush();
        return choice;
    }

    public static string GetString()
    {
        string text = "";
        Display.Write("\t> ", 25);

        try
        {
            text = Convert.ToString(Console.ReadLine()!);
        }
        catch (FormatException)
        {

        }

        Console.Out.Flush();
        return text;
    }

    public static string ConvertToCamelCase(string input)
    {
        string[] words = input.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            if (i > 0)
            {
                words[i] = CapitalizeFirstLetter(words[i]);
            }
        }

        return string.Join("", words);
    }

    public static string CapitalizeFirstLetter(string word)
    {
        if (string.IsNullOrEmpty(word))
            return word;

        char[] letters = word.ToCharArray();
        letters[0] = char.ToUpper(letters[0]);
        return new string(letters);
    }
}
