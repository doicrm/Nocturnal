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
            Display.WriteNarration($"{Globals.JsonReader!["STREET.LOOK_AT_EDEN_01"]}");
            Thread.Sleep(1000);
            Display.WriteNarration($"{Globals.JsonReader!["STREET.LOOK_AT_EDEN_02"]}");
            Thread.Sleep(1500);
            Display.WriteNarration($"{Globals.JsonReader!["STREET.LOOK_AT_EDEN_03"]}");
        }
        else
        {
            Display.WriteNarration($"{Globals.JsonReader!["STREET.LOOK_AT_EDEN_04"]}");
            Display.WriteNarration($"{Globals.JsonReader!["STREET.LOOK_AT_EDEN_05"]}");
            Thread.Sleep(1500);
            Display.WriteNarration($"{Globals.JsonReader!["STREET.LOOK_AT_EDEN_06"]}");
        }

        Menu lookAtEdenMenu = new(new Dictionary<string, Action>()
        {
            { $"{Globals.JsonReader!["STREET.LOOK_AT_EDEN_MENU.FIND_OUT_FINALLY"]}", LookAtEden_01 },
            { $"{Globals.JsonReader!["STREET.LOOK_AT_EDEN_MENU.IGNORE_STRANGER"]}", LookAtEden_02 }
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
        Program.Game!.SetCurrentLocation(Globals.Locations["GunShop"]);
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

        Console.Write("\n\n");

        Menu encounterGunStoreMenu = new(new Dictionary<string, Action>()
        {
            { $"{Globals.JsonReader!["STREET.LOOK_AT_EDEN_MENU.FIND_OUT_FINALLY"]}", GunShopEvents.EnterGunShop },
            { $"{Globals.JsonReader!["STREET.LOOK_AT_EDEN_MENU.IGNORE_STRANGER"]}", EndEncounterGunStore }
        });
    }

    public static void EndEncounterGunStore()
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
    //    Logger::startFuncLog(__FUNCTION__);
    //    Display::writeNarration("\tWhen you get closer, the faces turn toward you and start looking at you intently.");
    //    Console::wait(1000);
    //    Display::writeNarration(" You pass\n\tthem in silenceand are confronted by a broad-shouldered security guard.");
    //    Display::writeDialogue("\n\t- 'What are you looking for here?");
    //    Console::wait(1500);
    //    Display::writeDialogue(" Trouble, maybe?'");
    //    Display::writeNarration("\n\tThe man clenches his hands into fists and smiles unpleasantly.");
    //    Display::writeDialogue("\n\t- 'You asshole, better let us in! I want a drink and for fuck's sake. I'm losing my patience!");
    //    Display::writeNarration("\n\tOne of the men waiting in the queue rushes forward and threatens the bouncer with his fist.");
    //    Console::wait(1500);
    //    Display::writeNarration("\n\tThe security guard's attention shifts from you to the furious guy next to you.");
    //    Display::writeDialogue("\n\t- 'I'll say it one last time: get the fuck out of here or you'll get fucked.'\n");
    }

    public static void MeetingWithPolicemans()
    {
    //    Logger::startFuncLog(__FUNCTION__);

    //    if (Npc::npcs["Bob"].knowsHero())
    //    {
    //        Display::writeNarration("\tWhen you get closer, one of the police officers in a dark blue uniform turns toward you.");
    //    }
    //    else
    //    {
    //        Display::writeNarration("\tIn an instant the street is filled with the howling of a police siren.");
    //        Console::wait(1000);
    //        Display::writeNarration(" The reds and blues\n\tbegin to dance with each other on the sidewalk and the silhouettes of the people around you.");
    //        Console::wait(2000);
    //        Display::writeNarration("\n\tTwo grim-looking guys in dark blue uniforms get out of a police car and walk towards you.\n\tOne of them points at you, taking out a tablet from behind his belt.");
    //    }

    //    Display::writeDialogue("\n\t- 'Who are you?");
    //    Console::wait(1500);
    //    Display::writeDialogue(" And what are you doing here? Please show me your ID card.'");

    //    if (Hero::heroes["Hero"].hasItem(&Item::items["AD13"]))
    //    {
    //        Display::writeNarration("\n\tYou start searching through the pockets of your jacket and pants, but other than the accelerator\n\tyou found in the trash, there's nothing else there.");
    //    }
    //    else
    //    {
    //        Display::writeNarration("\n\tYou start searching through the pockets of your jacket and pants, but there's nothing there.");
    //    }

    //    Display::writeDialogue("\n\t- 'I see that we have a problem.");
    //    Console::wait(1500);
    //    Display::writeDialogue(" Okay, then what's your name, citizen?");

    //    if (!Npc::npcs["Bob"].knowsHero())
    //    {
    //        Display::writeDialogue("'\n");
    //        namingHero();
    //        Console::wait(1500);
    //        Display::writeDialogue("\t- '" + Npc::npcs["Hero"].getName() + "...");
    //        Console::wait(1000);
    //        Display::writeDialogue(" Caden, check it out in the database.");
    //    }
    //    else
    //    {
    //        Display::writeDialogue(" " + Npc::npcs["Hero"].getName() + "...");
    //        Console::wait(1000);
    //        Display::writeDialogue(" Caden, check\n\tit out in the database.");
    //    }

    //    Npc::npcs["CadenPartner"].setToKnowHero();
    //    Npc::npcs["Caden"].setToKnowHero();
    //    Console::wait(1500);
    //    Display::writeDialogue(" And you, stand where you are.'");
    //    Display::writeNarration("\n\tThe other police officer nods, gets back in the car, and it looks like he's connecting with\n\theadquarters.");
    //    Console::wait(3000);
    //    Display::writeNarration("\n\tA minute later, the same policeman returns and whispers something in his partner's ear.");
    //    Console::wait(1500);
    //    Display::writeNarration(" That\n\tone nods and turns to look at you.");
    //    Display::writeDialogue("\n\t- 'I have good news for you.");
    //    Console::wait(1500);
    //    Display::writeDialogue(" You're free for now, just don't let it occur to you to do\n\tsomething here, or you'll end up in arrest at the police station.");
    //    Console::wait(1000);
    //    Display::writeDialogue(" Caden, take care of the\n\trest of the attendees.'");
    //    Display::writeNarration("\n\tThe cop walks away to talk to the nearest person standing.");
    //    Display::writeDialogue("\n\t- 'If you're so pure, get in.'");
    //    Display::writeNarration("\n\tThe bouncer points to the door behind him.\n\n");

    //    Menu menu6({
    //        std::make_pair(json["prologue"]["menu6"][0], MeetingWithPolicemans_01),
    //    std::make_pair(json["prologue"]["menu6"][1], MeetingWithPolicemans_02)
    //        });
    }

    public static void MeetingWithPolicemans_01()
    {
        Program.Game!.SetCurrentLocation(Globals.Locations["Nightclub"]);
    }

    public static void MeetingWithPolicemans_02()
    {
        Program.Game!.SetCurrentLocation(Globals.Locations["GunShop"]);
    }

    public static void Crossroads()
    {
        Display.WriteNarration("\n\tOnce again you are on a street bathed in nighttime darkness.\n\n");

        Menu streetCrossroadsMenu = new(new Dictionary<string, Action>()
        {
            { $"{Globals.JsonReader!["VISIT"]}: {Globals.Locations["DarkAlley"].Name}", PrologueEvents.VisitDarkAlley },
            { $"{Globals.JsonReader!["VISIT"]}: {Globals.Locations["NightclubEden"].Name}", PrologueEvents.VisitNightclubEden },
            { $"{Globals.JsonReader!["VISIT"]}: {Globals.Locations["GunShop"].Name}", PrologueEvents.VisitGunShop }
        });
    }
}
