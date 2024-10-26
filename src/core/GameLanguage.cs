using Nocturnal.services;
using Nocturnal.ui;
using Spectre.Console;

namespace Nocturnal.core;

public enum GameLanguages {
    None = 0,
    En = 1,
    Pl = 2
}

public class GameLanguage
{
    private static readonly Dictionary<GameLanguages, string> LocalizationFileNames = new()
    {
        { GameLanguages.En, "en" },
        { GameLanguages.Pl, "pl" }
    };

    private GameLanguages Language { get; set; } = GameLanguages.None;

    public GameLanguage() {}

    public GameLanguages GetLanguage() {
        return Language;
    }

    public void SetLanguage(GameLanguages language)
        => Language = language;

    public static void SelectLanguage()
    {
        Console.ResetColor();
        Console.Clear();

        var languageOptions = LocalizationService.GetLocalizationOptions();

        if (languageOptions.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold red]ERROR:[/] [red]No localization files available.[/]");
            Environment.Exit(-1);
            return;
        }

        _ = new InteractiveMenu(languageOptions);
    }

    public static string GetLocalizationFileName(GameLanguages lang)
    {
        return LocalizationFileNames.TryGetValue(lang, out var fileName) 
            ? fileName 
            : throw new ArgumentException("Unknown language");
    }
}