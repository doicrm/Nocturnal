using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nocturnal.src.core.utilities;
using Nocturnal.src.core;
using Nocturnal.src.entitites;
using System.Reflection;

namespace Nocturnal.src.services
{
    public class JsonService
    {
        public static dynamic? JsonReader { get; set; }

        public static async ValueTask<bool> LoadAndParseLocalizationFile(GameLanguages lang)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "localization", $"{GameLanguage.GetLocalizationFileName(lang)}.json");
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

        public static async ValueTask<Dictionary<string, dynamic>> LocationsToJson()
        {
            var tempLocations = new Dictionary<string, dynamic>();

            foreach (var location in Globals.Locations)
            {
                dynamic tempLocation = await Task.Run(() => location.Value.ToJson());
                tempLocations.Add(location.Key, tempLocation);
            }

            return tempLocations;
        }

        public static async ValueTask<dynamic> LocationsFromJson(dynamic locations)
        {
            return await Task.Run(() =>
            {
                var tempLocations = new Dictionary<string, dynamic>();

                foreach (var location in locations)
                {
                    Type type = Type.GetType(location.Value.EventType)!;

                    if (type != null)
                    {
                        object instance = Activator.CreateInstance(type)!;
                        MethodInfo method = type.GetMethod(location.Value.EventName)!;

                        if (method != null)
                        {
                            location.Value.Events = (Action)instance;
                            tempLocations.Add(location.Key, location.Value);
                        }
                    }
                }
                return tempLocations;
            });
        }

        public static async ValueTask<Location> LocationFromJson(Location loc)
        {
            return await Task.Run(() =>
            {
                Location tempLocation = new();
                Console.WriteLine(loc.EventType + "." + loc.EventName);
                Type type = Type.GetType(loc.EventType!)!;

                if (type != null)
                {
                    MethodInfo method = type.GetMethod(loc.EventName!, BindingFlags.Static | BindingFlags.Public)!;

                    if (method != null)
                    {
                        loc.Events = (Func<Task>)Delegate.CreateDelegate(typeof(Action), method);
                        tempLocation = loc;
                    }
                }
                return tempLocation;
            });
        }
    }
}
