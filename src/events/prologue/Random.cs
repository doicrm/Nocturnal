using Nocturnal.core;
using Nocturnal.services;
using Nocturnal.ui;

namespace Nocturnal.events.prologue
{
    public static class RandomEvents
    {
        public static async Task StartRaining()
        {
            Game.Instance.Weather = Weather.Rainy;

            if (Game.Instance.Weather != Weather.Rainy) return;

            await Display.WriteNarration($"\n\t{LocalizationService.GetString("STREET.START_RAINING_01")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {LocalizationService.GetString("STREET.START_RAINING_02")}");
            await Task.Delay(500);
            await Display.WriteNarration($" {LocalizationService.GetString("STREET.START_RAINING_03")}");
        }

        public static async Task NickHandDiscovered()
        {
            await Display.WriteNarration($"\n\t{LocalizationService.GetString("STREET.NICK_HAND_DISCOVERED_01")}");
            await Task.Delay(500);
            await Display.WriteNarration($" {LocalizationService.GetString("STREET.NICK_HAND_DISCOVERED_02")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {LocalizationService.GetString("STREET.NICK_HAND_DISCOVERED_03")}");
            await Task.Delay(500);
            await Display.WriteNarration($" {LocalizationService.GetString("STREET.NICK_HAND_DISCOVERED_04")}");
            await Task.Delay(250);
            await Display.WriteNarration($"\n\t{LocalizationService.GetString("STREET.NICK_HAND_DISCOVERED_05")}");
            await Task.Delay(500);
            await Display.WriteNarration($" {LocalizationService.GetString("STREET.NICK_HAND_DISCOVERED_06")}");
            await Task.Delay(250);
            await Display.WriteNarration($" {LocalizationService.GetString("STREET.NICK_HAND_DISCOVERED_07")}");
        }

        public static async Task HookersMeeting()
        {
            await Display.WriteNarration($"\n\t{LocalizationService.GetString("STREET.HOOKERS_MEETING_01")}");
            await Task.Delay(500);
            await Display.WriteNarration($" {LocalizationService.GetString("STREET.HOOKERS_MEETING_02")}");
            await Task.Delay(650);
            await Display.WriteNarration($" {LocalizationService.GetString("STREET.HOOKERS_MEETING_03")}");
            await Task.Delay(500);
            await Display.WriteNarration($" {LocalizationService.GetString("STREET.HOOKERS_MEETING_04")}");
            await Task.Delay(800);
            await Display.WriteNarration($" {LocalizationService.GetString("STREET.HOOKERS_MEETING_05")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($"{LocalizationService.GetString("STREET.HOOKERS_MEETING_06")}");
            await Display.WriteNarration($"\n\t{LocalizationService.GetString("STREET.HOOKERS_MEETING_07")}\n");

            _ = new InteractiveMenu(new MenuOptions
            {
                { LocalizationService.GetString("STREET.HOOKERS_MEETING_MENU.I_DONT_HAVE_ANYTHING"), EndHookersMeeting_01 },
                { LocalizationService.GetString("STREET.HOOKERS_MEETING_MENU.GO_TO_SHOP"), EndHookersMeeting_02 },
                { LocalizationService.GetString("STREET.HOOKERS_MEETING_MENU.GO_TO_CLUB"), EndHookersMeeting_03 }
            });
        }

        private static async Task EndHookersMeeting_01()
        {
            await Display.WriteNarration($"\t{LocalizationService.GetString("STREET.HOOKERS_MEETING_08")}");
            await Display.WriteNarration($"\n\t{LocalizationService.GetString("STREET.HOOKERS_MEETING_09")}\n");
            await GunShopEvents.Crossroads();
        }

        private static async Task EndHookersMeeting_02()
        {
            await Display.WriteNarration($"\t{LocalizationService.GetString("STREET.HOOKERS_MEETING_09")}\n");
            await GunShopEvents.Crossroads();
        }

        private static async Task EndHookersMeeting_03()
        {
            await Display.WriteNarration($"\t{LocalizationService.GetString("STREET.HOOKERS_MEETING_10")}\n");
            
            if (!Globals.Npcs["Caden"].IsKnowHero && !Globals.Npcs["CadensPartner"].IsKnowHero)
            {
                if (!Globals.Npcs["Bob"].IsKnowHero)
                {
                    await StreetEvents.MeetingWithSecurityGuards();
                    await StreetEvents.MeetingWithPolicemans();
                    return;
                }
                await StreetEvents.MeetingWithPolicemans();
                return;
            }

            await Game.Instance.SetCurrentLocation(Globals.Locations["NightclubEden"]);        
        }

        //public static async Task PunksAmbush()
        //{
        //    await Display.WriteNarration($"\n\t{LocalizationService.GetString("STREET.PUNKS_AMBUSH_01")}");
        //    await Display.WriteNarration("");
        //}

        //public static async Task ClubOverdose()
        //{
        //    await Display.WriteNarration($"{LocalizationService.GetString("NIGHTCLUB_EDEN.CLUB_OVERDOSE_01")}");
        //}
    }
}
