using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nocturnal.src
{
    public static class Globals
    {
        public static Game Game = new();
        public static JObject? JsonReader;
        public static Dictionary<string, Location> Locations = new Dictionary<string, Location>();
    }
}
