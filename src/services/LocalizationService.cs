using Nocturnal.core;
using Nocturnal.ui;
using Spectre.Console;

namespace Nocturnal.services
{
    public static class LocalizationService
    {
        private static readonly Dictionary<string, (string LanguageName, GameLanguages LanguageEnum)> LanguageMap = new()
        {
            { "en", ("English", GameLanguages.En) },
            { "pl", ("Polski", GameLanguages.Pl) }
        };

        public static Dictionary<string, string> LocalizationStrings { get; private set; } = [];

        public static MenuOptions GetLocalizationOptions()
        {
            var options = new MenuOptions();

            var localizationFiles = FindLocalizationFiles();

            foreach (var fileName in localizationFiles.Select(Path.GetFileNameWithoutExtension))
            {
                if (!LanguageMap.TryGetValue(fileName ?? string.Empty, out var languageInfo)) continue;
                var languageName = languageInfo.LanguageName;
                var languageEnum = languageInfo.LanguageEnum;

                options.Add(languageName, async () =>
                {
                    Game.Instance.Settings.SetLanguage(languageEnum);
                    await Task.CompletedTask;
                });
            }

            return options;
        }

        private static List<string> FindLocalizationFiles()
        {
            var localizationDirectory = Path.Combine(Directory.GetCurrentDirectory(), "localization");

            if (Directory.Exists(localizationDirectory))
                return [..Directory.GetFiles(localizationDirectory, "*.json")];

            AnsiConsole.MarkupLine("[bold red]ERROR:[/] [red]The 'localization' directory does not exist.[/]");
            return [];
        }

        public static void InitLocalizationStrings(Dictionary<string, string> localizationStringsDictionary)
            => LocalizationStrings = localizationStringsDictionary;
    }
}
