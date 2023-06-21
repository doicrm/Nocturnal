using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Events.Prologue;

public static class PrologueEvents
{
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

        if (Globals.Locations.ContainsKey("DarkAlley"))
        {
            Program.Game!.SetCurrentLocation(Globals.Locations["DarkAlley"]);
        }
    }

    public static void DarkAlley()
    {
        Globals.Chapter = 0;
        SaveManager.UpdateSave();

        if (!Globals.Locations["DarkAlley"].IsVisited)
        {
            DarkAlleyEvents.WakeUp();
            return;
        }

        DarkAlleyEvents.Crossroads();
    }

    public static void Street()
    {
        if (Program.Game!.Weather != Weather.Rainy)
        {
            Random rnd = new(); int rand = rnd.Next(0, 10);
            if (rand > 5 && rand <= 10)
                RandomEvents.StartRaining();
        }

        SaveManager.UpdateSave();

        if (!Globals.Locations["Street"].IsVisited)
        {
            StreetEvents.LookAtEden();
            return;
        }

        StreetEvents.Crossroads();
    }

    public static void GunShop()
    {
        SaveManager.UpdateSave();

        if (!Globals.Locations["GunShop"].IsVisited)
        {
            Globals.Locations["GunShop"].IsVisited = true;
            GunShopEvents.EnterGunShop();
            return;
        }

        GunShopEvents.Crossroads();
    }

    public static void NightclubEden()
    {
        SaveManager.UpdateSave();

        if (!Globals.Locations["NightclubEden"].IsVisited)
        {
            NightclubEdenEvents.EnterClub();
            return;
        }

        NightclubEdenEvents.Crossroads();
    }

    public static void VisitDarkAlley()
    {
        Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
    }

    public static void VisitStreet()
    {
        Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
    }

    public static void VisitNightclubEden()
    {
        Display.WriteNarration($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.VISIT_CLUB"]}\n\n");
        Program.Game!.SetCurrentLocation(Globals.Locations["NightclubEden"]);
    }

    public static void VisitGunShop()
    {
        Program.Game!.SetCurrentLocation(Globals.Locations["GunShop"]);
    }
}
