using Newtonsoft.Json;
using Nocturnal.src.core.utilities;
using Nocturnal.src.core;
using Nocturnal.src.interfaces;

namespace Nocturnal.src.services
{
    public struct ConfigFileData(string? username, GameLanguages language)
    {
        public string? Username { get; set; } = username;
        public GameLanguages Language { get; set; } = language;
    }

    public class ConfigService : IConfigCreator, IConfigLoader
    {
        public const string configFilePath = "data\\config\\config.json";

        public static async Task CreateConfigFile()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), configFilePath);

            if (!Directory.Exists("data\\config"))
                Directory.CreateDirectory("data\\config");

            var oldLang = Game.Instance.Settings.GetLanguage();

            GameLanguage.SelectLanguage();

            if (Game.Instance.Settings.GetLanguage() == oldLang) return;

            var configFileData = new ConfigFileData(
                Environment.UserName,
                Game.Instance.Settings.GetLanguage()
            );

            try
            {
                string jsonString = JsonConvert.SerializeObject(configFileData, Formatting.Indented);
                await File.WriteAllTextAsync(path, jsonString);
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

        public static async ValueTask<bool> LoadConfigFile()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), configFilePath);

            if (!File.Exists(path))
                await CreateConfigFile();
            else
            {
                try
                {
                    string jsonString = await File.ReadAllTextAsync(path);

                    ConfigFileData? configFileData = JsonConvert.DeserializeObject<ConfigFileData>(jsonString);

                    if (configFileData.HasValue)
                    {
                        Game.Instance.Settings.SetLanguage(configFileData.Value.Language);
                    }
                    else
                    {
                        throw new InvalidOperationException("Configuration data is null or invalid.");
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                    return false;
                }
            }

            return await JsonService.LoadAndParseLocalizationFile(Game.Instance.Settings.GetLanguage());
        }
    }
}
