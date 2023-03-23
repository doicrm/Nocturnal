using System;
using System.Text.Json;

namespace Nocturnal.src
{
    public enum GameLanguages { EN = 1, PL = 2 }

    public struct ConfigFileData
    {
        public string? Username { get; set; }
        public int Language { get; set; }
    }

    internal class GameSettings
    {
        public static int lang = 0;

        public bool LoadConfigFile()
        {
            if (!File.Exists("config.json"))
                CreateConfigFile();
            else
            {
                string jsonString = File.ReadAllText("config.json");
                ConfigFileData configFileData = JsonSerializer.Deserialize<ConfigFileData>(jsonString)!;
                lang = configFileData.Language;
            }

            LoadDataFromFile(lang);

            return true;
        }

        public static void CreateConfigFile()
        {
            lang = SelectLanguage();

            var configFileData = new ConfigFileData
            {
                Username = Environment.UserName,
                Language = lang
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(configFileData, options);
            File.WriteAllText("config.json", jsonString);
        }

        public static void LoadDataFromFile(int lang)
        {
            string fileName = GetFileName(lang);
            string path = Directory.GetCurrentDirectory() + "\\data\\localisation\\" + fileName + ".json";
            File.ReadAllText(path);
        }

        public static int SelectLanguage()
        {
            int choice;
            while (true)
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("\t[1] EN");
                Console.WriteLine("\t[2] PL");
                Console.Write("\t> ");
                choice = Convert.ToInt32(Console.ReadLine());

                if (choice is (int)GameLanguages.EN
                    or (int)GameLanguages.PL)
                    break;
                else continue;
            }

            return choice;
        }

        private static string GetFileName(int lang)
        {
            if (lang is (int)GameLanguages.EN)
                return "en";
            return "pl";
        }
    }
}
