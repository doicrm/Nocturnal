using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nocturnal.src.core.utilities;
using Nocturnal.src.core;

namespace Nocturnal.src.services
{
    public class JsonService
    {
        public static dynamic? JsonReader { get; set; }

        public static async ValueTask<bool> LoadAndParseLocalizationFile(GameLanguages lang)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "localization", GameLanguage.GetLocalizationFileName(lang) + ".json");
                string jsonString = await File.ReadAllTextAsync(path);
                JsonReader = JObject.Parse(jsonString);
                return true;
            }
            catch (Exception e)
            {
                await Logger.WriteLog(e.Message);
                return false;
            }
        }

        public static async Task<string> GetJsonStringAsync(string stringName)
        {
            try
            {
                if (JsonReader == null || !JsonReader!.ContainsKey(stringName))
                {
                    await Logger.WriteLog($"Key '{stringName}' not found in JsonReader.");
                    return string.Empty;
                }
                return JsonReader![stringName].ToString();
            }
            catch (JsonException e)
            {
                await Logger.WriteLog($"JsonException: {e.Message}");
                return string.Empty;
            }
            catch (Exception e)
            {
                await Logger.WriteLog($"Exception: {e.Message}");
                return string.Empty;
            }
        }

        public static string GetJsonString(string stringName)
        {
            return GetJsonStringAsync(stringName).GetAwaiter().GetResult();
        }
    }
}
