using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Events.Prologue;

public static class PrologueEvents
{
    public static void DarkAlley()
    {
        Globals.Chapter = 0;

        if (!Globals.Locations["DarkAlley"].IsVisited)
        {
            Globals.Locations["DarkAlley"].IsVisited = true;
            SaveManager.UpdateSave();
            DarkAlleyEvents.Prologue();
            return;
        }
        DarkAlleyEvents.Crossroads();
    }

    public static void Street()
    {
        SaveManager.UpdateSave();
        if (Program.Game!.Weather != Weather.Rainy)
        {
            Random rnd = new(); int rand = rnd.Next(0, 10);
            if (rand > 5 && rand <= 10)
                RandomEvents.StartRaining();
        }

        if (!Globals.Locations["Street"].IsVisited)
        {
            Globals.Locations["Street"].IsVisited = true;
            StreetEvents.LookAtEden();
            return;
        }

        StreetEvents.Crossroads();
    }

    public static void GunShop()
    {
        if (!Globals.Locations["GunShop"].IsVisited)
        {
            Globals.Locations["GunShop"].IsVisited = true;
            StreetEvents.EncounterGunStore();
            return;
        }

        GunShopEvents.Crossroads();
    }

    public static void Nightclub()
    {
        if (!Globals.Locations["GunShop"].IsVisited)
        {
            Globals.Locations["GunShop"].IsVisited = true;
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
        Display.WriteNarration("\n\tYou enter from a fairly well-lit street into a slightly darkened nightclub, trembling with colour.\n\n");
        Program.Game!.SetCurrentLocation(Globals.Locations["NightclubEden"]);
    }

    public static void VisitGunShop()
    {
        Program.Game!.SetCurrentLocation(Globals.Locations["GunShop"]);
    }
}
