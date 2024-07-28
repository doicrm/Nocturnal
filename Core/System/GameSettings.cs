using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nocturnal.Core.Entitites;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.System
{
    public enum GameLanguages { EN = 1, PL = 2 }

    public struct ConfigFileData
    {
        public string? Username { get; set; }
        public int Language { get; set; }
    }

    public class GameSettings
    {
        public static int Lang { get; private set; }

        public GameSettings() { Lang = 0; }

        public static async ValueTask<bool> LoadConfigFile()
        {
            if (!File.Exists("config.json"))
                await CreateConfigFile();
            else
            {
                string jsonString;
                ConfigFileData configFileData;

                try
                {
                    jsonString = await File.ReadAllTextAsync("config.json");
                    configFileData = JsonConvert.DeserializeObject<ConfigFileData>(jsonString)!;
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                    return false;
                }

                Lang = configFileData.Language;
            }

            return await LoadDataFromFile(Lang);
        }

        public static async Task CreateConfigFile()
        {
            Lang = SelectLanguage();

            var configFileData = new ConfigFileData
            {
                Username = Environment.UserName,
                Language = Lang
            };

            try
            {
                string jsonString = JsonConvert.SerializeObject(configFileData, Formatting.Indented);
                await File.WriteAllTextAsync("config.json", jsonString);
            }
            catch (JsonException e)
            {
                await Logger.WriteLog(e.Message);
            }
            catch (Exception e)
            {
                await Logger.WriteLog(e.Message);
            }
        }

        public static async ValueTask<bool> LoadDataFromFile(int lang)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Data\\Lang\\" + GetFileName(lang) + ".json");
                string jsonString = await File.ReadAllTextAsync(path);
                Globals.JsonReader = JObject.Parse(jsonString);
                return true;
            }
            catch (Exception e)
            {
                await Logger.WriteLog(e.Message);
                return false;
            }
        }

        public static int SelectLanguage()
        {
            int choice = 0;

            Console.ResetColor();
            Console.Clear();

            _ = new InteractiveMenu(new Dictionary<string, Func<Task>>()
            {
                { "English", async () => { choice = (int)GameLanguages.EN; await Task.CompletedTask; } },
                { "Polski", async () => { choice = (int)GameLanguages.PL; await Task.CompletedTask; } },
            });

            return choice;
        }

        public static bool IsSetLanguage(GameLanguages lang)
        {
            return Lang == (uint)lang;
        }

        private static string GetFileName(int lang)
        {
            return lang is (int)GameLanguages.EN ? "EN_en" : "PL_pl";
        }
    }
}
