using Nocturnal.Core.System;

namespace Nocturnal.Core.Events.Prologue;

public static class PrologueEvents
{
    public static void DarkAlley()
    {
        if (!Globals.Locations["DarkAlley"].IsVisited)
        {
            Globals.Locations["DarkAlley"].IsVisited = true;
            DarkAlleyEvents.Prologue();
            return;
        }
        DarkAlleyEvents.DarkAlleyCrossroads();
    }

    public static void Street()
    {
        if (Program.Game!.Weather != Weather.Rainy)
        {
            Random rnd = new();
            int rand = rnd.Next(0, 10);

            if (rand > 5 && rand <= 10)
                StreetEvents.DownpourStart();
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
