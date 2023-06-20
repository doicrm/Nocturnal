using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.System;

public enum GameLanguages { EN = 1, PL = 2 }

public struct ConfigFileData
{
    public string? Username { get; set; }
    public int Language { get; set; }
}

public class GameSettings
{
    public static int Lang { get; private set; }

    public GameSettings() { Lang = 0; }

    public static bool LoadConfigFile()
    {
        if (!File.Exists("config.json"))
            CreateConfigFile();
        else
        {
            string jsonString;
            ConfigFileData configFileData;

            try
            {
                jsonString = File.ReadAllText("config.json");
                configFileData = JsonConvert.DeserializeObject<ConfigFileData>(jsonString)!;
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                return false;
            }

            Lang = configFileData.Language;
        }

        return LoadDataFromFile(Lang);
    }

    public static void CreateConfigFile()
    {
        Lang = SelectLanguage();

        var configFileData = new ConfigFileData
        {
            Username = Environment.UserName,
            Language = Lang
        };

        string jsonString = JsonConvert.SerializeObject(configFileData, Formatting.Indented);
        File.WriteAllText("config.json", jsonString);
    }

    public static bool LoadDataFromFile(int lang)
    {
        try
        {
            string path = Directory.GetCurrentDirectory() + "\\Data\\Lang\\" + GetFileName(lang) + ".json";
            string jsonString = File.ReadAllText(path);
            Globals.JsonReader = JObject.Parse(jsonString);
            return true;
        }
        catch (Exception e)
        {
            Console.Clear();
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public static int SelectLanguage()
    {
        int choice;
        while (true)
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t[1] EN");
            Console.WriteLine("\t[2] PL");
            choice = Input.GetChoice();

            if (choice is (int)GameLanguages.EN
                or (int)GameLanguages.PL)
                break;
            else continue;
        }
        return choice;
    }

    private static string GetFileName(int lang)
    {
        return lang is (int)GameLanguages.EN ? "en" : "pl";
    }
}
