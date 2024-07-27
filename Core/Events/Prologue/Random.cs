using Nocturnal.Core.Entitites;
using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Events.Prologue
{
    public static class RandomEvents
    {
        public static async Task StartRaining()
        {
            Program.Game!.Weather = Weather.Rainy;

            if (Program.Game!.Weather == Weather.Rainy)
            {
                await Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.START_RAINING_01"]}");
                await Task.Delay(1000);
                await Display.WriteNarration($" {Globals.JsonReader!["STREET.START_RAINING_02"]}");
                await Task.Delay(500);
                await Display.WriteNarration($" {Globals.JsonReader!["STREET.START_RAINING_03"]}");
            }
        }

        public static async Task NickHandDiscovered()
        {
            await Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_01"]}");
            await Task.Delay(500);
            await Display.WriteNarration($" {Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_02"]}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_03"]}");
            await Task.Delay(500);
            await Display.WriteNarration($" {Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_04"]}");
            await Task.Delay(250);
            await Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_05"]}");
            await Task.Delay(500);
            await Display.WriteNarration($" {Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_06"]}");
            await Task.Delay(250);
            await Display.WriteNarration($" {Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_07"]}");
        }

        public static async Task HookersMeeting()
        {
            await Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.HOOKERS_MEETING_01"]}");
            await Task.Delay(500);
            await Display.WriteNarration($" {Globals.JsonReader!["STREET.HOOKERS_MEETING_02"]}");
            await Task.Delay(650);
            await Display.WriteNarration($" {Globals.JsonReader!["STREET.HOOKERS_MEETING_03"]}");
            await Task.Delay(500);
            await Display.WriteNarration($" {Globals.JsonReader!["STREET.HOOKERS_MEETING_04"]}");
            await Task.Delay(800);
            await Display.WriteNarration($" {Globals.JsonReader!["STREET.HOOKERS_MEETING_05"]}");
            await Task.Delay(1000);
            await Display.WriteDialogue($"{Globals.JsonReader!["STREET.HOOKERS_MEETING_06"]}");
            await Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.HOOKERS_MEETING_07"]}\n");

            _ = new Menu(new Dictionary<string, Func<Task>>
            {
                { $"{Globals.JsonReader!["STREET.HOOKERS_MEETING_MENU.I_DONT_HAVE_ANYTHING"]}", EndHookersMeeting_01 },
                { $"{Globals.JsonReader!["STREET.HOOKERS_MEETING_MENU.GO_TO_SHOP"]}", EndHookersMeeting_02 },
                { $"{Globals.JsonReader!["STREET.HOOKERS_MEETING_MENU.GO_TO_CLUB"]}", EndHookersMeeting_03 }
            });
        }

        public static async Task EndHookersMeeting_01()
        {
            await Display.WriteNarration($"\t{Globals.JsonReader!["STREET.HOOKERS_MEETING_08"]}");
            await Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.HOOKERS_MEETING_09"]}\n");
            await GunShopEvents.Crossroads();
        }

        public static async Task EndHookersMeeting_02()
        {
            await Display.WriteNarration($"\t{Globals.JsonReader!["STREET.HOOKERS_MEETING_09"]}\n");
            await GunShopEvents.Crossroads();
        }

        public static async Task EndHookersMeeting_03()
        {
            await Display.WriteNarration($"\t{Globals.JsonReader!["STREET.HOOKERS_MEETING_10"]}\n");
            
            if (!Globals.Npcs["Caden"].IsKnowHero && !Globals.Npcs["CadensPartner"].IsKnowHero)
            {
                if (!Globals.Npcs["Bob"].IsKnowHero)
                {
                    await StreetEvents.MeetingWithSecurityGuards();
                    await StreetEvents.MeetingWithPolicemans();
                }
                else
                    await StreetEvents.MeetingWithPolicemans();
            }
            else
            {
                await Program.Game!.SetCurrentLocation(Globals.Locations["NightclubEden"]);
            }        
        }

        //public static async Task PunksAmbush()
        //{
        //    await Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.PUNKS_AMBUSH_01"]}");
        //    await Display.WriteNarration("");
        //}

        //public static async Task ClubOverdose()
        //{
        //    await Display.WriteNarration($"{Globals.JsonReader!["NIGHTCLUB_EDEN.CLUB_OVERDOSE_01"]}");
        //}
    }
}
