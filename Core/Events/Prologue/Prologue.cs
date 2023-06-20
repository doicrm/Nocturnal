using Nocturnal.Core.System;

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
        DarkAlleyEvents.DarkAlleyCrossroads();
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

        StreetEvents.StreetCrossroads();
    }
}
