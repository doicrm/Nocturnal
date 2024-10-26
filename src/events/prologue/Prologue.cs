using Nocturnal.core;
using Nocturnal.entitites;
using Nocturnal.services;
using Nocturnal.ui;

namespace Nocturnal.events.prologue;

public static class PrologueEvents
{
    public static async Task Prologue()
    {
        Globals.Chapter = 0;
        await Display.Write($"\n\t{Localizator.GetString("PROLOGUE")}");
        await Task.Delay(2000);
        await Display.Write($"\n\n\t{Localizator.GetString("PARADISE_LOST")}");
        await Task.Delay(5000);
        Console.Clear();
        await Intro();
    }

    private static async Task Intro()
    {
        await Display.Write($"\n\t{Localizator.GetString("INTRO_01")}");
        await Task.Delay(1000);
        await Display.Write($" {Localizator.GetString("INTRO_02")}\n\n", 20);
        await Game.Pause();
        Console.Clear();
        Console.WriteLine();
        await Task.Delay(2500);
        await Display.WriteNarration($"{Localizator.GetString("INTRO_03")}\n", 75);
        await Task.Delay(2500);
        await Display.WriteNarration($"{Localizator.GetString("INTRO_04")}\n", 75);
        await Task.Delay(2500);
        await Display.WriteNarration($"{Localizator.GetString("INTRO_05")}", 75);
        await Task.Delay(3000);
        Console.Clear();

        if (Globals.Locations.TryGetValue("DarkAlley", out Location? value))
            await Game.Instance.SetCurrentLocation(value);
    }

    public static async Task DarkAlley()
    {
        await SaveService.UpdateSave();

        if (!Globals.Locations["DarkAlley"].IsVisited)
        {
            await DarkAlleyEvents.WakeUp();
            return;
        }

        await DarkAlleyEvents.Crossroads();
    }

    public static async Task Street()
    {
        if (Game.Instance.Weather != Weather.Rainy)
        {
            var rand = new Random().Next(0, 10);
            if (rand is > 5 and <= 10)
                await RandomEvents.StartRaining();
        }

        await SaveService.UpdateSave();

        if (!Globals.Locations["Street"].IsVisited)
        {
            await StreetEvents.LookAtEden();
            return;
        }

        await StreetEvents.Crossroads();
    }

    public static async Task GunShop()
    {
        await SaveService.UpdateSave();

        if (!Globals.Locations["GunShop"].IsVisited)
        {
            Globals.Locations["GunShop"].IsVisited = true;
            await GunShopEvents.EnterGunShop();
            return;
        }

        await GunShopEvents.Crossroads();
    }

    public static async Task NightclubEden()
    {
        await SaveService.UpdateSave();

        if (!Globals.Locations["NightclubEden"].IsVisited)
        {
            await NightclubEdenEvents.EnterClub();
            return;
        }

        await NightclubEdenEvents.Crossroads();
    }

    public static async Task VisitDarkAlley()
    {
        await Game.Instance.SetCurrentLocation(Globals.Locations["Street"]);
    }

    public static async Task VisitStreet()
    {
        await Game.Instance.SetCurrentLocation(Globals.Locations["Street"]);
    }

    public static async Task VisitNightclubEden()
    {
        await Game.Instance.SetCurrentLocation(Globals.Locations["NightclubEden"]);
    }

    public static async Task VisitGunShop()
    {
        await Display.WriteNarration($"\n\t{Localizator.GetString("NIGHTCLUB_EDEN.VISIT_CLUB")}");
        await Game.Instance.SetCurrentLocation(Globals.Locations["GunShop"]);
    }
}