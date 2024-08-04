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
            await Display.Write($"\n\t{LocalizationService.GetString("PROLOGUE")}");
            await Task.Delay(2000);
            await Display.Write($"\n\n\t{LocalizationService.GetString("PARADISE_LOST")}");
            await Task.Delay(5000);
            Console.Clear();
            await Intro();
        }

        public static async Task Intro()
        {
            await Display.Write($"\n{LocalizationService.GetString("INTRO_01")}\n");
            await Task.Delay(1000);
            await Display.Write($" {LocalizationService.GetString("INTRO_02")}\n\n", 20);
            await Game.Pause();
            Console.Clear();
            Console.WriteLine();
            await Task.Delay(2500);
            await Display.WriteNarration($"{LocalizationService.GetString("INTRO_03")}\n", 75);
            await Task.Delay(2500);
            await Display.WriteNarration($"{LocalizationService.GetString("INTRO_04")}\n", 75);
            await Task.Delay(2500);
            await Display.WriteNarration($"{LocalizationService.GetString("INTRO_05")}", 75);
            await Task.Delay(3000);
            Console.Clear();

            if (Globals.Locations.ContainsKey("DarkAlley"))
                await Game.Instance.SetCurrentLocation(Globals.Locations["DarkAlley"]);
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
                int rand = new Random().Next(0, 10);
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
            await Display.WriteNarration($"\n\t{LocalizationService.GetString("NIGHTCLUB_EDEN.VISIT_CLUB")}");
            await Game.Instance.SetCurrentLocation(Globals.Locations["GunShop"]);
        }
    }
}
