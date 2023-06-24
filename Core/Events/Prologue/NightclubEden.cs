using Nocturnal.Core.Entitites;
using Nocturnal.Core.Entitites.Characters;
using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Events.Prologue
{
    public static class NightclubEdenEvents
    {
        // ************************************************************
        // 		NIGHTCLUB 'EDEN'
        // ************************************************************

        public static void EnterClub()
        {
            Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_01")}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_02")}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_03")}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_04")}");

            Menu enterClubMenu = new(new Dictionary<string, Action>()
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_MENU.GO_TO_DANCE_FLOOR"), ClubDanceFloor },
                { Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_MENU.GO_TO_BAR"), ClubBar },
                { Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_MENU.GO_UPSTAIRS"), ClubUpstairs },
                { Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_MENU.BACK_ON_STREET"), PrologueEvents.VisitStreet },
            });
        }

        public static void ClubDanceFloor()
        {
            if (!Globals.Npcs["Luna"].IsKnowHero)
            {
                Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_01")}");
                Thread.Sleep(1000);
                Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_02")}");
                Thread.Sleep(1500);
                Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_03")}");
                Thread.Sleep(1500);
                Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_04")}");
                Thread.Sleep(1000);
                Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_05")}");
                Thread.Sleep(1500);
                Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_06")}");

                Menu danceWithLunaMenu = new(new Dictionary<string, Action>()
                {
                    { Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_MENU.WHATS_SHE_WANTS"), ClubDanceFloor_01 },
                    { Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_MENU.HEY_BABY"), ClubDanceFloor_02 },
                    { Display.GetJsonString("NIGHTCLUB_EDEN.DANCE_FLOOR_MENU.KEEP_DANCING"), ClubDanceFloor_03 },
                });

                LunaMeeting();
            }
            else
            {
                Display.WriteNarration($"\t{Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_01"]}");
                Thread.Sleep(1000);
                Display.WriteNarration($" {Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_07"]}");
                Thread.Sleep(1500);
                Crossroads();
            }
        }

        public static void ClubDanceFloor_01()
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_08"]}");
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_09"]}");
        }

        public static void ClubDanceFloor_02()
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_08"]}");
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_10"]}");
            Display.WriteNarration($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_11"]}");
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_12"]}");
        }

        public static void ClubDanceFloor_03()
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_08"]}");
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_13"]}");
        }

        public static void ClubBar()
        {
            if (!Program.Game!.StoryGlobals.PC_IsAtBar)
            {
                Program.Game!.StoryGlobals.PC_IsAtBar = true;
                Display.WriteNarration($"\t{Globals.JsonReader!["NIGHTCLUB_EDEN.BAR_01"]}");
                Display.WriteDialogue($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.BAR_02"]}");
            };

            Menu dialogueWithBartenderMenu = new(new Dictionary<string, Action>()
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.BAR_MENU.GIVE_ANYTHING"), ClubBar_01 },
                { Display.GetJsonString("NIGHTCLUB_EDEN.BAR_MENU.WHO_RULES"), ClubBar_02 },
                { Display.GetJsonString("NIGHTCLUB_EDEN.BAR_MENU.BYE"), ClubBar_03 },
            });
        }

        public static void ClubBar_01()
        {
            Display.WriteDialogue($"\t{Globals.JsonReader!["NIGHTCLUB_EDEN.BAR_03"]}");
            Thread.Sleep(1000);
            Display.WriteDialogue($" {Globals.JsonReader!["NIGHTCLUB_EDEN.BAR_04"]}");
            Thread.Sleep(1500);
            Display.WriteNarration($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.BAR_05"]}");
            Thread.Sleep(1500);
            Display.WriteNarration($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.BAR_06"]}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Globals.JsonReader!["NIGHTCLUB_EDEN.BAR_07"]}.");
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.BAR_08"]}");
            ClubBar();
        }

        public static void ClubBar_02()
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["NIGHTCLUB_EDEN.BAR_09"]}");
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.BAR_10"]}");
            Thread.Sleep(1500);
            Display.WriteDialogue($" {Globals.JsonReader!["NIGHTCLUB_EDEN.BAR_11"]}");
            Thread.Sleep(1000);
            Display.WriteDialogue($" {Globals.JsonReader!["NIGHTCLUB_EDEN.BAR_12"]}");
            ClubBar();
        }

        public static void ClubBar_03()
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["NIGHTCLUB_EDEN.BAR_13"]}");
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.BAR_14"]}\n");
            Program.Game!.StoryGlobals.PC_IsAtBar = false;
            Crossroads();
        }

        public static void LunaMeeting()
        {
            Console.WriteLine();

            Menu lunaMeetingMenu = new(new Dictionary<string, Action>()
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_MEETING_MENU.WHATS_IT_ABOUT"), LunaMeeting_01 },
                { Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_MEETING_MENU.LET_HER_SPEAK"), LunaMeeting_02 }
            });

            Display.WriteDialogue($"\t{Globals.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_01"]}");
            Thread.Sleep(1000);
            Display.WriteDialogue($" {Globals.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_02"]}");
            Globals.Player.AddQuest(Globals.Quests["KillHex"]);
            Console.WriteLine();
            Thread.Sleep(1500);
            Display.WriteNarration($"\t{Globals.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_03"]}");
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_04"]}");

            if (Globals.Player.HasItem(Globals.Items["Pistol"]))
            {
                Display.WriteDialogue($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_05"]}");
                Thread.Sleep(1000);
                Display.WriteDialogue($" {Globals.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_06"]}");
            }
            else
            {
                Display.WriteDialogue($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_07"]}");
                Thread.Sleep(1000);
                Display.WriteDialogue($" {Globals.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_08"]}");
                Globals.Player.Money = 200.0f;

                Display.Write($"\n\n\t{Display.GetJsonString("ITEM_GAINED")}");
                Console.ForegroundColor = ConsoleColor.Green;
                Display.Write($"200$\n\n");
                Console.ResetColor();

                Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_MEETING_09")}");
                Display.WriteDialogue($" {Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_MEETING_10")}");
                Thread.Sleep(1000);
                Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_MEETING_11")}");
            }

            Crossroads();
        }

        public static void LunaMeeting_01()
        {
        }

        public static void LunaMeeting_02()
        {
        }

        public static void ClubUpstairs()
        {
            Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.UPSTAIRS_01")}");

            Menu clubUpstairsMenu = new(new Dictionary<string, Action>()
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.UPSTAIRS_MENU.COME_CLOSER"), ClubUpstairs_01 },
                { Display.GetJsonString("NIGHTCLUB_EDEN.UPSTAIRS_MENU.GO_BACK"), ClubUpstairs_02 }
            });
        }

        public static void ClubUpstairs_01()
        {
            Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.UPSTAIRS_02")}");
            Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.UPSTAIRS_03")}");
            Globals.Npcs["Jet"].IsKnowHero = true;
            DialogueWithJet();
        }

        public static void ClubUpstairs_02()
        {
            Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.UPSTAIRS_04")}\n");
            Crossroads();
        }

        public static void DialogueWithJet()
        {
            Menu dialogueWithJetMenu = new();
            dialogueWithJetMenu.ClearOptions();
            Dictionary<string, Action> options = new();

            JetGetsAngry(Program.Game!.StoryGlobals.Jet_Points);

            options.Add(Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_MENU.I_WANT_PASS"), DialogueWithJet_01);
            options.Add(Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_MENU.WHATS_THERE"), DialogueWithJet_02);

            if (Globals.Player.HasItem(Globals.Items["Pistol"]) || Globals.Player.Weapon == Globals.Items["Pistol"])
            {
                options.Add(Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_MENU.KILL"), DialogueWithJet_03);
                options.Add(Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_MENU.STUN"), DialogueWithJet_04);
            }

            options.Add(Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_MENU.BYE"), DialogueWithJet_05);
            dialogueWithJetMenu.AddOptions(options);
            dialogueWithJetMenu.ShowOptions();
            dialogueWithJetMenu.InputChoice();
        }

        public static void DialogueWithJet_01()
        {
            Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_01")}");
            Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_02")}");
            Program.Game!.StoryGlobals.Jet_Points += 1;
            DialogueWithJet();
        }

        public static void DialogueWithJet_02()
        {
            Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_03")}");
            Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_04")}");
            Program.Game!.StoryGlobals.Jet_Points += 1;
            DialogueWithJet();
        }

        public static void DialogueWithJet_03()
        {
            Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_05")}");
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_06")}");
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_07")}");
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_08")}\n");
            HexOffice();
        }

        public static void DialogueWithJet_04()
        {
            Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_09")}");
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_10")}");
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_11")}");
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_12")}\n");
            HexOffice();
        }

        public static void DialogueWithJet_05()
        {
            Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_13")}");
            Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_14")}");
            Crossroads();
        }

        public static void JetGetsAngry(int angerPoints)
        {
            if (angerPoints < 2 && !Program.Game!.StoryGlobals.Jet_WarnedPlayer && !Program.Game!.StoryGlobals.Jet_BeatedPlayer)
                return;

            if (angerPoints == 2 && !Program.Game!.StoryGlobals.Jet_WarnedPlayer)
            {
                Program.Game!.StoryGlobals.Jet_WarnedPlayer = true;
                Console.WriteLine();
                Globals.Npcs["Jet"].SetAttitude(Attitudes.Angry);
                Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_15")}");
                return;
            }

            if (angerPoints > 2 && Program.Game!.StoryGlobals.Jet_WarnedPlayer && !Program.Game!.StoryGlobals.Jet_BeatedPlayer)
            {
                Program.Game!.StoryGlobals.Jet_BeatedPlayer = true;
                Program.Game!.StoryGlobals.Jet_Points = 0;
                Console.WriteLine();
                Globals.Npcs["Jet"].SetAttitude(Attitudes.Hostile);
                Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_16")}");
                Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_17")}");
                Thread.Sleep(2500);
                Console.Clear();
                Thread.Sleep(2500);
                WakeUpAfterMeetingWithJet();
            }
        }

        public static void WakeUpAfterMeetingWithJet()
        {
            Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.GET_STUNNED_01")}", 65);
            Thread.Sleep(1000);
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.GET_STUNNED_02")}", 60);
            Thread.Sleep(1500);
            Display.WriteNarration(Display.GetJsonString("NIGHTCLUB_EDEN.GET_STUNNED_03"));
            Thread.Sleep(1500);
            Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.GET_STUNNED_04")}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.GET_STUNNED_05")}");
            Thread.Sleep(500);
            Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.GET_STUNNED_06")}");
            Thread.Sleep(1000);
            Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.GET_STUNNED_07")}");

            Random rnd = new(); int rand = rnd.Next(0, 10);

            if (rand > 5 && rand <= 10)
            {
                RandomEvents.NickHandDiscovered();
            }

            Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
        }

        public static void HexOffice()
        {
            Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_OFFICE")}");

            Menu hexOfficeMenu = new(new Dictionary<string, Action>()
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.HEX_OFFICE_MENU.GO_TO_OTHER_ROOM"), HexHideoutCode },
                { Display.GetJsonString("NIGHTCLUB_EDEN.HEX_OFFICE_MENU.STAY_AND_SEARCH"), CheckHexDesk }
            });
        }

        public static void CheckHexDesk()
        {
            Program.Game!.StoryGlobals.PC_KnowsHexCode = true;
            Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.CHECK_HEX_DESK")}");
            Console.WriteLine();
            HexHideoutCode();
        }

        public static void HexHideoutCode()
        {
            Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_CODE_01")}");
            Thread.Sleep(1500);

            if (!Program.Game!.StoryGlobals.PC_KnowsHexCode)
            {
                Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_CODE_02")}");
                Console.WriteLine();
                CheckHexDesk();
            }
            else
            {
                Menu hexHideoutCodeMenu = new(new Dictionary<string, Action>()
                {
                    { Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_CODE_MENU.USE_CODE"), RightAccessCode },
                    { Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_CODE_MENU.SEARCH_OFFICE"), CheckHexDesk }
                });
            }
        }

        public static void RightAccessCode()
        {
            Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.RIGHT_ACCES_CODE_01")}");
            Thread.Sleep(2000);
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.RIGHT_ACCES_CODE_02")}");
            HexHideout();
        }

        public static void HexHideout()
        {
            Globals.Npcs["HexFolstam"].Status = NpcStatus.Unconscious;
            Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_01")}");
            Thread.Sleep(2000);
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_02")}");
            Thread.Sleep(1500);
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_03")}");
            Thread.Sleep(2000);
            Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_04")}");

            Menu hexHideoutMenu = new(new Dictionary<string, Action>()
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_MENU.KILL"), HexHideout_01 },
                { Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_MENU.WAIT"), HexHideout_02 }
            });
        }

        public static void HexHideout_01()
        {
            Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_05")}");
            Thread.Sleep(1500);
            Globals.Npcs["HexFolstam"].Status = NpcStatus.Dead;
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_06")}");
            LunaAppears();
        }

        public static void HexHideout_02()
        {
            Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_07")}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_08")}");
            Thread.Sleep(1500);
            Globals.Npcs["HexFolstam"].Status = NpcStatus.Normal;
            Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_09")}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_10")}");
            Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.HEX_HIDEOUT_11")}");
            DialogueWithHex();
        }

        public static void DialogueWithHex()
        {
            Menu dialogueWithHexMenu1 = new(new Dictionary<string, Action>()
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_MENU_01.DIE"), DialogueWithHex_01 },
                { Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_MENU_01.DONT_WANNA_FIGHT"), DialogueWithHex_02 },
                { Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_MENU_01.LUNA_WANTS_YOU_DEAD"), DialogueWithHex_03 }
            });

            Menu dialogueWithHexMenu2 = new(new Dictionary<string, Action>()
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_MENU_02.DOESNT_MATTER"), DialogueWithHex_04 },
                { Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_MENU_02.LUNA"), DialogueWithHex_05 }
            });
        }

        public static void LunaAppears()
        {
            Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_APPEARS_01")}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_APPEARS_02")}");
            Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_APPEARS_03")}");
            Thread.Sleep(1500);
            Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_APPEARS_04")}");
            Thread.Sleep(1000);
            Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_APPEARS_05")}");
            Thread.Sleep(1000);
            Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_APPEARS_06")}");

            Menu lunaAppearsMenu = new(new Dictionary<string, Action>()
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_APPEARS_MENU.DO_NOTHING"), DialogueWithHex_06 },
                { Display.GetJsonString("NIGHTCLUB_EDEN.LUNA_APPEARS_MENU.WHAT_ITS_ABOUT"), DialogueWithHex_07 }
            });

            HexResurrection();
        }

        public static void DialogueWithHex_01()
        {
            Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_01")}");
            Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_02")}");
            Thread.Sleep(1000);
            Display.WriteDialogue($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_03")}");
            Thread.Sleep(1000);
            Display.WriteDialogue($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_04")}");
        }

        public static void DialogueWithHex_02()
        {
            Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_05")}");
            Thread.Sleep(1000);
            HexDeath();
        }

        public static void DialogueWithHex_03()
        {
            DialogueWithHex_05();
        }

        public static void DialogueWithHex_04()
        {
            Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_06")}");
            Thread.Sleep(1500);
            HexDeath();
        }

        public static void DialogueWithHex_05()
        {
            Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_07")}");
            Display.WriteDialogue($" {Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_08")}");
            HexDeath();
        }

        public static void DialogueWithHex_06()
        {
            Display.WriteNarration($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_09")}");
            Display.WriteDialogue($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_10")}");
        }

        public static void DialogueWithHex_07()
        {
            Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_11")}");
            Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_HEX_12")}");
        }

        public static void HexDeath()
        {
            Display.WriteNarration($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.HEX_DEATH_01"]}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Globals.JsonReader!["NIGHTCLUB_EDEN.HEX_DEATH_02"]}");
            Thread.Sleep(1500);
            Display.WriteNarration($" {Globals.JsonReader!["NIGHTCLUB_EDEN.HEX_DEATH_03"]}");
            Globals.Npcs["HexFolstam"].Status = NpcStatus.Dead;
            Display.WriteNarration($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.HEX_DEATH_04"]}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Globals.JsonReader!["NIGHTCLUB_EDEN.HEX_DEATH_05"]}");
            LunaAppears();
        }

        public static void HexResurrection()
        {
            Display.WriteNarration($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.HEX_RESSURECTION_01"]}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Globals.JsonReader!["NIGHTCLUB_EDEN.HEX_RESSURECTION_02"]}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Display.Write($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.HEX_RESSURECTION_03"]}");
            Thread.Sleep(1000);
            Display.Write($" {Globals.JsonReader!["NIGHTCLUB_EDEN.HEX_RESSURECTION_04"]}");
            Thread.Sleep(1000);
            Display.Write($" {Globals.JsonReader!["NIGHTCLUB_EDEN.HEX_RESSURECTION_05"]}");
            Display.WriteNarration($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.HEX_RESSURECTION_06"]}");
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.HEX_RESSURECTION_07"]}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Display.Write($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.HEX_RESSURECTION_08"]}");
            Thread.Sleep(1500);
            Console.ForegroundColor = ConsoleColor.Blue;
            Display.Write($" {Globals.JsonReader!["NIGHTCLUB_EDEN.HEX_RESSURECTION_09"]}");
            Thread.Sleep(1000);
            Display.Write($" {Globals.JsonReader!["NIGHTCLUB_EDEN.HEX_RESSURECTION_10"]}");
            Thread.Sleep(3000);
            Event.EndGame();
        }

        public static void Crossroads()
        {
            Menu enterClubMenu = new(new Dictionary<string, Action>()
            {
                { Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_MENU.GO_TO_DANCE_FLOOR"), ClubDanceFloor },
                { Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_MENU.GO_TO_BAR"), ClubBar },
                { Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_MENU.GO_UPSTAIRS"), ClubUpstairs },
                { Display.GetJsonString("NIGHTCLUB_EDEN.ENTER_MENU.BACK_ON_STREET"), PrologueEvents.VisitStreet },
            });
        }
    }
}
