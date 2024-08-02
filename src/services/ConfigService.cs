using Newtonsoft.Json;
using Nocturnal.src.core.utilities;
using Nocturnal.src.core;

namespace Nocturnal.src.services
{
    public struct ConfigFileData
    {
        public string? Username { get; set; }
        public GameLanguages Language { get; set; }

        public ConfigFileData(string? username, GameLanguages language)
        {
            Username = username;
            Language = language;
        }
    }

    public class ConfigService
    {
        public const string configFilePath = "data\\config\\config.json";

        public static async ValueTask<bool> LoadConfigFile()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), configFilePath);

            if (!File.Exists(path))
                await CreateConfigFile(path);
            else
            {
                string jsonString;
                ConfigFileData configFileData;

                try
                {
                    jsonString = await File.ReadAllTextAsync(path);
                    configFileData = JsonConvert.DeserializeObject<ConfigFileData>(jsonString)!;
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                    return false;
                }

                Game.Instance.Settings.SetLanguage(configFileData.Language);
            }

            return await LoadDataFromFile(Game.Instance.Settings.GetLanguage());
        }

        public static async Task CreateConfigFile(string filePath)
        {
            if (!Directory.Exists("data\\config"))
                Directory.CreateDirectory("data\\config");

            Game.Instance.Settings.Language.SelectLanguage();

            var configFileData = new ConfigFileData(
                Environment.UserName,
                Game.Instance.Settings.GetLanguage()
            );

            try
            {
                string jsonString = JsonConvert.SerializeObject(configFileData, Formatting.Indented);
                await File.WriteAllTextAsync(filePath, jsonString);
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

        public static async ValueTask<bool> LoadDataFromFile(GameLanguages lang)
        {
            try
            {
                await JsonService.LoadAndParseLocalizationFile(lang);
                return true;
            }
            catch (Exception e)
            {
                await Logger.WriteLog(e.Message);
                return false;
            }
        }
    }
}
