using Nocturnal.Core.Entitites;
using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Events.Prologue
{
    public static class RandomEvents
    {
        public static void StartRaining()
        {
            Program.Game!.Weather = Weather.Rainy;

            if (Program.Game!.Weather == Weather.Rainy)
            {
                Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.START_RAINING_01"]}");
                Thread.Sleep(1000);
                Display.WriteNarration($" {Globals.JsonReader!["STREET.START_RAINING_02"]}");
                Thread.Sleep(500);
                Display.WriteNarration($" {Globals.JsonReader!["STREET.START_RAINING_03"]}");
            }
        }

        public static void NickHandDiscovered()
        {
            Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_01"]}");
            Thread.Sleep(500);
            Display.WriteNarration($" {Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_02"]}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_03"]}");
            Thread.Sleep(500);
            Display.WriteNarration($" {Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_04"]}");
            Thread.Sleep(250);
            Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_05"]}");
            Thread.Sleep(500);
            Display.WriteNarration($" {Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_06"]}");
            Thread.Sleep(250);
            Display.WriteNarration($" {Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_07"]}");
        }

        public static void HookersMeeting()
        {
            Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.HOOKERS_MEETING_01"]}");
            Thread.Sleep(500);
            Display.WriteNarration($" {Globals.JsonReader!["STREET.HOOKERS_MEETING_02"]}");
            Thread.Sleep(650);
            Display.WriteNarration($" {Globals.JsonReader!["STREET.HOOKERS_MEETING_03"]}");
            Thread.Sleep(500);
            Display.WriteNarration($" {Globals.JsonReader!["STREET.HOOKERS_MEETING_04"]}");
            Thread.Sleep(800);
            Display.WriteNarration($" {Globals.JsonReader!["STREET.HOOKERS_MEETING_05"]}");
            Thread.Sleep(1000);
            Display.WriteDialogue($"{Globals.JsonReader!["STREET.HOOKERS_MEETING_06"]}");
            Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.HOOKERS_MEETING_07"]}\n");

            Menu hookersMeetingMenu = new(new Dictionary<string, Action>()
            {
                { $"{Globals.JsonReader!["STREET.HOOKERS_MEETING_MENU.I_DONT_HAVE_ANYTHING"]}", EndHookersMeeting_01 },
                { $"{Globals.JsonReader!["STREET.HOOKERS_MEETING_MENU.GO_TO_SHOP"]}", EndHookersMeeting_02 },
                { $"{Globals.JsonReader!["STREET.HOOKERS_MEETING_MENU.GO_TO_CLUB"]}", EndHookersMeeting_03 }
            });
        }

        public static void EndHookersMeeting_01()
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["STREET.HOOKERS_MEETING_08"]}");
            Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.HOOKERS_MEETING_09"]}\n");
            GunShopEvents.Crossroads();
        }

        public static void EndHookersMeeting_02()
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["STREET.HOOKERS_MEETING_09"]}\n");
            GunShopEvents.Crossroads();
        }

        public static void EndHookersMeeting_03()
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["STREET.HOOKERS_MEETING_10"]}\n");
            
            if (!Globals.Npcs["Caden"].IsKnowHero && !Globals.Npcs["CadensPartner"].IsKnowHero)
            {
                if (!Globals.Npcs["Bob"].IsKnowHero)
                {
                    StreetEvents.MeetingWithSecurityGuards();
                    StreetEvents.MeetingWithPolicemans();
                }
                else
                    StreetEvents.MeetingWithPolicemans();
            }
            else
            {
                Program.Game!.SetCurrentLocation(Globals.Locations["NightclubEden"]);
            }        
        }

        //public static void PunksAmbush()
        //{
        //    Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.PUNKS_AMBUSH_01"]}");
        //    Display.WriteNarration("");
        //}

        //public static void ClubOverdose()
        //{
        //    Display.WriteNarration($"{Globals.JsonReader!["NIGHTCLUB_EDEN.CLUB_OVERDOSE_01"]}");
        //}
    }
}
