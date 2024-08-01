using Nocturnal.src.entitites;
using Nocturnal.src.core;
using Nocturnal.src.services;
using Nocturnal.src.ui;

namespace Nocturnal.src.events.prologue
{
    public static class NightclubEdenEvents
    {
        ///////////////////////////////////////////////////////////////////////
        //	NIGHTCLUB 'EDEN'
        ///////////////////////////////////////////////////////////////////////

        public static async Task EnterClub()
        {
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_01")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_02")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_03")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_04")}");

            _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_MENU.GO_TO_DANCE_FLOOR"), ClubDanceFloor },
                { Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_MENU.GO_TO_BAR"), ClubBar },
                { Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_MENU.GO_UPSTAIRS"), ClubUpstairs },
                { Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_MENU.BACK_ON_STREET"), PrologueEvents.VisitStreet },
            });
        }

        public static async Task ClubDanceFloor()
        {
            if (!Globals.Npcs["Luna"].IsKnowHero)
            {
                await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_01")}");
                await Task.Delay(1000);
                await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_02")}");
                await Task.Delay(1500);
                await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_03")}");
                await Task.Delay(1500);
                await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_04")}");
                await Task.Delay(1000);
                await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_05")}");
                await Task.Delay(1500);
                await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_06")}");

                _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
                {
                    { Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_MENU.WHATS_SHE_WANTS"), ClubDanceFloor_01 },
                    { Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_MENU.HEY_BABY"), ClubDanceFloor_02 },
                    { Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_MENU.KEEP_DANCING"), ClubDanceFloor_03 },
                });

                await LunaMeeting();
            }
            else
            {
                await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_01")}");
                await Task.Delay(1000);
                await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_07")}");
                await Task.Delay(1500);
                await Crossroads();
            }
        }

        public static async Task ClubDanceFloor_01()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_08")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_09")}");
        }

        public static async Task ClubDanceFloor_02()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_08")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_10")}");
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_11")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_12")}");
        }

        public static async Task ClubDanceFloor_03()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_08")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_13")}");
        }

        public static async Task ClubBar()
        {
            if (!Program.Game!.StoryGlobals.PC_IsAtBar)
            {
                Program.Game!.StoryGlobals.PC_IsAtBar = true;
                await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.BAR_01")}");
                await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.BAR_02")}");
            };

            _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.BAR_MENU.GIVE_ANYTHING"), ClubBar_01 },
                { Display.GetJsonString("NIGHTCLUB_EDEN.BAR_MENU.WHO_RULES"), ClubBar_02 },
                { Display.GetJsonString("NIGHTCLUB_EDEN.BAR_MENU.BYE"), ClubBar_03 },
            });
        }

        public static async Task ClubBar_01()
        {
            await Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.BAR_03")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($" {Display.GetJsonString("NIGHTCLUB_EDEN.BAR_04")}");
            await Task.Delay(1500);
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.BAR_05")}");
            await Task.Delay(1500);
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.BAR_06")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.BAR_07")}.");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.BAR_08")}");
            await ClubBar();
        }

        public static async Task ClubBar_02()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.BAR_09")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.BAR_10")}");
            await Task.Delay(1500);
            await Display.WriteDialogue($" {Display.GetJsonString("NIGHTCLUB_EDEN.BAR_11")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($" {Display.GetJsonString("NIGHTCLUB_EDEN.BAR_12")}");
            await ClubBar();
        }

        public static async Task ClubBar_03()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.BAR_13")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.BAR_14")}\n");
            Program.Game!.StoryGlobals.PC_IsAtBar = false;
            await Crossroads();
        }

        public static async Task LunaMeeting()
        {
            Console.WriteLine();

            _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_MEETING_MENU.WHATS_IT_ABOUT"), LunaMeeting_01 },
                { Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_MEETING_MENU.LET_HER_SPEAK"), LunaMeeting_02 }
            });

            await Display.WriteDialogue($"\t{JsonService.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_01"]}");
            await Task.Delay(1000);
            await Display.WriteDialogue($" {JsonService.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_02"]}");
            await Globals.Player.AddQuest(Globals.Quests["KillHex"]);
            Console.WriteLine();
            await Task.Delay(1500);
            await Display.WriteNarration($"\t{JsonService.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_03"]}");
            await Display.WriteDialogue($"\n\t{JsonService.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_04"]}");

            if (Globals.Player.HasItem(Globals.Items["Pistol"]))
            {
                await Display.WriteDialogue($"\n\t{JsonService.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_05"]}");
                await Task.Delay(1000);
                await Display.WriteDialogue($" {JsonService.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_06"]}");
            }
            else
            {
                await Display.WriteDialogue($"\n\t{JsonService.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_07"]}");
                await Task.Delay(1000);
                await Display.WriteDialogue($" {JsonService.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_08"]}");
                Globals.Player.Money = 200.0f;

                await Display.Write($"\n\n\t{Display.GetJsonString("ITEM_GAINED")}");
                await Display.WriteColoredText($"200$\n\n", ConsoleColor.Green);
                Console.ResetColor();

                await Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_MEETING_09")}");
                await Display.WriteDialogue($" {Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_MEETING_10")}");
                await Task.Delay(1000);
                await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_MEETING_11")}");
            }

            await Crossroads();
        }

        public static async Task LunaMeeting_01()
        {
            await Task.Run(() => {});
        }

        public static async Task LunaMeeting_02()
        {
            await Task.Run(() => { });
        }

        public static async Task ClubUpstairs()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.UPSTAIRS_01")}");

            _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.UPSTAIRS_MENU.COME_CLOSER"), ClubUpstairs_01 },
                { Display.GetJsonString("NIGHTCLUB_EDEN.UPSTAIRS_MENU.GO_BACK"), ClubUpstairs_02 }
            });
        }

        public static async Task ClubUpstairs_01()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.UPSTAIRS_02")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.UPSTAIRS_03")}");
            Globals.Npcs["Jet"].IsKnowHero = true;
            await DialogueWithJet();
        }

        public static async Task ClubUpstairs_02()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.UPSTAIRS_04")}\n");
            await Crossroads();
        }

        public static async Task DialogueWithJet()
        {
            Menu dialogueWithJetMenu = new();
            dialogueWithJetMenu.ClearOptions();
            Dictionary<string, Func<Task>> options = new();

            await JetGetsAngry(Program.Game!.StoryGlobals.Jet_Points);

            options.Add(Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_MENU.I_WANT_PASS"), DialogueWithJet_01);
            options.Add(Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_MENU.WHATS_THERE"), DialogueWithJet_02);

            if (Globals.Player.HasItem(Globals.Items["Pistol"]) || Globals.Player.Weapon == Globals.Items["Pistol"])
            {
                options.Add(Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_MENU.KILL"), DialogueWithJet_03);
                options.Add(Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_MENU.STUN"), DialogueWithJet_04);
            }

            options.Add(Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_MENU.BYE"), DialogueWithJet_05);
            dialogueWithJetMenu.AddOptions(options);
            await dialogueWithJetMenu.ShowOptions();
            await dialogueWithJetMenu.InputChoice();
        }

        public static async Task DialogueWithJet_01()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_01")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_02")}");
            Program.Game!.StoryGlobals.Jet_Points += 1;
            await DialogueWithJet();
        }

        public static async Task DialogueWithJet_02()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_03")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_04")}");
            Program.Game!.StoryGlobals.Jet_Points += 1;
            await DialogueWithJet();
        }

        public static async Task DialogueWithJet_03()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_05")}");
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_06")}");
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_07")}");
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_08")}\n");
            await HexOffice();
        }

        public static async Task DialogueWithJet_04()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_09")}");
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_10")}");
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_11")}");
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_12")}\n");
            await HexOffice();
        }

        public static async Task DialogueWithJet_05()
        {
            await Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_13")}");
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_14")}");
            await Crossroads();
        }

        public static async Task JetGetsAngry(int angerPoints)
        {
            if (angerPoints < 2 && !Program.Game!.StoryGlobals.Jet_WarnedPlayer && !Program.Game!.StoryGlobals.Jet_BeatedPlayer)
                return;

            if (angerPoints == 2 && !Program.Game!.StoryGlobals.Jet_WarnedPlayer)
            {
                Program.Game!.StoryGlobals.Jet_WarnedPlayer = true;
                Console.WriteLine();
                await Globals.Npcs["Jet"].SetAttitude(Attitudes.Angry);
                await Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_15")}");
                return;
            }

            if (angerPoints > 2 && Program.Game!.StoryGlobals.Jet_WarnedPlayer && !Program.Game!.StoryGlobals.Jet_BeatedPlayer)
            {
                Program.Game!.StoryGlobals.Jet_BeatedPlayer = true;
                Program.Game!.StoryGlobals.Jet_Points = 0;
                Console.WriteLine();
                await Globals.Npcs["Jet"].SetAttitude(Attitudes.Hostile);
                await Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_16")}");
                await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_17")}");
                await Task.Delay(2500);
                Console.Clear();
                await Task.Delay(2500);
                await WakeUpAfterMeetingWithJet();
            }
        }

        public static async Task WakeUpAfterMeetingWithJet()
        {
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.GET_STUNNED_01")}", 65);
            await Task.Delay(1000);
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.GET_STUNNED_02")}", 60);
            await Task.Delay(1500);
            await Display.WriteNarration(Display.GetJsonString("NIGHTCLUB_EDEN.GET_STUNNED_03"));
            await Task.Delay(1500);
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.GET_STUNNED_04")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.GET_STUNNED_05")}");
            await Task.Delay(500);
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.GET_STUNNED_06")}");
            await Task.Delay(1000);
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.GET_STUNNED_07")}");

            Random rnd = new(); int rand = rnd.Next(0, 10);

            if (rand > 5 && rand <= 10)
                await RandomEvents.NickHandDiscovered();

            await Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
        }

        public static async Task HexOffice()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_OFFICE")}");

            _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.HEX_OFFICE_MENU.GO_TO_OTHER_ROOM"), HexHideoutCode },
                { Display.GetJsonString("NIGHTCLUB_EDEN.HEX_OFFICE_MENU.STAY_AND_SEARCH"), CheckHexDesk }
            });
        }

        public static async Task CheckHexDesk()
        {
            Program.Game!.StoryGlobals.PC_KnowsHexCode = true;
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.CHECK_HEX_DESK")}");
            Console.WriteLine();
            await HexHideoutCode();
        }

        public static async Task HexHideoutCode()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_CODE_01")}");
            await Task.Delay(1500);

            if (!Program.Game!.StoryGlobals.PC_KnowsHexCode)
            {
                await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_CODE_02")}");
                Console.WriteLine();
                await CheckHexDesk();
                return;
            }

            _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_CODE_MENU.USE_CODE"), RightAccessCode },
                { Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_CODE_MENU.SEARCH_OFFICE"), CheckHexDesk }
            });
        }

        public static async Task RightAccessCode()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.RIGHT_ACCES_CODE_01")}");
            await Task.Delay(2000);
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.RIGHT_ACCES_CODE_02")}");
            await HexHideout();
        }

        public static async Task HexHideout()
        {
            Globals.Npcs["HexFolstam"].Status = NpcStatus.Unconscious;
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_01")}");
            await Task.Delay(2000);
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_02")}");
            await Task.Delay(1500);
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_03")}");
            await Task.Delay(2000);
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_04")}");

            _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_MENU.KILL"), HexHideout_01 },
                { Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_MENU.WAIT"), HexHideout_02 }
            });
        }

        public static async Task HexHideout_01()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_05")}");
            await Task.Delay(1500);
            Globals.Npcs["HexFolstam"].Status = NpcStatus.Dead;
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_06")}");
            await LunaAppears();
        }

        public static async Task HexHideout_02()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_07")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_08")}");
            await Task.Delay(1500);
            Globals.Npcs["HexFolstam"].Status = NpcStatus.Normal;
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_09")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_10")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_11")}");
            await DialogueWithHex();
        }

        public static async Task DialogueWithHex()
        {
            await Task.Run(() =>
            {
                _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
                {
                    { Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_MENU_01.DIE"), DialogueWithHex_01 },
                    { Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_MENU_01.DONT_WANNA_FIGHT"), DialogueWithHex_02 },
                    { Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_MENU_01.LUNA_WANTS_YOU_DEAD"), DialogueWithHex_03 }
                });

                _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
                {
                    { Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_MENU_02.DOESNT_MATTER"), DialogueWithHex_04 },
                    { Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_MENU_02.LUNA"), DialogueWithHex_05 }
                });
            });
        }

        public static async Task LunaAppears()
        {
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_APPEARS_01")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_APPEARS_02")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_APPEARS_03")}");
            await Task.Delay(1500);
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_APPEARS_04")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_APPEARS_05")}");
            await Task.Delay(1000);
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_APPEARS_06")}");

            _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_APPEARS_MENU.DO_NOTHING"), DialogueWithHex_06 },
                { Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_APPEARS_MENU.WHAT_ITS_ABOUT"), DialogueWithHex_07 }
            });

            await HexResurrection();
        }

        public static async Task DialogueWithHex_01()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_01")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_02")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_03")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_04")}");
        }

        public static async Task DialogueWithHex_02()
        {
            await Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_05")}");
            await Task.Delay(1000);
            await HexDeath();
        }

        public static async Task DialogueWithHex_03()
        {
            await DialogueWithHex_05();
        }

        public static async Task DialogueWithHex_04()
        {
            await Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_06")}");
            await Task.Delay(1500);
            await HexDeath();
        }

        public static async Task DialogueWithHex_05()
        {
            await Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_07")}");
            await Display.WriteDialogue($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_08")}");
            await HexDeath();
        }

        public static async Task DialogueWithHex_06()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_09")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_10")}");
        }

        public static async Task DialogueWithHex_07()
        {
            await Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_11")}");
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_12")}");
        }

        public static async Task HexDeath()
        {
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_DEATH_01")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_DEATH_02")}");
            await Task.Delay(1500);
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_DEATH_03")}");
            Globals.Npcs["HexFolstam"].Status = NpcStatus.Dead;
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_DEATH_04")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_DEATH_05")}");
            await LunaAppears();
        }

        public static async Task HexResurrection()
        {
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_RESSURECTION_01")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_RESSURECTION_02")}");
            Console.ForegroundColor = ConsoleColor.Blue;
            await Display.Write($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_RESSURECTION_03")}");
            await Task.Delay(1000);
            await Display.Write($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_RESSURECTION_04")}");
            await Task.Delay(1000);
            await Display.Write($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_RESSURECTION_05")}");
            await Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_RESSURECTION_06")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_RESSURECTION_07")}");
            Console.ForegroundColor = ConsoleColor.Blue;
            await Display.Write($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_RESSURECTION_08")}");
            await Task.Delay(1500);
            Console.ForegroundColor = ConsoleColor.Blue;
            await Display.Write($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_RESSURECTION_09")}");
            await Task.Delay(1000);
            await Display.Write($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_RESSURECTION_10")}");
            await Task.Delay(3000);
            await Event.EndGame();
        }

        public static async Task Crossroads()
        {
            await Task.Run(() =>
            {
                _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
                {
                    { Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_MENU.GO_TO_DANCE_FLOOR"), ClubDanceFloor },
                    { Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_MENU.GO_TO_BAR"), ClubBar },
                    { Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_MENU.GO_UPSTAIRS"), ClubUpstairs },
                    { Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_MENU.BACK_ON_STREET"), PrologueEvents.VisitStreet },
                });
            });
        }
    }
}
