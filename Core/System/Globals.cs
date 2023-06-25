using Nocturnal.Core.Entitites;
using Nocturnal.Core.Entitites.Characters;
using Nocturnal.Core.Entitites.Items;
using System.Reflection;

namespace Nocturnal.Core.System
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

        public static dynamic LocationsToJson()
        {
            Dictionary<string, dynamic> TempLocations = new();

            foreach (var location in Locations)
            {
                dynamic TempLocation = location.Value.ToJson();
                TempLocations.Add(location.Key, TempLocation);
            }

            return TempLocations;
        }

        public static dynamic LocationsFromJson(dynamic locations)
        {
            Dictionary<string, dynamic> TempLocations = new();

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
                        TempLocations.Add(location.Key, location.Value);
                    }
                }
            }
            return TempLocations;
        }

        public static Location LocationFromJson(Location loc)
        {
            Location TempLocation = new();
            Console.WriteLine(loc.EventType + "." + loc.EventName);
            Type type = Type.GetType(loc.EventType!)!;

            if (type != null)
            {
                MethodInfo method = type.GetMethod(loc.EventName!, BindingFlags.Static | BindingFlags.Public)!;

                if (method != null)
                {
                    loc.Events = (Action)Delegate.CreateDelegate(typeof(Action), method);
                    TempLocation = loc;
                }
            }
            return TempLocation;
        }
    }
}
