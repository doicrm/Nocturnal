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
            Display.WriteNarration($" {Globals.JsonReader!["STREET.LOOK_AT_EDEN_03"]}\n");
        }
        else
        {
            Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.LOOK_AT_EDEN_04"]}");
            Display.WriteNarration($" {Globals.JsonReader!["STREET.LOOK_AT_EDEN_05"]}");
            Thread.Sleep(1500);
            Display.WriteNarration($" {Globals.JsonReader!["STREET.LOOK_AT_EDEN_03"]}\n");
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

        Console.Write("\n");

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
        Console.WriteLine();

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
            Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
        }
    }

    public static void MeetingWithSecurityGuards()
    {
        Display.WriteNarration("\tWhen you get closer, the faces turn toward you and start looking at you intently.");
        Thread.Sleep(1000);
        Display.WriteNarration(" You pass\n\tthem in silenceand are confronted by a broad-shouldered security guard.");
        Display.WriteDialogue("\n\t- 'What are you looking for here?");
        Thread.Sleep(1500);
        Display.WriteDialogue(" Trouble, maybe?'");
        Display.WriteNarration("\n\tThe man clenches his hands into fists and smiles unpleasantly.");
        Display.WriteDialogue("\n\t- 'You asshole, better let us in! I want a drink and for fuck's sake. I'm losing my patience!");
        Display.WriteNarration("\n\tOne of the men waiting in the queue rushes forward and threatens the bouncer with his fist.");
        Thread.Sleep(1500);
        Display.WriteNarration("\n\tThe security guard's attention shifts from you to the furious guy next to you.");
        Display.WriteDialogue("\n\t- 'I'll say it one last time: get the fuck out of here or you'll get fucked.'\n");
    }

    public static void MeetingWithPolicemans()
    {
        if (Globals.Npcs["Bob"].IsKnowHero)
        {
            Display.WriteNarration("\tWhen you get closer, one of the police officers in a dark blue uniform turns toward you.");
        }
        else
        {
            Display.WriteNarration("\tIn an instant the street is filled with the howling of a police siren.");
            Thread.Sleep(1000);
            Display.WriteNarration(" The reds and blues\n\tbegin to dance with each other on the sidewalk and the silhouettes of the people around you.");
            Thread.Sleep(2000);
            Display.WriteNarration("\n\tTwo grim-looking guys in dark blue uniforms get out of a police car and walk towards you.\n\tOne of them points at you, taking out a tablet from behind his belt.");
        }

        Display.WriteDialogue("\n\t- 'Who are you?");
        Thread.Sleep(1500);
        Display.WriteDialogue(" And what are you doing here? Please show me your ID card.'");

        if (Globals.Player.HasItem(Globals.Items["AD13"]))
        {
            Display.WriteNarration("\n\tYou start searching through the pockets of your jacket and pants, but other than the accelerator\n\tyou found in the trash, there's nothing else there.");
        }
        else
        {
            Display.WriteNarration("\n\tYou start searching through the pockets of your jacket and pants, but there's nothing there.");
        }

        Display.WriteDialogue("\n\t- 'I see that we have a problem.");
        Thread.Sleep(1500);
        Display.WriteDialogue(" Okay, then what's your name, citizen?");

        if (!Globals.Npcs["Bob"].IsKnowHero)
        {
            Display.WriteDialogue("'\n");
            MiscEvents.NamingHero();
            Thread.Sleep(1500);
            Display.WriteDialogue($"\t- '{Globals.Player.Name}...");
            Thread.Sleep(1000);
            Display.WriteDialogue(" Caden, check it out in the database.");
        }
        else
        {
            Display.WriteDialogue($" {Globals.Player.Name}...");
            Thread.Sleep(1000);
            Display.WriteDialogue(" Caden, check\n\tit out in the database.");
        }

        Globals.Npcs["CadensPartner"].IsKnowHero = true;
        Globals.Npcs["Caden"].IsKnowHero = true;
        Thread.Sleep(1500);
        Display.WriteDialogue(" And you, stand where you are.'");
        Display.WriteNarration("\n\tThe other police officer nods, gets back in the car, and it looks like he's connecting with\n\theadquarters.");
        Thread.Sleep(3000);
        Display.WriteNarration("\n\tA minute later, the same policeman returns and whispers something in his partner's ear.");
        Thread.Sleep(1500);
        Display.WriteNarration(" That\n\tone nods and turns to look at you.");
        Display.WriteDialogue("\n\t- 'I have good news for you.");
        Thread.Sleep(1500);
        Display.WriteDialogue(" You're free for now, just don't let it occur to you to do\n\tsomething here, or you'll end up in arrest at the police station.");
        Thread.Sleep(1000);
        Display.WriteDialogue(" Caden, take care of the\n\trest of the attendees.'");
        Display.WriteNarration("\n\tThe cop walks away to talk to the nearest person standing.");
        Display.WriteDialogue("\n\t- 'If you're so pure, get in.'");
        Display.WriteNarration("\n\tThe bouncer points to the door behind him.\n\n");

        Menu meeetingWithPolicemansMenu = new(new Dictionary<string, Action>()
        {
            { $"{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_MENU.ENTER_CLUB"]}", MeetingWithPolicemans_01 },
            { $"{Globals.JsonReader!["STREET.MEETING_WITH_POLICEMANS_MENU.GO_TO"]}", MeetingWithPolicemans_02 }
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
        Display.WriteNarration($"\n\t{Globals.JsonReader!["STREET.CROSSROADS_01"]}\n");

        Menu streetCrossroadsMenu = new(new Dictionary<string, Action>()
        {
            { $"{Globals.JsonReader!["VISIT"]}: {Globals.Locations["DarkAlley"].Name}", PrologueEvents.VisitDarkAlley },
            { $"{Globals.JsonReader!["VISIT"]}: {Globals.Locations["NightclubEden"].Name}", PrologueEvents.VisitNightclubEden },
            { $"{Globals.JsonReader!["VISIT"]}: {Globals.Locations["GunShop"].Name}", PrologueEvents.VisitGunShop }
        });
    }
}
