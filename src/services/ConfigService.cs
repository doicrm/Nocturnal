using Newtonsoft.Json;
using Nocturnal.src.core.utilities;
using Nocturnal.src.core;

namespace Nocturnal.src.services
{
    public struct ConfigFileData
    {
        public string? Username { get; set; }
        public int Language { get; set; }
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

                GameSettings.SetLanguage((GameLanguages)configFileData.Language);
            }

            return await LoadDataFromFile(GameSettings.Lang);
        }

        public static async Task CreateConfigFile(string filePath)
        {
            if (!Directory.Exists("data\\config"))
                Directory.CreateDirectory("data\\config");

            GameSettings.SetLanguage((GameLanguages)GameSettings.SelectLanguage());

            var configFileData = new ConfigFileData
            {
                Username = Environment.UserName,
                Language = GameSettings.Lang
            };

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

        public static async ValueTask<bool> LoadDataFromFile(int lang)
        {
            try
            {
                await JsonService.LoadAndParseJsonContent(lang);
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
