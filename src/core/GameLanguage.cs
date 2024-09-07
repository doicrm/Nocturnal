using Nocturnal.src.ui;
using Nocturnal.src.services;
using Spectre.Console;

namespace Nocturnal.src.core
{
    public enum GameLanguages {
        None = 0,
        EN = 1,
        PL = 2
    }

    public class GameLanguage
    {
        public static readonly Dictionary<GameLanguages, string> LocalizationFileNames = new()
        {
            { GameLanguages.EN, "en" },
            { GameLanguages.PL, "pl" }
        };

        public GameLanguages Language { get; private set; } = GameLanguages.None;

        public GameLanguage() {}

        public GameLanguages GetLanguage()
        {
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
}
