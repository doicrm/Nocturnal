using Nocturnal.src.core;
using Nocturnal.src.services;
using Nocturnal.src.ui;

namespace Nocturnal.src.events.prologue
{
    public static class StreetEvents
    {
        ///////////////////////////////////////////////////////////////////////
        //	STREET in front of the nightclub 'Eden'
        ///////////////////////////////////////////////////////////////////////

        public static async Task LookAtEden()
        {
            if (!Globals.Npcs["Bob"].IsKnowHero)
            {
                await Display.WriteNarration($"\n\t{Display.GetJsonString("STREET.LOOK_AT_EDEN_01")}");
                await Task.Delay(1000);
                await Display.WriteNarration($" {Display.GetJsonString("STREET.LOOK_AT_EDEN_02")}");
                await Task.Delay(1500);
                await Display.WriteNarration($" {Display.GetJsonString("STREET.LOOK_AT_EDEN_03")}");
            }
            else
            {
                await Display.WriteNarration($"\n\t{Display.GetJsonString("STREET.LOOK_AT_EDEN_04")}");
                await Display.WriteNarration($" {Display.GetJsonString("STREET.LOOK_AT_EDEN_05")}");
                await Task.Delay(1500);
                await Display.WriteNarration($" {Display.GetJsonString("STREET.LOOK_AT_EDEN_06")}");
            }

            _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
            {
                { Display.GetJsonString("STREET.LOOK_AT_EDEN_MENU.COME_CLOSER"), LookAtEden_01 },
                { Display.GetJsonString("STREET.LOOK_AT_EDEN_MENU.SEARCH_AREA"), LookAtEden_02 }
            });
        }

        public static async Task LookAtEden_01()
        {
            Console.WriteLine();
            if (!Globals.Npcs["Bob"].IsKnowHero)
                await MeetingWithSecurityGuards();
            await MeetingWithPolicemans();
        }

        public static async Task LookAtEden_02()
        {
            await EncounterGunStore();
        }

        public static async Task EncounterGunStore()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("STREET.ENCOUNTER_GUN_STORE_01")}");

            if (Program.Game!.StoryGlobals.Bob_RecommendsZed)
                await Display.WriteNarration($"\n\t{Display.GetJsonString("STREET.ENCOUNTER_GUN_STORE_02")}");

            Random rnd = new(); int rand = rnd.Next(0, 20);

            if (rand > 10 && rand <= 15)
                await RandomEvents.HookersMeeting();
            //else if (rand > 15 && rand <= 20)
            //{
            //    Console.WriteLine();
            //    RandomEvents.PunksAmbush();
            //}

            _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
            {
                { Display.GetJsonString("STREET.ENCOUNTER_GUN_STORE_MENU.ENTER_SHOP"), EncounterGunStore_01 },
                { Display.GetJsonString("STREET.ENCOUNTER_GUN_STORE_MENU.LEAVE"), EncounterGunStore_02 }
            });
        }

        public static async Task EncounterGunStore_01()
        {
            await Program.Game!.SetCurrentLocation(Globals.Locations["GunShop"]);
        }

        public static async Task EncounterGunStore_02()
        {
            if (!Globals.Npcs["Caden"].IsKnowHero && !Globals.Npcs["CadensPartner"].IsKnowHero)
            {
                if (!Globals.Npcs["Bob"].IsKnowHero)
                    await MeetingWithSecurityGuards();
                await MeetingWithPolicemans();
                return;
            }

            await Crossroads();
        }

        public static async Task MeetingWithSecurityGuards()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("STREET.MEETING_WITH_SECURITY_GUARDS_01")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {Display.GetJsonString("STREET.MEETING_WITH_SECURITY_GUARDS_02")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_SECURITY_GUARDS_03")}");
            await Task.Delay(1500);
            await Display.WriteDialogue($" {Display.GetJsonString("STREET.MEETING_WITH_SECURITY_GUARDS_04")}");
            await Display.WriteNarration($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_SECURITY_GUARDS_05")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_SECURITY_GUARDS_06")}");
            await Display.WriteNarration($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_SECURITY_GUARDS_07")}");
            await Task.Delay(1500);
            await Display.WriteNarration($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_SECURITY_GUARDS_08")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_SECURITY_GUARDS_09")}");
        }

        public static async Task MeetingWithPolicemans()
        {
            if (Globals.Npcs["Bob"].IsKnowHero)
                await Display.WriteNarration($"\t{Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_01")}");
            else
            {
                await Display.WriteNarration($"\t{Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_02")}");
                await Task.Delay(1000);
                await Display.WriteNarration($" {Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_03")}");
                await Task.Delay(2000);
                await Display.WriteNarration($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_04")}");
            }

            await Display.WriteDialogue($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_05")}");
            await Task.Delay(1500);
            await Display.WriteDialogue($" {Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_06")}");

            if (Globals.Player.HasItem(Globals.Items["AD13"]))
                await Display.WriteNarration($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_07")}");
            else
                await Display.WriteNarration($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_08")}");

            await Display.WriteDialogue($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_09")}");
            await Task.Delay(1500);
            await Display.WriteDialogue($" {Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_10")}");

            if (!Globals.Npcs["Bob"].IsKnowHero)
            {
                if (Game.Instance.Settings.Language.IsSetLanguage(GameLanguages.EN))
                    await Display.WriteDialogue("'\n");
                else
                    await Display.WriteDialogue("\n");

                await MiscEvents.NamingHero();
                await Task.Delay(1500);

                if (Game.Instance.Settings.Language.IsSetLanguage(GameLanguages.EN))
                    await Display.WriteDialogue($"\t- '{Globals.Player.Name}...");
                else
                    await Display.WriteDialogue($"\t- {Globals.Player.Name}...");

                await Task.Delay(1000);
                await Display.WriteDialogue($" {Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_11")}");
            }
            else
            {
                await Display.WriteDialogue($" {Globals.Player.Name}...");
                await Task.Delay(1000);
                await Display.WriteDialogue($" {Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_12")}");
            }

            Globals.Npcs["CadensPartner"].IsKnowHero = true;
            Globals.Npcs["Caden"].IsKnowHero = true;
            await Task.Delay(1500);
            await Display.WriteDialogue($" {Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_13")}");
            await Display.WriteNarration($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_14")}");
            await Task.Delay(3000);
            await Display.WriteNarration($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_15")}");
            await Task.Delay(1500);
            await Display.WriteNarration($" {Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_16")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_17")}");
            await Task.Delay(1500);
            await Display.WriteDialogue($" {Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_18")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($" {Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_19")}");
            await Display.WriteNarration($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_20")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_21")}");
            await Display.WriteNarration($"\n\t{Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_22")}");

            _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
            {
                { Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_MENU.ENTER_CLUB"), MeetingWithPolicemans_01 },
                { Display.GetJsonString("STREET.MEETING_WITH_POLICEMANS_MENU.GO_TO_STORE"), MeetingWithPolicemans_02 }
            });
        }

        public static async Task MeetingWithPolicemans_01()
        {
            await Program.Game!.SetCurrentLocation(Globals.Locations["NightclubEden"]);
        }

        public static async Task MeetingWithPolicemans_02()
        {
            await EncounterGunStore();
        }

        public static async Task Crossroads()
        {
            await Display.WriteNarration($"\n\t{JsonService.JsonReader!["STREET.CROSSROADS_01"]}");

            _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
            {
                { $"{Display.GetJsonString("VISIT")}: {Globals.Locations["DarkAlley"].Name}", PrologueEvents.VisitDarkAlley },
                { $"{Display.GetJsonString("VISIT")}: {Globals.Locations["NightclubEden"].Name}", PrologueEvents.VisitNightclubEden },
                { $"{Display.GetJsonString("VISIT")}: {Globals.Locations["GunShop"].Name}", PrologueEvents.VisitGunShop }
            });
        }
    }
}
