using Nocturnal.src.entitites;
using Nocturnal.src.services;
using System.Reflection;

namespace Nocturnal.src.core
{
    public static class Globals
    {
        public static Player Player { get; set; } = new();
        public static uint Chapter { get; set; } = 0;
        public static IDictionary<string, Npc> Npcs { get; set; } = new Dictionary<string, Npc>();
        public static IDictionary<string, Item> Items { get; set; } = new Dictionary<string, Item>();
        public static IDictionary<string, Fraction> Fractions { get; set; } = new Dictionary<string, Fraction>();
        public static IDictionary<string, Location> Locations { get; set; } = new Dictionary<string, Location>();
        public static IDictionary<string, Quest> Quests { get; set; } = new Dictionary<string, Quest>();

        public static void UpdateGlobalsFromSave(SaveData saveInfo)
        {
            Player = saveInfo.Player;
            Dictionary<string, Npc> npcs = saveInfo.Npcs.ToObject<Dictionary<string, Npc>>();
            Npcs = npcs;
            Dictionary<string, Location> locations = saveInfo.Locations.ToObject<Dictionary<string, Location>>();
            Locations = locations;
            Dictionary<string, Fraction> fractions = saveInfo.Fractions.ToObject<Dictionary<string, Fraction>>();
            Fractions = fractions;
            Dictionary<string, Quest> quests = saveInfo.Quests.ToObject<Dictionary<string, Quest>>();
            Quests = quests;
            Chapter = saveInfo.Chapter;
            Game.Instance.Weather = saveInfo.Weather;
            StoryGlobals storyGlobals = saveInfo.StoryGlobals.ToObject<StoryGlobals>();
            Game.Instance.StoryGlobals = storyGlobals;
        }

        public static async Task ClearInstances()
        {
            await Task.Run(() =>
            {
                Items.Clear();
                Npcs.Clear();
                Locations.Clear();
                Fractions.Clear();
                Quests.Clear();
            });
        }
    }
}