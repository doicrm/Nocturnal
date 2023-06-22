using Newtonsoft.Json;
using Nocturnal.Core.Entitites;
using Nocturnal.Core.Entitites.Characters;
using Nocturnal.Core.Entitites.Items;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.System;

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

    public static void CreateSave()
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
                    Locations = Globals.LocationsToJson(),
                    Fractions = Globals.Fractions,
                    Quests = Globals.Quests,
                    Chapter = Globals.Chapter,
                    CurrentLocation = null!,
                    Weather = Program.Game!.Weather,
                    StoryGlobals = Program.Game!.StoryGlobals
                };

                var serializedObject = JsonConvert.SerializeObject(save, Formatting.Indented);
                newSave.Write(serializedObject);
                newSave.Close();
                break;
            }
        }
    }

    public static void LoadSave(uint nr)
    {
        string path = $"{Directory.GetCurrentDirectory()}\\Data\\Saves\\Save{nr}.dat";

        if (!File.Exists(path))
        {
            Program.Game!.LoadLogo();
            return;
        }

        CurrentSaveNr = nr;

        using StreamReader saveFile = File.OpenText(path);
        string? content = null;
        string? s = null;

        while ((s = saveFile.ReadLine()!) != null)
        {
            content += s;
        }

        saveFile.Close();
        var saveInfo = JsonConvert.DeserializeObject<SaveData>(content!);

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
            currentLocation = saveInfo.CurrentLocation.ToObject<Location>();
        else
            currentLocation = locations["DarkAlley"];

        if (!Globals.Locations.ContainsKey(currentLocation.ID))
            Globals.Locations.Add(currentLocation.ID, currentLocation);

        foreach (Location location in Globals.Locations.Values)
            location.SetEvent();

        Item.InsertInstances();
        Console.Clear();

        Program.Game!.SetCurrentLocation(Globals.Locations[currentLocation.ID]);
    }

    public static void UpdateSave()
    {
        if (!Directory.Exists("Data\\Saves"))
            Directory.CreateDirectory("Data\\Saves");

        string path = $"{Directory.GetCurrentDirectory()}\\Data\\Saves\\Save{CurrentSaveNr}.dat";

        using StreamWriter saveFile = File.CreateText(path);
        SaveData save = new()
        {
            Timestamp = Logger.GetFormattedTimestamp(),
            Player = Globals.Player,
            Npcs = Globals.Npcs,
            Locations = Globals.LocationsToJson(),
            Fractions = Globals.Fractions,
            Quests = Globals.Quests,
            Chapter = Globals.Chapter,
            CurrentLocation = Program.Game!.CurrentLocation!.ToJson(),
            Weather = Program.Game!.Weather,
            StoryGlobals = Program.Game!.StoryGlobals
        };

        var serializedObject = JsonConvert.SerializeObject(save, Formatting.Indented);
        saveFile.Write(serializedObject);
        saveFile.Close();
    }

    public static string PrintSex(uint sex)
    {
        if (sex == Convert.ToInt32(Genders.Male))
        {
            return $"{Globals.JsonReader!["SEX.MALE"]}";
        }
        else if (sex == Convert.ToInt32(Genders.Female))
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
        else if (chapter == 1 || chapter == 2 || chapter == 3)
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
        else if (location.ID == "Street")
        {
            return $": {Globals.JsonReader!["LOCATION.STREET"]}";
        }
        else if (location.ID == "GunShop")
        {
            return $": {Globals.JsonReader!["LOCATION.GUN_SHOP"]}";
        }
        else if (location.ID == "NightclubEden")
        {
            return $": {Globals.JsonReader!["LOCATION.NIGHTCLUB_EDEN"]}";
        }
        return "";
    }

    private static string LoadSaveInfo(string saveToLoad)
    {
        if (!File.Exists(saveToLoad))
        {
            Console.WriteLine($"{saveToLoad} - nie ma takiego pliku!");
            return "";
        }

        using StreamReader oldSave = File.OpenText(saveToLoad);
        string? content = null;
        string? s = null;

        while ((s = oldSave.ReadLine()!) != null)
        {
            content += s;
        }

        oldSave.Close();
        var saveInfo = JsonConvert.DeserializeObject<SaveData>(content!);

        Location currentLocation;

        if (saveInfo.CurrentLocation != null)
            currentLocation = saveInfo.CurrentLocation.ToObject<Location>();
        else
        {
            Dictionary<string, Location> locations = saveInfo.Locations.ToObject<Dictionary<string, Location>>();
            currentLocation = locations["DarkAlley"];
        }

        return $"{PrintName(saveInfo.Player.Name)}, {PrintSex((uint)saveInfo.Player.Sex).ToLower()} | {GetChapterToString(saveInfo.Chapter)}{GetLocationName(currentLocation)} | {saveInfo.Timestamp}";
    }

    public static void SearchForSaves()
    {
        string path = $"{Directory.GetCurrentDirectory()}\\Data\\Saves";

        if (Directory.Exists("Data\\Saves"))
        {
            var files = Directory.GetFiles(path, "Save*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".dat"));

            if (files.Any())
            {
                Menu savesMenu = new();
                savesMenu.ClearOptions();
                Dictionary<string, Action> options = new();
                uint i = 0;

                foreach (dynamic file in files)
                {
                    uint currentIndex = i;
                    void action() => LoadSave(currentIndex);
                    options.Add(LoadSaveInfo(file), (Action)action);
                    i++;
                }

                i = 0;
                options.Add($"{Globals.JsonReader!["BACK_TO_MAIN_MENU"]}", BackToMainMenu);
                savesMenu.AddOptions(options);
                savesMenu.ShowOptions(0);
                savesMenu.InputChoice();
                return;
            }
        }

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"\t{Globals.JsonReader!["LOAD_GAME.NO_SAVES_FOUND"]}");
        Thread.Sleep(2000);
        Console.ResetColor();
        Console.Clear();
        Program.Game!.LoadLogo();
        Program.Game!.MainMenu();
    }

    public static void BackToMainMenu()
    {
        Console.ResetColor();
        Console.Clear();
        Program.Game!.LoadLogo();
        Program.Game!.MainMenu();
    }
}
