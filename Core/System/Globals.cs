using Nocturnal.Core.Entitites;
using Nocturnal.Core.Entitites.Living;

namespace Nocturnal.Core.System;

public static class Globals
{
    public static Player Player { get; set; } = new();
    public static dynamic? JsonReader { get; set; }
    public static Dictionary<string, Location> Locations { get; set; } = new();
}
