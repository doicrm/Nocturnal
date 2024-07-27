using Newtonsoft.Json;
using Nocturnal.Core.Entitites;
using Nocturnal.Core.Entitites.Characters;
using Nocturnal.Core.Entitites.Items;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.System
{
    public struct SaveData
    {
        public string Timestamp;
        public Player Player;
        public dynamic Npcs;
        public dynamic Locations;
        public dynamic Fractions;
        public dynamic Quests;
        public uint Chapter;
        public dynamic CurrentLocation;
        public Weather Weather;
        public dynamic StoryGlobals;
    };

    public class SaveManager
    {
        private static uint CurrentSaveNr = 0;

        public static async Task CreateSave()
        {
            if (!Directory.Exists("Data\\Saves"))
                Directory.CreateDirectory("Data\\Saves");

            for (int i = 0; i < Constants.MAX_SAVES; i++)
            {
                string path = $"{Directory.GetCurrentDirectory()}\\Data\\Saves\\Save{i}.dat";

                if (!File.Exists(path))
                {
                    using StreamWriter newSave = File.CreateText(path);
                    CurrentSaveNr = (uint)i;
                    SaveData save = new()
                    {
                        Timestamp = Logger.GetFormattedTimestamp(),
                        Player = Globals.Player,
                        Npcs = Globals.Npcs,
                        Locations = await Globals.LocationsToJson(),
                        Fractions = Globals.Fractions,
                        Quests = Globals.Quests,
                        Chapter = Globals.Chapter,
                        CurrentLocation = null!,
                        Weather = Program.Game!.Weather,
                        StoryGlobals = Program.Game!.StoryGlobals
                    };

                    var serializedObject = JsonConvert.SerializeObject(save, Formatting.Indented);
                    await newSave.WriteAsync(serializedObject);
                    break;
                }
            }
        }

        public static async Task LoadSave(uint nr)
        {
            Item.InsertInstances();
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\Saves\\Save{nr}.dat");

            if (!File.Exists(path))
            {
                await Program.Game!.LoadLogo();
                return;
            }

            CurrentSaveNr = nr;

            string content = await File.ReadAllTextAsync(path);
            var saveInfo = JsonConvert.DeserializeObject<SaveData>(content);

            Globals.Player = saveInfo.Player;
            Dictionary<string, Npc> npcs = saveInfo.Npcs.ToObject<Dictionary<string, Npc>>();
            Globals.Npcs = npcs;
            Dictionary<string, Location> locations = saveInfo.Locations.ToObject<Dictionary<string, Location>>();
            Globals.Locations = locations;
            Dictionary<string, Fraction> fractions = saveInfo.Fractions.ToObject<Dictionary<string, Fraction>>();
            Globals.Fractions = fractions;
            Dictionary<string, Quest> quests = saveInfo.Quests.ToObject<Dictionary<string, Quest>>();
            Globals.Quests = quests;
            Globals.Chapter = saveInfo.Chapter;
            Program.Game!.Weather = saveInfo.Weather;
            StoryGlobals storyGlobals = saveInfo.StoryGlobals.ToObject<StoryGlobals>();
            Program.Game!.StoryGlobals = storyGlobals;

            Location currentLocation;

            if (saveInfo.CurrentLocation != null)
                currentLocation = await saveInfo.CurrentLocation.ToObject<Location>();
            else
                currentLocation = locations["DarkAlley"];

            if (!Globals.Locations.ContainsKey(currentLocation.ID))
                Globals.Locations.Add(currentLocation.ID, currentLocation);

            foreach (Location location in Globals.Locations.Values)
                location.SetEvent();

            await Game.InitHeroInventory();
            await Game.InitHeroJournal();
            Console.Clear();

            await Program.Game!.SetCurrentLocation(Globals.Locations[currentLocation.ID]);
        }

        public static async Task UpdateSave()
        {
            if (!Directory.Exists("Data\\Saves"))
                Directory.CreateDirectory("Data\\Saves");

            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\Saves\\Save{CurrentSaveNr}.dat");

            using StreamWriter saveFile = new(path);
            SaveData save = new()
            {
                Timestamp = Logger.GetFormattedTimestamp(),
                Player = Globals.Player,
                Npcs = Globals.Npcs,
                Locations = await Globals.LocationsToJson(),
                Fractions = Globals.Fractions,
                Quests = Globals.Quests,
                Chapter = Globals.Chapter,
                CurrentLocation = Program.Game!.CurrentLocation!.ToJson(),
                Weather = Program.Game!.Weather,
                StoryGlobals = Program.Game!.StoryGlobals
            };

            var serializedObject = JsonConvert.SerializeObject(save, Formatting.Indented);
            await saveFile.WriteAsync(serializedObject);
        }

        public static string PrintSex(uint sex)
        {
            if (sex == Convert.ToInt32(Genders.Male))
            {
                return $"{Globals.JsonReader!["SEX.MALE"]}";
            }
            if (sex == Convert.ToInt32(Genders.Female))
            {
                return $"{Globals.JsonReader!["SEX.FEMALE"]}";
            }
            return $"{Globals.JsonReader!["SEX.UNDEFINED"]}";
        }

        private static string PrintName(string name)
        {
            return name == "" ? Globals.JsonReader!["UNKNOWN"] : name;
        }

        private static string GetChapterToString(uint chapter)
        {
            if (chapter == 0 || chapter < 0)
            {
                return $"{Globals.JsonReader!["PROLOGUE"]}";
            }
            if (chapter == 1 || chapter == 2 || chapter == 3)
            {
                return $"{Globals.JsonReader!["CHAPTER"]} {chapter}";
            }
            return $"{Globals.JsonReader!["EPILOGUE"]}";
        }

        private static string GetLocationName(Location location)
        {
            if (location.ID == "DarkAlley")
            {
                return $": {Globals.JsonReader!["LOCATION.DARK_ALLEY"]}";
            }
            if (location.ID == "Street")
            {
                return $": {Globals.JsonReader!["LOCATION.STREET"]}";
            }
            if (location.ID == "GunShop")
            {
                return $": {Globals.JsonReader!["LOCATION.GUN_SHOP"]}";
            }
            if (location.ID == "NightclubEden")
            {
                return $": {Globals.JsonReader!["LOCATION.NIGHTCLUB_EDEN"]}";
            }
            return "";
        }

        private static async ValueTask<string> LoadSaveInfo(string saveToLoad)
        {
            if (!File.Exists(saveToLoad))
                return string.Empty;

            string content = await File.ReadAllTextAsync(saveToLoad);
            var saveInfo = JsonConvert.DeserializeObject<SaveData>(content);

            Location currentLocation;

            if (saveInfo.CurrentLocation != null)
                currentLocation = await saveInfo.CurrentLocation.ToObject<Location>();
            else
            {
                Dictionary<string, Location> locations = saveInfo.Locations.ToObject<Dictionary<string, Location>>();
                currentLocation = locations["DarkAlley"];
            }

            return $"{PrintName(saveInfo.Player.Name)}, {PrintSex((uint)saveInfo.Player.Sex).ToLower()} | {GetChapterToString(saveInfo.Chapter)}{GetLocationName(currentLocation)} | {saveInfo.Timestamp}";
        }

        public static async Task SearchForSaves()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Data\\Saves");

            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path, "Save*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".dat"));

                if (files.Any())
                {
                    Menu savesMenu = new();
                    savesMenu.ClearOptions();
                    Dictionary<string, Func<Task>> options = new();
                    uint i = 0;

                    foreach (string file in files)
                    {
                        uint currentIndex = i;
                        options.Add(await LoadSaveInfo(file), async () => await LoadSave(currentIndex));
                        i++;
                    }

                    i = 0;
                    options.Add($"{Display.GetJsonString("BACK_TO_MAIN_MENU")}", BackToMainMenu);
                    savesMenu.AddOptions(options);
                    await savesMenu.ShowOptions(0);
                    await savesMenu.InputChoice();
                    return;
                }
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\n\n\t{Display.GetJsonString("LOAD_GAME.NO_SAVES_FOUND")}");
            await Task.Delay(2000);
            Console.ResetColor();
            Console.Clear();
            await Program.Game!.LoadLogo();
            await Program.Game!.MainMenu();
        }

        public static async Task BackToMainMenu()
        {
            Console.ResetColor();
            Console.Clear();
            await Program.Game!.LoadLogo();
            await Program.Game!.MainMenu();
        }
    }
}
