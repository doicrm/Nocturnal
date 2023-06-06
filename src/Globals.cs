namespace Nocturnal.src;

public static class Globals
{
    public static Player Player { get; set; } = new();
    public static dynamic? JsonReader { get; set; }
    public static Dictionary<string, Location> Locations { get; set; } = new();
}
