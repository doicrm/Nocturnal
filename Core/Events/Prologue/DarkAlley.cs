using Nocturnal.Core.Entitites;
using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Events.Prologue;

public static class DarkAlleyEvents
{
    // ************************************************************
    // 		DARK ALLEY in 'Eden' nigthclub area
    // ************************************************************

    public static void Prologue()
    {
        Display.Write($"\n\t{Globals.JsonReader!["PROLOGUE"]}");
        Thread.Sleep(2000);
        Display.Write($"\n\n\t{Globals.JsonReader!["PARADISE_LOST"]}");
        Thread.Sleep(5000);
        Console.Clear();
        StoryIntroduction();
    }

    public static void StoryIntroduction()
    {
        Display.Write($"\n\t{Globals.JsonReader!["INTRO_01"]}");
        Thread.Sleep(1000);
        Display.Write($" {Globals.JsonReader!["INTRO_02"]}\n\n", 20);
        Game.Pause();
        Console.Clear();
        Console.WriteLine();
        Thread.Sleep(2500);
        Display.WriteNarration($"\t{Globals.JsonReader!["INTRO_03"]}", 75);
        Thread.Sleep(2500);
        Display.WriteNarration($"\n\t{Globals.JsonReader!["INTRO_04"]}", 75);
        Thread.Sleep(2500);
        Display.WriteNarration($"\n\t{Globals.JsonReader!["INTRO_05"]}", 75);
        Thread.Sleep(3000);
        Console.Clear();
        WakeUp();
    }

    public static void WakeUp()
    {
        Thread.Sleep(2000);
        Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_01"]}");
        Thread.Sleep(1000);
        Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.WAKE_UP_02"]}");
        Thread.Sleep(1500);
        Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_03"]}");
        Thread.Sleep(1000);
        Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.WAKE_UP_04"]}");
        Thread.Sleep(3000);
        Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_05"]}");
        Thread.Sleep(500);
        Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.WAKE_UP_06"]}");
        Thread.Sleep(3000);
        Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_07"]}");
        Thread.Sleep(1000);
        Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.WAKE_UP_08"]}");
        Thread.Sleep(2500);
        Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_09"]}\n\n");

        Menu wakeUpMenu = new(new Dictionary<string, Action>()
        {
            { $"{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_MENU.LOOK_OUT"]}", SearchRubbish },
            { $"{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_MENU.FIND_EXIT"]}", OutOfAlley }
        });
    }

    public static void SearchRubbish()
    {
        Console.Write("DEBUG: SearchRubbish");
    }

    public static void OutOfAlley()
    {
        Console.Write("DEBUG: OutOfAlley");
    }

    public static void AcceleratorFinding()
    {
        Console.Write("DEBUG: AcceleratorFinding");
    }

    public static void DarkAlleyCrossroads()
    {
        Console.Write("DEBUG: DarkAlleyCrossroads");
    }

    public static void DarkAlleyCrossroads_1()
    {
        AcceleratorFinding();
        Display.WriteNarration($"{Globals.JsonReader!["DARK_ALLEY.CROSSROADS_04"]}");
        Thread.Sleep(1500);
        Console.Clear();
        Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
    }

    public static void DarkAlleyCrossroads_2()
    {
        Console.Clear();
        Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
    }
}
