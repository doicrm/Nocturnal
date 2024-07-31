using Nocturnal.src.entitites;
using System.Reflection;

namespace Nocturnal.src.core
{
    public static class Globals
    {
        public static Player Player { get; set; } = new();
        public static dynamic? JsonReader { get; set; }
        public static uint Chapter { get; set; } = 0;
        public static IDictionary<string, Npc> Npcs { get; set; } = new Dictionary<string, Npc>();
        public static IDictionary<string, Item> Items { get; set; } = new Dictionary<string, Item>();
        public static IDictionary<string, Fraction> Fractions { get; set; } = new Dictionary<string, Fraction>();
        public static IDictionary<string, Location> Locations { get; set; } = new Dictionary<string, Location>();
        public static IDictionary<string, Quest> Quests { get; set; } = new Dictionary<string, Quest>();

        public static async ValueTask<dynamic> LocationsToJson()
        {
            var tempLocations = new Dictionary<string, dynamic>();

            foreach (var location in Locations)
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