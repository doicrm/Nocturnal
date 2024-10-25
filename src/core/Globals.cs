using Nocturnal.entitites;
using Nocturnal.services;

namespace Nocturnal.core
{
    public static class Globals
    {
        public static Player Player { get; set; } = new();
        public static uint Chapter { get; set; } = 0;
        public static Dictionary<string, Npc> Npcs { get; set; } = [];
        public static Dictionary<string, Item> Items { get; set; } = [];
        public static Dictionary<string, Fraction> Fractions { get; set; } = [];
        public static Dictionary<string, Location> Locations { get; set; } = [];
        public static Dictionary<string, Quest> Quests { get; set; } = [];

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