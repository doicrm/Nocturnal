using System.Reflection;
using Newtonsoft.Json;
using Nocturnal.core;
using Nocturnal.core.utils;
using Nocturnal.entitites;

namespace Nocturnal.services
{
    public abstract class JsonService
    {
        public static async ValueTask<bool> LoadAndParseLocalizationFile(GameLanguages lang)
        {
            try
            {
                var path = GetLocalizationFilePath(lang);
                var jsonString = await File.ReadAllTextAsync(path).ConfigureAwait(false);

                var localizationStrings = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString) ?? [];

                LocalizationService.InitLocalizationStrings(localizationStrings);

                return true;
            }
            catch (Exception ex)
            {
                await Logger.WriteLog($"Error loading localization file: {ex.Message}").ConfigureAwait(false);
                return false;
            }
        }

        private static string GetLocalizationFilePath(GameLanguages lang)
        {
            return Path.Combine(
                Directory.GetCurrentDirectory(),
                "localization",
                $"{GameLanguage.GetLocalizationFileName(lang)}.json");
        }

        public static async ValueTask<string> GetJsonStringAsync(string stringName)
        {
            if (LocalizationService.LocalizationStrings.TryGetValue(stringName, out string? value))
                return value?.ToString() ?? string.Empty;
            await Logger.WriteLog($"Key '{stringName}' not found in LocalizationStrings.").ConfigureAwait(false);
            return string.Empty;
        }

        public static async ValueTask<Dictionary<string, dynamic>> LocationsToJson()
        {
            var tempLocations = new Dictionary<string, dynamic>();

            foreach (var location in Globals.Locations)
            {
                var tempLocation = await Task.Run(() => location.Value.ToJson()).ConfigureAwait(false);
                tempLocations.Add(location.Key, tempLocation);
            }

            return tempLocations;
        }

        public static async ValueTask<Dictionary<string, dynamic>> LocationsFromJson(dynamic locations)
        {
            return await Task.Run(() =>
            {
                var tempLocations = new Dictionary<string, dynamic>();

                foreach (var location in locations)
                {
                    if (location.Value?.EventType == null || location.Value?.EventName == null)
                    {
                        continue;
                    }

                    Type? type = Type.GetType(location.Value.EventType);
                    if (type == null)
                    {
                        continue;
                    }

                    MethodInfo? method = type.GetMethod(location.Value.EventName, BindingFlags.Static | BindingFlags.Public);
                    if (method == null)
                    {
                        continue;
                    }

                    if (method.CreateDelegate(typeof(Action)) is not Action eventDelegate) continue;
                    location.Value.Events = eventDelegate;
                    tempLocations.Add(location.Key, location.Value);
                }

                return tempLocations;
            }).ConfigureAwait(false);
        }

        public static async ValueTask<Location> LocationFromJson(Location loc)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(loc.EventType) || string.IsNullOrEmpty(loc.EventName))
                {
                    return loc;
                }

                Console.WriteLine($"{loc.EventType}.{loc.EventName}");

                var type = Type.GetType(loc.EventType);
                if (type == null)
                {
                    return loc;
                }

                var method = type.GetMethod(loc.EventName, BindingFlags.Static | BindingFlags.Public);
                if (method == null)
                {
                    return loc;
                }

                if (Delegate.CreateDelegate(typeof(Func<Task>), method) is Func<Task> eventFunc)
                {
                    loc.Events = eventFunc;
                }

                return loc;
            }).ConfigureAwait(false);
        }
    }
}
