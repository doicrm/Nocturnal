using Nocturnal.ui;

namespace Nocturnal.core.utils;

public static class Input
{
    public static async ValueTask<int> GetChoice()
    {
        await Display.Write("\t> ", 25);
        var input = await Task.Run(() => Console.ReadLine()?.Trim() ?? string.Empty);
        var result = int.TryParse(input, out var choice);
        return result ? choice : -1;
    }

    public static async ValueTask<string> GetString()
    {
        await Display.Write("\t> ", 25);
        return await Task.Run(() => Console.ReadLine()!.Trim());
    }

    public static string ConvertToCamelCase(string input)
    {
        return string.Concat(input.Split(' ')
            .Select((word, index) => index == 0 ? word : CapitalizeFirstLetter(word)));
    }

    private static string CapitalizeFirstLetter(string word) {
        return string.IsNullOrEmpty(word) ? word : $"{char.ToUpper(word[0])}{word.AsSpan(1).ToString()}";
    }
}