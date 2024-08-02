using Newtonsoft.Json;
using Nocturnal.src.core;
using Nocturnal.src.core.utils;
using Nocturnal.src.entitites;
using Nocturnal.src.ui;

namespace Nocturnal.src.services
{
    public struct SaveData(string timestamp, Player player, dynamic npcs, dynamic locations, dynamic fractions, dynamic quests, uint chapter, dynamic currentLocation, Weather weather, dynamic storyGlobals)
    {
        public string Timestamp = timestamp;
        public Player Player = player;
        public dynamic Npcs = npcs;
        public dynamic Locations = locations;
        public dynamic Fractions = fractions;
        public dynamic Quests = quests;
        public uint Chapter = chapter;
        public dynamic CurrentLocation = currentLocation;
        public Weather Weather = weather;
        public dynamic StoryGlobals = storyGlobals;
    };

    public class SaveService
    {
        private static readonly Dictionary<uint, string> genderMap = new()
        {
            { (uint)Genders.Male, "SEX.MALE" },
            { (uint)Genders.Female, "SEX.FEMALE" }
        };

        private static readonly Dictionary<string, string> locationNames = new()
        {
            { "DarkAlley", "LOCATION.DARK_ALLEY" },
            { "Street", "LOCATION.STREET" },
            { "GunShop", "LOCATION.GUN_SHOP" },
            { "NightclubEden", "LOCATION.NIGHTCLUB_EDEN" }
        };


        private static uint CurrentSaveNr = 0;

        public static async Task CreateSave()
        {
            string saveDirectory = Path.Combine(Directory.GetCurrentDirectory(), "data", "saves");

            if (!Directory.Exists(saveDirectory))
                Directory.CreateDirectory(saveDirectory);

            for (int i = 0; i < Constants.MAX_SAVES; i++)
            {
                string path = Path.Combine(saveDirectory, $"Save{i}.dat");

                if (!File.Exists(path))
                {
                    try
                    {
                        using var newSave = File.CreateText(path);
                        CurrentSaveNr = (uint)i;

                        var save = new SaveData(
                            TimeFormatter.GetFormattedTimestamp(),
                            Globals.Player,
                            Globals.Npcs,
                            await JsonService.LocationsToJson(),
                            Globals.Fractions,
                            Globals.Quests,
                            Globals.Chapter,
                            null!,
                            Game.Instance.Weather,
                            Game.Instance.StoryGlobals
                        );

                        var serializedObject = JsonConvert.SerializeObject(save, Formatting.Indented);
                        await newSave.WriteAsync(serializedObject);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error creating save file: {ex.Message}");
                    }
                }
            }
        }

        public static async Task LoadSave(uint nr)
        {
            Item.InsertInstances();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "data", "saves", $"Save{nr}.dat");

            if (!File.Exists(path))
            {
                await Display.LoadLogo();
                return;
            }

            SaveData saveInfo;
            try
            {
                string content = await File.ReadAllTextAsync(path);
                saveInfo = JsonConvert.DeserializeObject<SaveData>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load save file: {ex.Message}");
                return;
            }

            CurrentSaveNr = nr;

            Globals.UpdateGlobalsFromSave(saveInfo);

            Location currentLocation = DetermineCurrentLocation(saveInfo);

            await GameDataService.InitHeroInventory();
            await GameDataService.InitHeroJournal();
            Console.Clear();

            await Game.Instance.SetCurrentLocation(Globals.Locations[currentLocation.ID]);
        }

        private static Location DetermineCurrentLocation(SaveData saveInfo)
        {
            Location? currentLocation = saveInfo.CurrentLocation?.ToObject<Location>();

            if (currentLocation == null || !Globals.Locations.ContainsKey(currentLocation.ID))
            {
                if (!Globals.Locations.TryGetValue("DarkAlley", out currentLocation))
                {
                    throw new InvalidOperationException("Default location 'DarkAlley' not found in Globals.Locations.");
                }
            }

            if (!Globals.Locations.ContainsKey(currentLocation.ID))
            {
                Globals.Locations.Add(currentLocation.ID, currentLocation);
            }

            foreach (var location in Globals.Locations.Values)
            {
                location.SetEvent();
            }

            return currentLocation;
        }

        public static async Task UpdateSave()
        {
            try
            {
                string saveDirectory = Path.Combine(Directory.GetCurrentDirectory(), "data", "saves");

                if (!Directory.Exists(saveDirectory))
                    Directory.CreateDirectory(saveDirectory);

                string path = Path.Combine(saveDirectory, $"Save{CurrentSaveNr}.dat");

                var save = new SaveData(
                    TimeFormatter.GetFormattedTimestamp(),
                    Globals.Player,
                    Globals.Npcs,
                    await JsonService.LocationsToJson(),
                    Globals.Fractions,
                    Globals.Quests,
                    Globals.Chapter,
                    Game.Instance.CurrentLocation!.ToJson(),
                    Game.Instance.Weather,
                    Game.Instance.StoryGlobals
                );

                var serializedObject = JsonConvert.SerializeObject(save, Formatting.Indented);
                await File.WriteAllTextAsync(path, serializedObject);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during a save update: {ex.Message}");
            }
        }

        private static async ValueTask<string> LoadSaveInfo(string saveToLoad)
        {
            if (!File.Exists(saveToLoad))
                return string.Empty;

            try
            {
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

                return $"{GetName(saveInfo.Player.Name)}, {GetSex((uint)saveInfo.Player.Sex).ToLower()} | {GetChapterToString(saveInfo.Chapter)}{GetLocationName(currentLocation)} | {saveInfo.Timestamp}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during loading of save info: {ex.Message}");
                return string.Empty;
            }
        }

        public static async Task FindSaves()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "data", "saves");

            if (!Directory.Exists(path))
            {
                await NoSavesFound();
                return;
            }

            var files = Directory.EnumerateFiles(path, "Save*.dat", SearchOption.TopDirectoryOnly);

            if (!files.Any())
            {
                await NoSavesFound();
                return;
            }

            _ = new InteractiveMenu(await CreateSaveOptions(files));
        }

        private static async Task<MenuOptions> CreateSaveOptions(IEnumerable<string> files)
        {
            var options = new MenuOptions();
            uint i = 0;

            foreach (var file in files)
            {
                uint currentIndex = i;
                var saveInfo = await LoadSaveInfo(file);
                options.Add(saveInfo, async () => await LoadSave(currentIndex));
                i++;
            }

            options.Add(Display.GetJsonString("BACK_TO_MAIN_MENU"), Display.LoadLogo);
            return options;
        }

        private static async Task NoSavesFound()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\n\n\t{Display.GetJsonString("LOAD_GAME.NO_SAVES_FOUND")}");
            await Task.Delay(2000);
            await Display.LoadLogo();
        }

        public static string GetSex(uint sex)
        {
            if (genderMap.TryGetValue(sex, out var genderKey))
                return Display.GetJsonString(genderKey);
            return Display.GetJsonString("SEX.UNDEFINED");
        }

        private static string GetName(string name)
        {
            return name != "" ? name : Display.GetJsonString("UNKNOWN");
        }

        private static string GetChapterToString(uint chapter)
        {
            if (chapter == 0)
                return Display.GetJsonString("PROLOGUE");

            if (chapter >= 1 && chapter <= 3)
                return $"{Display.GetJsonString("CHAPTER")} {chapter}";

            return Display.GetJsonString("EPILOGUE");
        }

        private static string GetLocationName(Location location)
        {
            if (locationNames.TryGetValue(location.ID, out var locationKey))
                return $": {Display.GetJsonString(locationKey)}";
            return string.Empty;
        }
    }
}
