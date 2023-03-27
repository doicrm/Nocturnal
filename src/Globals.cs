using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nocturnal.src
{
    public static class Globals
    {
        public static Game Game = new();
        public static Player Player = new();
        public static dynamic? JsonReader;
        public static Dictionary<string, Location> Locations = new();
    }
}
