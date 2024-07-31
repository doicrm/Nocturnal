using Nocturnal.src.core;
using Nocturnal.src.services;
using Nocturnal.src.ui;

namespace Nocturnal.src.events.prologue
{
    public static class PrologueEvents
    {
        public static async Task Prologue()
        {
            Globals.Chapter = 0;
            await Display.Write($"\n\t{Display.GetJsonString("PROLOGUE")}");
            await Task.Delay(2000);
            await Display.Write($"\n\n\t{Display.GetJsonString("PARADISE_LOST")}");
            await Task.Delay(5000);
            Console.Clear();
            await StoryIntroduction();
        }

        public static async Task StoryIntroduction()
        {
            await Display.Write($"\n\t{Display.GetJsonString("INTRO_01")}");
            await Task.Delay(1000);
            await Display.Write($" {Display.GetJsonString("INTRO_02")}\n\n", 20);
            await Game.Pause();
            Console.Clear();
            Console.WriteLine();
            await Task.Delay(2500);
            await Display.WriteNarration($"\t{Display.GetJsonString("INTRO_03")}", 75);
            await Task.Delay(2500);
            await Display.WriteNarration($"\n\t{Display.GetJsonString("INTRO_04")}", 75);
            await Task.Delay(2500);
            await Display.WriteNarration($"\n\t{Display.GetJsonString("INTRO_05")}", 75);
            await Task.Delay(3000);
            Console.Clear();

            if (Globals.Locations.ContainsKey("DarkAlley"))
                await Program.Game!.SetCurrentLocation(Globals.Locations["DarkAlley"]);
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
            if (Program.Game!.Weather != Weather.Rainy)
            {
                Random rnd = new(); int rand = rnd.Next(0, 10);
                if (rand > 5 && rand <= 10)
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
            await Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
        }

        public static async Task VisitStreet()
        {
            await Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
        }

        public static async Task VisitNightclubEden()
        {
            await Program.Game!.SetCurrentLocation(Globals.Locations["NightclubEden"]);
        }

        public static async Task VisitGunShop()
        {
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.VISIT_CLUB")}");
            await Program.Game!.SetCurrentLocation(Globals.Locations["GunShop"]);
        }
    }
}
