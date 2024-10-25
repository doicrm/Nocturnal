using Newtonsoft.Json;
using Nocturnal.core;
using Nocturnal.core.utils;
using Nocturnal.interfaces;

namespace Nocturnal.services
{
    public struct ConfigFileData(string? username, GameLanguages language)
    {
        public string? Username { get; set; } = username;
        public GameLanguages Language { get; set; } = language;
    }

    public abstract class ConfigService : IConfigCreator, IConfigLoader
    {
        private const string ConfigDirectory = "data\\config";
        private const string ConfigFileName = "config.json";

        private static string GetConfigFilePath() =>
            Path.Combine(Directory.GetCurrentDirectory(), ConfigDirectory, ConfigFileName);

        public static async Task CreateConfigFile()
        {
            var path = GetConfigFilePath();

            if (!Directory.Exists(ConfigDirectory))
                Directory.CreateDirectory(ConfigDirectory);

            var oldLanguage = Game.Instance.Settings.GetLanguage();
            GameLanguage.SelectLanguage();
            var newLanguage = Game.Instance.Settings.GetLanguage();

            if (newLanguage == oldLanguage) return;

            var configFileData = new ConfigFileData(Environment.UserName, newLanguage);

            try
            {
                var jsonString = JsonConvert.SerializeObject(configFileData, Formatting.Indented);
                await File.WriteAllTextAsync(path, jsonString).ConfigureAwait(false);
            }
            catch (JsonException jsonEx)
            {
                await Logger.WriteLog($"JSON Error: {jsonEx.Message}").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.WriteLog($"Error writing config file: {ex.Message}").ConfigureAwait(false);
            }
        }

        public static async ValueTask<bool> LoadConfigFile()
        {
            var path = GetConfigFilePath();

            if (!File.Exists(path))
            {
                await CreateConfigFile().ConfigureAwait(false);
            }
            else
            {
                try
                {
                    var jsonString = await File.ReadAllTextAsync(path);

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
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Error loading config file: {ex.Message}");
                    return false;
                }
            }

            return await JsonService.LoadAndParseLocalizationFile(Game.Instance.Settings.GetLanguage()).ConfigureAwait(false);
        }
    }
}

