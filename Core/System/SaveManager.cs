using Nocturnal.Core.Entitites.Living;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.System;

public struct SaveData
{
    public string Date;
    public string Hour;
    public string Player;
    public int Chapter;
    public int Gender;
    public int Stage;
};

public class SaveManager
{
    private static uint SaveNr = 0;

    public static void CreateSave()
    {
        if (!Directory.Exists("data\\saves"))
            Directory.CreateDirectory("data\\saves");

        string path = $"{Directory.GetCurrentDirectory()}\\data\\saves\\save_{SaveNr}.dat";

        if (!File.Exists(path))
        {
            using StreamWriter newSave = File.CreateText(path);
            SaveNr++;
            newSave.WriteLine(Logger.GetFormattedUtcTimestamp());
            newSave.WriteLine($"Player::Unknown");
            newSave.WriteLine($"Sex::Undefined");
            newSave.WriteLine($"{0}::{1}");
            newSave.Close();
        }
    }

    public static void LoadSave(uint nr)
    {
        //SaveData save;
        string path = $"{Directory.GetCurrentDirectory()}\\data\\saves\\save_{nr}.dat";

        if (!File.Exists(path))
        {
            Program.Game!.LoadLogo();
            return;
        }

        using StreamReader oldSave = File.OpenText(path);
        string s;
        while ((s = oldSave.ReadLine()!) != null)
        {
            Console.WriteLine(s);
        }
        oldSave.Close();

        //Hero.heroes["Hero"].Name = save.player;
        //Hero.heroes["Hero"].Sex = save.gender;
    }

    //public static void UpdateSave(int saveNr, string player, int sex, int chapter, int stage)
    //{

    //}

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

    private static string GetChapterString(uint chapter)
    {
        if (chapter == 0 || chapter < 0)
        {
            return $"{Globals.JsonReader!["PROLOGUE"]}";
        }
        else if (chapter == 1 || chapter == 2)
        {
            return $"{Globals.JsonReader!["CHAPTER"]} {chapter}";
        }
        return $"{Globals.JsonReader!["EPILOGUE"]}";
    }

    private static void LoadSaveInfo(string saveToLoad)
    {
        //SaveData save;

        if (!File.Exists(saveToLoad))
        {
            Console.WriteLine($"{saveToLoad} - nie ma takiego pliku!");
            return;
        }

        using StreamReader oldSave = File.OpenText(saveToLoad);
        string s;
        while ((s = oldSave.ReadLine()!) != null)
        {
            Console.WriteLine($"\t{s}");
        }
        oldSave.Close();

        //Console.WriteLine($"\t{save.Player}, {PrintSex(save.Gender)} | {GetChapterString(save.Chapter)} : {save.Stage} | {save.Date} {save.Hour}");
    }

    public static void SearchForSaves()
    {
        string path = $"{Directory.GetCurrentDirectory()}\\data\\saves";
        var files = Directory.GetFiles(path, "save_*", SearchOption.AllDirectories)
        .Where(s => s.EndsWith(".dat"));

        if (files.Any())
        {
            foreach (dynamic file in files)
                LoadSaveInfo(file);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"{Globals.JsonReader!["NO_SAVES_FOUND"]}");
            Console.ResetColor();
        }
    }
}
