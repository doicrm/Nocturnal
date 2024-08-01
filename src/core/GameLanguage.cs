using Nocturnal.src.ui;
using Nocturnal.src.services;

namespace Nocturnal.src.core
{
    public enum GameLanguages { NONE = 0, EN = 1, PL = 2 }

    public class GameLanguage
    {
        public static readonly Dictionary<GameLanguages, string> LocalizationFileNames = new()
        {
            { GameLanguages.EN, "en" },
            { GameLanguages.PL, "pl" }
        };

        public GameLanguages Language { get; private set; } = GameLanguages.NONE;

        public GameLanguage() {}

        public GameLanguages GetLanguage()
        {
            return Language;
        }

        public void SetLanguage(GameLanguages language)
            => Language = language;

        public void SelectLanguage()
        {
            Console.ResetColor();
            Console.Clear();

            var languageOptions = LocalizationService.GetLocalizationOptions();

            if (languageOptions.Count == 0)
            {
                Console.WriteLine("No localization files available.");
                Environment.Exit(-1);
                return;
            }

            _ = new InteractiveMenu(languageOptions);
        }

        public bool IsSetLanguage(GameLanguages language)
        {
            return Language == language;
        }

        public static string GetLocalizationFileName(GameLanguages lang)
        {
            if (LocalizationFileNames.TryGetValue(lang, out var fileName))
                return fileName;
            throw new ArgumentException("Unknown language");
        }
    }
}
