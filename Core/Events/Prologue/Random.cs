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
            Display.WriteNarration($"{Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_01"]}");
            Thread.Sleep(500);
            Display.WriteNarration($"{Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_02"]}");
            Thread.Sleep(1000);
            Display.WriteNarration($"{Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_03"]}");
            Thread.Sleep(500);
            Display.WriteNarration($"{Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_04"]}");
            Thread.Sleep(250);
            Display.WriteNarration($"{Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_05"]}");
            Thread.Sleep(500);
            Display.WriteNarration($"{Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_06"]}");
            Thread.Sleep(250);
            Display.WriteNarration($"{Globals.JsonReader!["STREET.NICK_HAND_DISCOVERED_07"]}");
        }

        public static void HookersMeeting()
        {
            Display.WriteNarration($"{Globals.JsonReader!["STREET.HOOKERS_MEETING_01"]}");
            Thread.Sleep(500);
            Display.WriteNarration($"{Globals.JsonReader!["STREET.HOOKERS_MEETING_02"]}");
            Thread.Sleep(650);
            Display.WriteNarration($"{Globals.JsonReader!["STREET.HOOKERS_MEETING_03"]}");
            Thread.Sleep(500);
            Display.WriteNarration($"{Globals.JsonReader!["STREET.HOOKERS_MEETING_04"]}");
            Thread.Sleep(800);
            Display.WriteNarration($"{Globals.JsonReader!["STREET.HOOKERS_MEETING_05"]}");
            Thread.Sleep(1000);
            Display.Write($"{Globals.JsonReader!["STREET.HOOKERS_MEETING_06"]}");
            Display.WriteNarration($"{Globals.JsonReader!["STREET.HOOKERS_MEETING_07"]}");

            Menu hookersMeetingMenu = new(new Dictionary<string, Action>()
            {
                { $"{Globals.JsonReader!["STREET.HOOKERS_MEETING_MENU.01"]}", null! },
                { $"{Globals.JsonReader!["STREET.HOOKERS_MEETING_MENU.02"]}", null! },
                { $"{Globals.JsonReader!["STREET.HOOKERS_MEETING_MENU.03"]}", null! }
            });
        }

        public static void PunksAmbush()
        {
            Display.WriteNarration($"{Globals.JsonReader!["STREET.PUNKS_AMBUSH_01"]}");
            Display.WriteNarration("");
        }

        public static void ClubOverdose()
        {
            Display.WriteNarration($"{Globals.JsonReader!["NIGHTCLUB_EDEN.CLUB_OVERDOSE_01"]}");
        }
    }
}
