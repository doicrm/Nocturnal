using Nocturnal.Core.Entitites;
using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Events.Prologue;

public static class StreetEvents
{
    // ************************************************************
    // 		STREET in front of the nightclub 'Eden'
    // ************************************************************

    public static void LookAtEden()
    {
        if (!Globals.Npcs["Bob"].IsKnowHero)
        {
            Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.LOOK_AT_EDEN_01"]}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Globals.JsonReader!["STREET.LOOK_AT_EDEN_02"]}");
            Thread.Sleep(1500);
            Display.WriteNarration($" {Globals.JsonReader!["STREET.LOOK_AT_EDEN_03"]}");
        }
        else
        {
            Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.LOOK_AT_EDEN_04"]}");
            Display.WriteNarration($" {Globals.JsonReader!["STREET.LOOK_AT_EDEN_05"]}");
            Thread.Sleep(1500);
            Display.WriteNarration($" {Globals.JsonReader!["STREET.LOOK_AT_EDEN_03"]}");
        }

        Menu lookAtEdenMenu = new(new Dictionary<string, Action>()
        {
            { $"{Globals.JsonReader!["STREET.LOOK_AT_EDEN_MENU.COME_CLOSER"]}", LookAtEden_01 },
            { $"{Globals.JsonReader!["STREET.LOOK_AT_EDEN_MENU.SEARCH_AREA"]}", LookAtEden_02 }
        });
    }

    public static void LookAtEden_01()
    {
        Console.WriteLine();

        if (!Globals.Npcs["Bob"].IsKnowHero)
        {
            MeetingWithSecurityGuards();
            MeetingWithPolicemans();
        }
        else
            MeetingWithPolicemans();
    }

    public static void LookAtEden_02()
    {
        EncounterGunStore();
    }

    public static void EncounterGunStore()
    {
        Display.WriteNarration($"\t{Globals.JsonReader!["STREET.ENCOUNTER_GUN_STORE_01"]}");

        if (Program.Game!.StoryGlobals.Bob_RecommendsZed)
            Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.ENCOUNTER_GUN_STORE_02"]}");

        Random rnd = new(); int rand = rnd.Next(0, 20);

        if (rand > 10 && rand <= 15)
        {
            RandomEvents.HookersMeeting();
        }
        else if (rand > 15 && rand <= 20)
        {
            Console.WriteLine();
            RandomEvents.PunksAmbush();
        }

        Menu encounterGunStoreMenu = new(new Dictionary<string, Action>()
        {
            { $"{Globals.JsonReader!["STREET.ENCOUNTER_GUN_STORE_MENU.ENTER_SHOP"]}", EncounterGunStore_01 },
            { $"{Globals.JsonReader!["STREET.ENCOUNTER_GUN_STORE_MENU.LEAVE"]}", EncounterGunStore_02 }
        });
    }

    public static void EncounterGunStore_01()
    {
        Program.Game!.SetCurrentLocation(Globals.Locations["GunShop"]);
    }

    public static void EncounterGunStore_02()
    {
        if (!Globals.Npcs["Caden"].IsKnowHero && !Globals.Npcs["CadensPartner"].IsKnowHero)
        {
            if (!Globals.Npcs["Bob"].IsKnowHero)
            {
                MeetingWithSecurityGuards();
                MeetingWithPolicemans();
            }
            else
                MeetingWithPolicemans();
        }
        else
        {
            Crossroads();
        }
    }

    public static void MeetingWithSecurityGuards()
    {
        Display.WriteNarration($"\t{Globals.JsonReader!["STREET.MEETING_WITH_SECURITY_GUARDS_01"]}");
        Thread.Sleep(1000);
        Display.WriteNarration($" {Globals.JsonReader!["STREET.MEETING_WITH_SECURITY_GUARDS_02"]}");
        Display.WriteDialogue($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_SECURITY_GUARDS_03"]}");
        Thread.Sleep(1500);
        Display.WriteDialogue($" {Globals.JsonReader!["STREET.MEETING_WITH_SECURITY_GUARDS_04"]}");
        Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_SECURITY_GUARDS_05"]}");
        Display.WriteDialogue($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_SECURITY_GUARDS_06"]}");
        Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_SECURITY_GUARDS_07"]}");
        Thread.Sleep(1500);
        Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_SECURITY_GUARDS_08"]}");
        Display.WriteDialogue($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_SECURITY_GUARDS_09"]}");
    }

    public static void MeetingWithPolicemans()
    {
        if (Globals.Npcs["Bob"].IsKnowHero)
            Display.WriteNarration($"\t{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_01"]}");
        else
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_02"]}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_03"]}");
            Thread.Sleep(2000);
            Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_04"]}");
        }

        Display.WriteDialogue($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_05"]}");
        Thread.Sleep(1500);
        Display.WriteDialogue($" {Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_06"]}");

        if (Globals.Player.HasItem(Globals.Items["AD13"]))
            Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_07"]}");
        else
            Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_08"]}");

        Display.WriteDialogue($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_09"]}");
        Thread.Sleep(1500);
        Display.WriteDialogue($" {Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_10"]}");

        if (!Globals.Npcs["Bob"].IsKnowHero)
        {
            if (GameSettings.Lang == (int)GameLanguages.EN)
                Display.WriteDialogue("'\n");
            else
                Display.WriteDialogue("\n");

            MiscEvents.NamingHero();
            Thread.Sleep(1500);

            if (GameSettings.Lang == (int)GameLanguages.EN)
                Display.WriteDialogue($"\t- '{Globals.Player.Name}...");
            else
                Display.WriteDialogue($"\t- {Globals.Player.Name}...");

            Thread.Sleep(1000);
            Display.WriteDialogue($" {Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_11"]}");
        }
        else
        {
            Display.WriteDialogue($" {Globals.Player.Name}...");
            Thread.Sleep(1000);
            Display.WriteDialogue($" {Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_12"]}");
        }

        Globals.Npcs["CadensPartner"].IsKnowHero = true;
        Globals.Npcs["Caden"].IsKnowHero = true;
        Thread.Sleep(1500);
        Display.WriteDialogue($" {Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_13"]}");
        Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_14"]}");
        Thread.Sleep(3000);
        Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_15"]}");
        Thread.Sleep(1500);
        Display.WriteNarration($" {Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_16"]}");
        Display.WriteDialogue($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_17"]}");
        Thread.Sleep(1500);
        Display.WriteDialogue($" {Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_18"]}");
        Thread.Sleep(1000);
        Display.WriteDialogue($" {Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_19"]}");
        Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_20"]}");
        Display.WriteDialogue($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_21"]}");
        Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_22"]}");

        Menu meeetingWithPolicemansMenu = new(new Dictionary<string, Action>()
        {
            { $"{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_MENU.ENTER_CLUB"]}", MeetingWithPolicemans_01 },
            { $"{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_MENU.GO_TO_STORE"]}", MeetingWithPolicemans_02 }
        });
    }

    public static void MeetingWithPolicemans_01()
    {
        Program.Game!.SetCurrentLocation(Globals.Locations["NightclubEden"]);
    }

    public static void MeetingWithPolicemans_02()
    {
        EncounterGunStore();
    }

    public static void Crossroads()
    {
        Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.CROSSROADS_01"]}");

        Menu streetCrossroadsMenu = new(new Dictionary<string, Action>()
        {
            { $"{Globals.JsonReader!["VISIT"]}: {Globals.Locations["DarkAlley"].Name}", PrologueEvents.VisitDarkAlley },
            { $"{Globals.JsonReader!["VISIT"]}: {Globals.Locations["NightclubEden"].Name}", PrologueEvents.VisitNightclubEden },
            { $"{Globals.JsonReader!["VISIT"]}: {Globals.Locations["GunShop"].Name}", PrologueEvents.VisitGunShop }
        });
    }
}
