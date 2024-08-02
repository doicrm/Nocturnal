﻿using Nocturnal.src.core;
using Nocturnal.src.ui;

namespace Nocturnal.src.services
{
    public class LocalizationService
    {
        private static readonly Dictionary<string, (string LanguageName, GameLanguages LanguageEnum)> LanguageMap = new()
        {
            { "en", ("English", GameLanguages.EN) },
            { "pl", ("Polski", GameLanguages.PL) }
        };

        public static MenuOptions GetLocalizationOptions()
        {
            var options = new MenuOptions();

            var localizationFiles = FindLocalizationFiles();

            foreach (string file in localizationFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);

                if (LanguageMap.TryGetValue(fileName, out var languageInfo))
                {
                    string languageName = languageInfo.LanguageName;
                    GameLanguages languageEnum = languageInfo.LanguageEnum;

                    options.Add(languageName, async () =>
                    {
                        Game.Instance.Settings.SetLanguage(languageEnum);
                        await Task.CompletedTask;
                    });
                }
            }

            return options;
        }

        public static List<string> FindLocalizationFiles()
        {
            string localizationDirectory = Path.Combine(Directory.GetCurrentDirectory(), "localization");

            if (Directory.Exists(localizationDirectory))
                return new List<string>(Directory.GetFiles(localizationDirectory, "*.json"));

            Console.WriteLine("The 'localization' directory does not exist.");
            return new List<string>();
        }
    }
}