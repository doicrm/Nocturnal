using Newtonsoft.Json;
using Nocturnal.core;
using Nocturnal.core.utils;
using Nocturnal.entitites;
using Nocturnal.interfaces;
using Nocturnal.ui;

namespace Nocturnal.services;

public struct SaveData(string timestamp, Player player, dynamic npcs, dynamic locations, dynamic fractions, dynamic quests, uint chapter, dynamic currentLocation, Weather weather, dynamic storyGlobals)
{
    public readonly string Timestamp = timestamp;
    public readonly Player Player = player;
    public readonly dynamic Npcs = npcs;
    public readonly dynamic Locations = locations;
    public readonly dynamic Fractions = fractions;
    public readonly dynamic Quests = quests;
    public readonly uint Chapter = chapter;
    public readonly dynamic CurrentLocation = currentLocation;
    public readonly Weather Weather = weather;
    public readonly dynamic StoryGlobals = storyGlobals;
};

public abstract class SaveService : ISaveCreator, ISaveLoader, ISaveUpdater, ISaveFinder
{
    private static readonly Dictionary<Genders, string> GenderMap = new()
    {
        { Genders.Male, "SEX.MALE" },
        { Genders.Female, "SEX.FEMALE" }
    };
    
    private static uint _currentSaveNr = 0;

    private static string GetSaveDirectory() {
        return Path.Combine(Directory.GetCurrentDirectory(), "data", "saves");
    }

    public static async Task CreateSave()
    {
        var saveDirectory = GetSaveDirectory();

        if (!Directory.Exists(saveDirectory))
            Directory.CreateDirectory(saveDirectory);

        for (var i = 0; i < Constants.MaxSaves; i++)
        {
            var path = Path.Combine(saveDirectory, $"Save{i}.dat");

            if (File.Exists(path)) continue;
            try
            {
                await using var newSave = File.CreateText(path);
                _currentSaveNr = (uint)i;

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

    public static async Task LoadSave(uint nr)
    {
        Item.InsertInstances();

        var path = Path.Combine(GetSaveDirectory(), $"Save{nr}.dat");

        if (!File.Exists(path))
        {
            await Display.LoadLogo();
            return;
        }

        try
        {
            var content = await File.ReadAllTextAsync(path);
            var saveInfo = JsonConvert.DeserializeObject<SaveData>(content)!;
            _currentSaveNr = nr;

            Globals.UpdateGlobalsFromSave(saveInfo);
            var currentLocation = DetermineCurrentLocation(saveInfo);

            await GameDataService.InitHeroInventory();
            await GameDataService.InitHeroJournal();
            Console.Clear();

            await Game.Instance.SetCurrentLocation(Globals.Locations[currentLocation.Id]);
        }
        catch (Exception ex) {
            Console.WriteLine($"Failed to load save file: {ex.Message}");
        }
    }

    private static Location DetermineCurrentLocation(SaveData saveInfo)
    {
        Location? currentLocation = saveInfo.CurrentLocation.ToObject<Location>();

        if (currentLocation == null || !Globals.Locations.ContainsKey(currentLocation.Id))
        {
            if (!Globals.Locations.TryGetValue("DarkAlley", out currentLocation)) {
                throw new InvalidOperationException("Default location 'DarkAlley' not found in Globals.Locations.");
            }
        }

        Globals.Locations.TryAdd(currentLocation.Id, currentLocation);
        foreach (var location in Globals.Locations.Values) {
            location.SetEvent();
        }

        return currentLocation;
    }

    public static async Task UpdateSave()
    {
        try
        {
            var path = Path.Combine(GetSaveDirectory(), $"Save{_currentSaveNr}.dat");

            if (!Directory.Exists(GetSaveDirectory()))
                Directory.CreateDirectory(GetSaveDirectory());

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
        catch (Exception ex) {
            Console.WriteLine($"An error occurred during a save update: {ex.Message}");
        }
    }

    private static async ValueTask<string> LoadSaveInfo(string saveToLoad)
    {
        if (!File.Exists(saveToLoad))
            return string.Empty;

        try
        {
            var content = await File.ReadAllTextAsync(saveToLoad).ConfigureAwait(false);
            var saveInfo = JsonConvert.DeserializeObject<SaveData>(content);

            Location currentLocation = saveInfo.CurrentLocation != null
                ? await saveInfo.CurrentLocation.ToObject<Location>()
                : saveInfo.Locations.ToObject<Dictionary<string, Location>>()["DarkAlley"];

            return $"{SaveInfoPrinter.GetName(saveInfo.Player.Name)}, " +
                   $"{GetSex(saveInfo.Player.Sex).ToLower()} | " +
                   $"{SaveInfoPrinter.GetChapterToString(saveInfo.Chapter)}" +
                   $"{SaveInfoPrinter.GetLocationName(currentLocation)} | " +
                   $"{saveInfo.Timestamp}";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during loading of save info: {ex.Message}");
            return string.Empty;
        }
    }

    public static async Task FindSaves()
    {
        var path = GetSaveDirectory();

        if (!Directory.Exists(path))
        {
            await NoSavesFound();
            return;
        }

        var files = Directory.EnumerateFiles(path, "Save*.dat", SearchOption.TopDirectoryOnly);

        var enumerable = files.ToList();
        if (enumerable.Count == 0)
        {
            await NoSavesFound();
            return;
        }

        _ = new InteractiveMenu(await CreateSaveOptions(enumerable));
    }

    private static async Task<MenuOptions> CreateSaveOptions(IEnumerable<string> files)
    {
        var options = new MenuOptions();
        uint i = 0;

        foreach (var file in files)
        {
            var currentIndex = i;
            var saveInfo = await LoadSaveInfo(file);
            options.Add(saveInfo, async () => await LoadSave(currentIndex));
            i++;
        }

        options.Add(Localizator.GetString("BACK_TO_MAIN_MENU"), Display.LoadLogo);
        return options;
    }

    private static async Task NoSavesFound()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"\n\n\t{Localizator.GetString("LOAD_GAME.NO_SAVES_FOUND")}");
        await Task.Delay(2000);
        await Display.LoadLogo();
    }

    private static string GetSex(Genders sex)
    {
        return GenderMap.TryGetValue(sex, out var genderKey)
            ? Localizator.GetString(genderKey)
            : Localizator.GetString("SEX.UNDEFINED");
    }
}