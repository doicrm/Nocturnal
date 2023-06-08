using Nocturnal.Core.Entitites;
using Nocturnal.Core.Events.Prologue;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.System;

public enum Weather { Sunny, Cloudy, Stormy, Rainy, Snowfall }

public class Game
{
    public static readonly Game Instance = new();
    public bool IsPlaying { get; set; }
    public Location? CurrentLocation { get; set; } = null;
    public Weather Weather { get; set; }
    public GameSettings Settings { get; set; } = new GameSettings();

    public Game()
    {
        IsPlaying = true;
        ChangeConsoleName();
    }

    public void Run()
    {
        while (IsPlaying)
        {
            Welcome();
            //WriteLogo(); // Disable for testing use
            LoadLogo();
            MainMenu();
            End();
        }
    }

    public static void ChangeConsoleName()
        => Console.Title = $"{Constants.GAME_NAME} {Constants.GAME_VERSION}"; 

    public static void Pause()
    {
        Console.Write($"\t{Globals.JsonReader!["PRESS_ANY_KEY"]}");
        Console.ReadKey();
    }

    public static void Welcome()
    {
        Console.Clear();
        Thread.Sleep(500);
        Display.Write($"\n\t{Globals.JsonReader!["AUTHOR_PRESENTS"]}", 40);
        Thread.Sleep(2000);
        Console.Clear();
    }

    public static void WriteLogo()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine();
        foreach (var s in Constants.GAME_LOGO) Display.Write(s, 1);
        Console.ResetColor();
        Console.WriteLine();
    }

    public void MainMenu()
    {
        Console.ResetColor();
        Console.WriteLine();
        Menu mainMenu = new(new Dictionary<string, Action>()
        {
            { $"{Globals.JsonReader!["MAIN_MENU.NEW_GAME"]}", NewGame },
            { $"{Globals.JsonReader!["MAIN_MENU.LOAD_GAME"]}", LoadGame },
            { $"{Globals.JsonReader!["MAIN_MENU.CHANGE_LANG"]}", ChangeLanguage },
            { $"{Globals.JsonReader!["MAIN_MENU.QUIT_GAME"]}", EndGame }
        });
    }

    public void NewGame()
    {
        InitAll();
        SaveManager.CreateSave();
        Console.Clear();

        if (Globals.Locations.ContainsKey("DarkAlley"))
        {
            SetCurrentLocation(Globals.Locations["DarkAlley"]);
        }
    }

    public void LoadGame()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"\n\t{Globals.JsonReader!["FEATURE_UNAVAILABLE"]}\n\n");
        Console.ResetColor();
        SaveManager.SearchForSaves();
        Thread.Sleep(1000);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write($"\n\n\t{Globals.JsonReader!["BACK_TO_MENU"]}");
        Console.ReadKey();
        Console.ResetColor();
        Console.Clear();
        LoadLogo();
        MainMenu();
    }

    public void ChangeLanguage()
    {
        GameSettings.CreateConfigFile();
        GameSettings.LoadDataFromFile(GameSettings.Lang);
        LoadLogo();
    }

    public void LoadLogo()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine();
        foreach (var s in Constants.GAME_LOGO) Console.Write(s);
        Console.WriteLine();
        Console.ResetColor();
        MainMenu();
    }

    public void EndGame()
    {
        Console.Clear();
        Display.Write($"\n\t{Globals.JsonReader!["QUIT_GAME"]}", 25);
        Menu quitMenu = new(new Dictionary<string, Action>()
        {
            { $"{Globals.JsonReader!["YES"]}", End },
            { $"{Globals.JsonReader!["NO"]}", LoadLogo }
        });
    }

    public void End()
    {
        IsPlaying = false;
        Console.Clear();
        Environment.Exit(1);
    }

    public void SetCurrentLocation(Location location)
    {
        CurrentLocation = location;
        CurrentLocation.Events.Invoke();
    }

    public void SetWeather(Weather weather) { Weather = weather; }

    public static void InitHeroIventory()
    {
        string path = $"{Directory.GetCurrentDirectory()}\\inventory.txt";
        using StreamWriter output = new(path);
        output.WriteLine($"{Globals.JsonReader!["INVENTORY.NO_ITEMS"]}");
        output.Close();
    }

    public static void InitHeroJournal()
    {
        string path = $"{Directory.GetCurrentDirectory()}\\journal.txt";
        using StreamWriter output = new(path);
        output.WriteLine($"{Globals.JsonReader!["JOURNAL.NO_QUESTS"]}");
        output.Close();
    }

    public static void InitLocations()
    {
        Location DarkAlley = new("Dark alley", null!, PrologueEvents.DarkAlley);
        Location Street = new("Street", null!, PrologueEvents.Street);

        Globals.Locations.Add("DarkAlley", DarkAlley);
        Globals.Locations.Add("Street", Street);
    }

    public static void InitAll()
    {
        InitHeroIventory();
        InitHeroJournal();
        InitLocations();
    }
}
