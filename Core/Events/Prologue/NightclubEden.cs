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
            Display.WriteNarration($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.ENTER_01"]}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Globals.JsonReader!["NIGHTCLUB_EDEN.ENTER_02"]}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Globals.JsonReader!["NIGHTCLUB_EDEN.ENTER_03"]}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Globals.JsonReader!["NIGHTCLUB_EDEN.ENTER_04"]}");

            Menu enterClubMenu = new(new Dictionary<string, Action>()
            {
                { $"{Globals.JsonReader!["NIGHTCLUB_EDEN.ENTER_MENU.GO_TO_DANCE_FLOOR"]}", ClubDanceFloor },
                { $"{Globals.JsonReader!["NIGHTCLUB_EDEN.ENTER_MENU.GO_TO_BAR"]}", ClubBar },
                { $"{Globals.JsonReader!["NIGHTCLUB_EDEN.ENTER_MENU.GO_UPSTAIRS"]}", ClubUpstairs },
                { $"{Globals.JsonReader!["NIGHTCLUB_EDEN.ENTER_MENU.BACK_ON_STREET"]}", PrologueEvents.VisitStreet },
            });
        }

        public static void ClubDanceFloor()
        {
            if (!Globals.Npcs["Luna"].IsKnowHero)
            {
                Display.WriteNarration($"\t{Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_01"]}");
                Thread.Sleep(1000);
                Display.WriteNarration($" {Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_02"]}");
                Thread.Sleep(1500);
                Display.WriteNarration($" {Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_03"]}");
                Thread.Sleep(1500);
                Display.WriteNarration($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_04"]}");
                Thread.Sleep(1000);
                Display.WriteNarration($" {Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_05"]}");
                Thread.Sleep(1500);
                Display.WriteNarration($" {Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_06"]}");

                Menu danceWithLunaMenu = new(new Dictionary<string, Action>()
                {
                    { $"{Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_MENU.WHATS_SHE_WANTS"]}", ClubDanceFloor_01 },
                    { $"{Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_MENU.HEY_BABY"]}", ClubDanceFloor_02 },
                    { $"{Globals.JsonReader!["NIGHTCLUB_EDEN.DANCE_FLOOR_MENU.KEEP_DANCING"]}", ClubDanceFloor_03 },
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
                { $"{Globals.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_MENU.WHATS_IT_ABOUT"]}", LunaMeeting_01 },
                { $"{Globals.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_MENU.LET_HER_SPEAK"]}", LunaMeeting_02 }
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

                Display.Write($"\n\n\t{Globals.JsonReader!["ITEM_GAINED"]}");
                Console.ForegroundColor = ConsoleColor.Green;
                Display.Write($"200$\n\n");
                Console.ResetColor();

                Display.WriteDialogue($"\t{Globals.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_09"]}");
                Display.WriteDialogue($" {Globals.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_10"]}");
                Thread.Sleep(1000);
                Display.WriteDialogue($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.LUNA_MEETING_11"]}");
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
            Display.WriteNarration($"\t{Globals.JsonReader!["NIGHTCLUB_EDEN.UPSTAIRS_01"]}");

            Menu clubUpstairsMenu = new(new Dictionary<string, Action>()
            {
                { $"{Globals.JsonReader!["NIGHTCLUB_EDEN.UPSTAIRS_MENU.COME_CLOSER"]}", ClubUpstairs_01 },
                { $"{Globals.JsonReader!["NIGHTCLUB_EDEN.UPSTAIRS_MENU.GO_BACK"]}", ClubUpstairs_02 }
            });
        }

        public static void ClubUpstairs_01()
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["NIGHTCLUB_EDEN.UPSTAIRS_02"]}");
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["NIGHTCLUB_EDEN.UPSTAIRS_03"]}");
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
                Console.WriteLine();
                Display.WriteDialogue($"\t{Display.GetJsonString("NIGHTCLUB_EDEN.DIA_JET_15")}");
                return;
            }

            if (angerPoints > 2 && Program.Game!.StoryGlobals.Jet_WarnedPlayer && !Program.Game!.StoryGlobals.Jet_BeatedPlayer)
            {
                Program.Game!.StoryGlobals.Jet_BeatedPlayer = true;
                Program.Game!.StoryGlobals.Jet_Points = 0;
                Console.WriteLine();
                Globals.Npcs["Jet"].SetAttitude(Attitudes.Hostile);
                Console.WriteLine();
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
            Display.WriteNarration($"\n\t{Display.GetJsonString("NIGHTCLUB_EDEN.RIGHT_ACCES_CODE_01")}");
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
            Display.WriteNarration("\tYou lean over Hex and, in a fluid motion without hesitation, pull the stimulation helmet\n\toff his head. You witness the nightclub owner being shaken by a wave of convulsions. Foam\n\tbegins to come out of his mouth and after a moment the man freezes.");
            Thread.Sleep(1500);
            Globals.Npcs["HexFolstam"].Status = NpcStatus.Dead;
            Display.WriteNarration(" He's probably dead, just like\n\tLuna wanted.");
            LunaAppears();
        }

        public static void HexHideout_02()
        {
            Display.WriteNarration("\tYou are not a coward.");
            Thread.Sleep(1000);
            Display.WriteNarration(" You don't stab people in the back. That's not your style. You prefer an\n\topen fight.");
            Thread.Sleep(1500);
            Globals.Npcs["HexFolstam"].Status = NpcStatus.Normal;
            Display.WriteNarration(" After a while, you notice Hex moving slightly in his seat. He opens his eyes -\n\tthey are shining with the excitement of his online adventure.");
            Thread.Sleep(1000);
            Display.WriteNarration(" It is only a matter of time before\n\tHex notices your presence. There is no turning back now.");
            Display.WriteDialogue("\n\t- 'What the fuck are you doing here, dickhead?'");
            DialogueWithHex();
        }

        public static void DialogueWithHex()
        {
            Console.WriteLine();

            Menu dialogueWithHexMenu1 = new(new Dictionary<string, Action>()
            {
                { $"'Die!'", DialogueWithHex_01 },
                { $"'I don't want to fight with you.'", DialogueWithHex_02 },
                { $"'Luna wants you dead.'", DialogueWithHex_03 }
            });

            Menu dialogueWithHexMenu2 = new(new Dictionary<string, Action>()
            {
                { $"'It doesn't matter.'", DialogueWithHex_04 },
                { $"'Your girlfriend, Luna.'", DialogueWithHex_05 }
            });

            LunaAppears();
        }

        public static void LunaAppears()
        {
            Display.WriteNarration("\n\tThe passage behind your back is opened.");
            Thread.Sleep(1000);
            Display.WriteNarration(" You turn around, in front of you is Luna.");
            Display.WriteDialogue("\n\t- 'Did you do what I asked you to do...'");
            Thread.Sleep(1500);
            Display.WriteNarration("\n\tThe girl's gaze wanders from you to the body of her ex-boyfriend behind you.");
            Thread.Sleep(1000);
            Display.WriteDialogue("\n\t- 'You did it... You really did it... Is he - is he dead?'");
            Thread.Sleep(1000);
            Display.WriteNarration("\n\tLuna walks past you and kneels by the dead man. She starts searching his pockets for something.");

            Menu lunaAppearsMenu = new(new Dictionary<string, Action>()
            {
                { $"Do nothing.", DialogueWithHex_06 },
                { $"'What is this all about?'", DialogueWithHex_07 }
            });

            HexResurrection();
        }

        public static void DialogueWithHex_01()
        {
            Display.WriteNarration("\n\tWith a shout, you bring out your gun and aim it at Hex's chest.");
            Display.WriteDialogue("\n\t- 'Are you such a hero?");
            Thread.Sleep(1000);
            Display.WriteDialogue(" Would you kill an unarmed man?");
            Thread.Sleep(1000);
            Display.WriteDialogue(" All right, can I at least find out who\n\tsent you? I know you're not working alone. I'm seeing you for the first time in my life.'");
        }

        public static void DialogueWithHex_02()
        {
            Display.WriteNarration("\n\tYou are not a coward.");
            Thread.Sleep(1000);
            Display.WriteNarration(" You don't stab people in the back. That's not your style. You prefer an\n\topen fight.");
        }

        public static void DialogueWithHex_03()
        {
            Display.WriteNarration("\n\tYou are not a coward. You don't stab people in the back. That's not your style. You prefer an open fight.");
        }

        public static void DialogueWithHex_04()
        {
            Display.WriteDialogue("\n\t- 'Ugh, you little prick. I'll get you from beyond the grave. There will be no peace. I won't let that happen!'");
            Thread.Sleep(1500);
            HexDeath();
        }

        public static void DialogueWithHex_05()
        {
            Display.WriteDialogue("\n\t- 'Traitorous bitch! She'll get her due someday.");
            Display.WriteDialogue(" All right, shithead, let's get this over with.'");
            HexDeath();
        }

        public static void DialogueWithHex_06()
        {
            Display.WriteNarration("\n\tYou let the girl quietly plunder the corpse. This gives you more time to look at her shapes more closely.");
            Display.WriteDialogue("\n\t- 'Spare me, if you please.'");
        }

        public static void DialogueWithHex_07()
        {
            Display.WriteDialogue("\n\t- 'Well, now I think I owe you an explanation.'");
            Display.WriteNarration("\n\tA girl talks to you without even looking at you.");
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
                { $"{Globals.JsonReader!["NIGHTCLUB_EDEN.ENTER_MENU.GO_TO_DANCE_FLOOR"]}", ClubDanceFloor },
                { $"{Globals.JsonReader!["NIGHTCLUB_EDEN.ENTER_MENU.GO_TO_BAR"]}", ClubBar },
                { $"{Globals.JsonReader!["NIGHTCLUB_EDEN.ENTER_MENU.GO_UPSTAIRS"]}", ClubUpstairs },
                { $"{Globals.JsonReader!["NIGHTCLUB_EDEN.ENTER_MENU.BACK_ON_STREET"]}", PrologueEvents.VisitStreet },
            });
        }
    }
}
