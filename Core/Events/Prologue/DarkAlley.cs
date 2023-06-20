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
        Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_09"]}\n");

        Menu wakeUpMenu = new(new Dictionary<string, Action>()
        {
            { $"{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_MENU.LOOK_OUT"]}", SearchGarbage },
            { $"{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_MENU.FIND_EXIT"]}", OutOfAlley }
        });
    }

    public static void SearchGarbage()
    {
        AcceleratorFinding();
        Console.WriteLine();
        OutOfAlley();
    }

    public static void AcceleratorFinding()
    {
        Display.WriteNarration($"\t{Globals.JsonReader!["DARK_ALLEY.FINDING_ACCELERATOR_01"]}");
        Thread.Sleep(1500);
        Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.FINDING_ACCELERATOR_02"]}");
        Thread.Sleep(2000);
        Globals.Player.AddItem(Globals.Items["AD13"]);

        Display.Write($"\n\n\t{Globals.JsonReader!["ITEM_FOUND"]}");
        Console.ForegroundColor = ConsoleColor.Blue;
        Display.Write(Globals.Items["AD13"].Name!);
        Console.ResetColor();
        Display.Write($"{Globals.JsonReader!["AND"]}");
        Console.ForegroundColor = ConsoleColor.Green;
        Display.Write("5$");
        Console.ResetColor();

        Display.Write($"\n\t{Globals.JsonReader!["INVENTORY.TIP"]}", 15);
        Thread.Sleep(4000);
        Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.FINDING_ACCELERATOR_03"]}");
        Thread.Sleep(1000);
    }

    public static void OutOfAlley()
    {
        if (!Globals.Player.HasItem(Globals.Items["AD13"]))
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_01"]}");
            Thread.Sleep(1500);
            Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_02"]}");
            Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_03"]}");
        }
        else
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_04"]}");
        }

        Thread.Sleep(3000);
        Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_05"]}");
        Display.WriteDialogue($"\n\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_02"]}\n");

        Menu outOfAlleyMenu = new(new Dictionary<string, Action>()
        {
            { $"{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_MENU.FIND_OUT"]}", OutOfAlley_01 },
            { $"{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_MENU.IGNORE_STRANGER"]}", OutOfAlley_02 }
        });
    }

    public static void OutOfAlley_01()
    {
        Display.WriteNarration($"\t{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_06"]}");
        Thread.Sleep(1500);
        Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_07"]}\n");
        Thread.Sleep(1000);
        DialogueWithBob();
    }

    public static void OutOfAlley_02()
    {
        Display.WriteNarration($"\t{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_08"]}");
        Display.WriteDialogue($"{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_03"]}");
        Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_09"]}");
        Thread.Sleep(1000);
        Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
    }

    public static void DialogueWithBob()
    {
        Console.WriteLine("DEBUG: DialogueWithBob");
    }

    //public static void DialogueWithBob_01()
    //{
    //    Console.WriteLine();
    //    MiscEvents.NamingHero();
    //    Globals.Npcs["Bob"].IsKnowHero = true;
    //    Display.WriteDialogue((std::string)json["prologue"]["dialogueWithBob"][4]
    //        + Npc::npcs["Hero"].getName()
    //        + (std::string)json["prologue"]["dialogueWithBob"][5]);
    //    Thread.Sleep(1500);
    //    Display.WriteDialogue((std::string)json["prologue"]["dialogueWithBob"][6]
    //        + Npc::npcs["Bob"].getName() + ".");
    //    Thread.Sleep(1500);
    //    Display.WriteDialogue(json["prologue"]["dialogueWithBob"][7]);
    //    Display.WriteNarration(json["prologue"]["dialogueWithBob"][0]);
    //}

    //public static void DialogueWithBob_2()
    //{
    //    Console.WriteLine();
    //    Globals.Npcs["Bob"].SetAttitude(Attitudes.Angry);
    //    Thread.Sleep(500);
    //    Display.WriteDialogue(json["prologue"]["dialogueWithBob"][8]);
    //}

    //public static void DialogueWithBob_3()
    //{
    //    Program.Game!.StoryGlobals.Bob_RecommendsZed = true; // Bob recommends Zed's gun shop to the hero
    //    Display.WriteDialogue(json["prologue"]["dialogueWithBob"][9]);
    //    Thread.Sleep(1000);
    //    Display.WriteDialogue(json["prologue"]["dialogueWithBob"][10]);
    //    Thread.Sleep(1000);
    //    Display.WriteDialogue(json["prologue"]["dialogueWithBob"][11]);
    //}

    //public static void DialogueWithBob_4()
    //{
    //    Display.WriteDialogue(json["prologue"]["dialogueWithBob"][12]);
    //    Thread.Sleep(1000);
    //    Display.WriteDialogue(json["prologue"]["dialogueWithBob"][13]);
    //    Thread.Sleep(1500);
    //    Display.WriteDialogue(json["prologue"]["dialogueWithBob"][14]);
    //    Thread.Sleep(1500);
    //    Display.WriteDialogue(json["prologue"]["dialogueWithBob"][15]);
    //    Thread.Sleep(1000);
    //    Display.WriteDialogue(json["prologue"]["dialogueWithBob"][16]);
    //    AboutParadiseLost();
    //}

    //public static void DialogueWithBob_5()
    //{
    //    Console.WriteLine();
    //    Globals.Npcs["Bob"].SetAttitude(Attitudes.Angry);
    //    Thread.Sleep(500);
    //    Display.WriteDialogue(json["prologue"]["dialogueWithBob"][17]);
    //}

    //public static void AboutParadiseLost()
    //{
    //    Console.WriteLine();

    //    Menu paradiseLostMenu = new(new Dictionary<string, Action>()
    //    {
    //        { $"{Globals.JsonReader!["DARK_ALLEY.PARADISE_LOST_MENU.01"]}", AboutParadiseLost_01 },
    //        { $"{Globals.JsonReader!["DARK_ALLEY.PARADISE_LOST_MENU.02"]}", AboutParadiseLost_02 }
    //    });
    //}

    //public static void AboutParadiseLost_01()
    //{
    //    Display.WriteDialogue($"{Globals.JsonReader!["DARK_ALLEY.ABOUT_PARADISE_LOST_01"]}");
    //    Thread.Sleep(500);
    //    Display.WriteDialogue($"{Globals.JsonReader!["DARK_ALLEY.ABOUT_PARADISE_LOST_02"]}");
    //    Thread.Sleep(1000);
    //    Display.WriteDialogue($"{Globals.JsonReader!["DARK_ALLEY.ABOUT_PARADISE_LOST_03"]}");
    //    Thread.Sleep(1000);
    //    Display.WriteDialogue($"{Globals.JsonReader!["DARK_ALLEY.ABOUT_PARADISE_LOST_04"]}");
    //    Thread.Sleep(1000);
    //    Display.WriteDialogue($"{Globals.JsonReader!["DARK_ALLEY.ABOUT_PARADISE_LOST_05"]}");
    //    Thread.Sleep(1500);
    //    Display.WriteDialogue($"{Globals.JsonReader!["DARK_ALLEY.ABOUT_PARADISE_LOST_06"]}");
    //}

    //public static void AboutParadiseLost_02()
    //{
    //}

    public static void DarkAlleyCrossroads()
    {
        if (!Globals.Npcs["Bob"].IsKnowHero)
        {
            Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.CROSSROADS_01"]}");
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["DARK_ALLEY.CROSSROADS_02"]}\n");

            Menu outOfAlleyMenu = new(new Dictionary<string, Action>()
            {
                { $"{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_MENU.FIND_OUT_FINALLY"]}", OutOfAlley_01 },
                { $"{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_MENU.IGNORE_STRANGER"]}", OutOfAlley_02 }
            });
        }
        else
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["DARK_ALLEY.CROSSROADS_03"]}");
            Thread.Sleep(1000);
            Display.WriteNarration($"{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_08"]}");

            if (!Globals.Player.HasItem(Globals.Items["AD13"]))
            {
                Thread.Sleep(1500);
                Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.WAKE_UP_09"]}");

                Menu wakeUpMenu = new(new Dictionary<string, Action>()
                {
                    { $"{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_MENU.FIND_OUT_FINALLY"]}", DarkAlleyCrossroads_01 },
                    { $"{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_MENU.IGNORE_STRANGER"]}", DarkAlleyCrossroads_02 }
                });
            }
            else
            {
                Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.CROSSROADS_04"]}\n\n");
                Game.Pause();
                Console.Clear();
                Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
            }
        }
    }

    public static void DarkAlleyCrossroads_01()
    {
        AcceleratorFinding();
        Display.WriteNarration($"{Globals.JsonReader!["DARK_ALLEY.CROSSROADS_04"]}");
        Thread.Sleep(1500);
        Console.Clear();
        Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
    }

    public static void DarkAlleyCrossroads_02()
    {
        Console.Clear();
        Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
    }
}
