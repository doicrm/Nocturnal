using Nocturnal.Core.Entitites;
using Nocturnal.Core.Entitites.Items;
using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Events.Prologue
{
    public static class GunShopEvents
    {
        // ************************************************************
        // 		ZED'S GUN SHOP
        // ************************************************************

        public static void EnterGunShop()
        {
            Display.WriteNarration($"\n\t{Globals.JsonReader!["GUN_SHOP.ENTER_01"]}");

            if (!Globals.Npcs["Zed"].IsKnowHero)
            {
                Thread.Sleep(1000);
                Display.WriteNarration($" {Globals.JsonReader!["GUN_SHOP.ENTER_02"]}");
                Thread.Sleep(1500);
                Display.WriteNarration($" {Globals.JsonReader!["GUN_SHOP.ENTER_03"]}");
                Display.WriteDialogue($"\n\t{Globals.JsonReader!["GUN_SHOP.ENTER_04"]}");
                Globals.Npcs["Zed"].IsKnowHero = true;
                DialogueWithZed();
            }
            else
            {
                Thread.Sleep(1000);
                Display.WriteNarration($" {Globals.JsonReader!["GUN_SHOP.ENTER_05"]}");

                if (Globals.Player.HasItem(Globals.Items["Pistol"]))
                    Display.WriteDialogue($"\n\t{Globals.JsonReader!["GUN_SHOP.ENTER_06"]}");

                DialogueWithZed();
            }
        }

        public static void DialogueWithZed()
        {
            Console.WriteLine();

            Menu dialogueWithZedMenu = new();
            dialogueWithZedMenu.ClearOptions();
            Dictionary<string, Action> options = new()
            {
                { Display.GetJsonString("GUN_SHOP.DIA_ZED_MENU.01"), DialogueWithZed_01 },
                { Display.GetJsonString("GUN_SHOP.DIA_ZED_MENU.02"), DialogueWithZed_02 }
            };

            if (Program.Game!.StoryGlobals.Bob_RecommendsZed && !Program.Game!.StoryGlobals.Zed_KnowsAboutBobAndZed)
                options.Add(Display.GetJsonString("GUN_SHOP.DIA_ZED_MENU.03"), DialogueWithZed_03);

            if (Globals.Quests["ZedAccelerator"].IsRunning && Globals.Player.HasItem(Globals.Items["AD13"]))
                options.Add(Display.GetJsonString("GUN_SHOP.DIA_ZED_MENU.04"), DialogueWithZed_04);

            options.Add(Display.GetJsonString("GUN_SHOP.DIA_ZED_MENU.05"), DialogueWithZed_05);
            dialogueWithZedMenu.AddOptions(options);
            dialogueWithZedMenu.ShowOptions();
            dialogueWithZedMenu.InputChoice();
        }

        public static void DialogueWithZed_01()
        {
            ZedTrade();
        }

        public static void DialogueWithZed_02()
        {
            if (Program.Game!.StoryGlobals.PC_TalkedAboutBusinessWithZed)
                Display.WriteDialogue($"\t{Display.GetJsonString("GUN_SHOP.DIA_ZED_01")}");
            else
            {
                Program.Game!.StoryGlobals.PC_TalkedAboutBusinessWithZed = true;
                Display.WriteDialogue($"\t{Display.GetJsonString("GUN_SHOP.DIA_ZED_02")}");
            }

            DialogueWithZed();
        }

        public static void DialogueWithZed_03()
        {
            Program.Game!.StoryGlobals.Zed_KnowsAboutBobAndZed = true;
            Display.WriteDialogue($"\t{Globals.JsonReader!["GUN_SHOP.DIA_ZED_03"]}");
            Thread.Sleep(1000);
            Display.WriteDialogue($" {Globals.JsonReader!["GUN_SHOP.DIA_ZED_04"]}");
            Globals.Npcs["Zed"].SetAttitude(Attitudes.Friendly);
            DialogueWithZed();
        }

        public static void DialogueWithZed_04()
        {
            ZedGetsAnAccelerator();
            DialogueWithZed();
        }

        public static void DialogueWithZed_05()
        {
            Display.WriteDialogue($"\t{Globals.JsonReader!["GUN_SHOP.DIA_ZED_05"]}\n");
            Console.Clear();

            if (!Globals.Npcs["Caden"].IsKnowHero && !Globals.Npcs["CadensPartner"].IsKnowHero)
                StreetEvents.MeetingWithPolicemans();
            else
                Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
        }

        public static void ZedGetsAnAccelerator()
        {
            Globals.Player.RemoveItem(Globals.Items["AD13"]);
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["GUN_SHOP.DIA_ZED_06"]}");
            Thread.Sleep(1000);
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["GUN_SHOP.DIA_ZED_07"]}");
            Globals.Player.AddItem(Globals.Items["Pistol"]);
            Globals.Player.Weapon = (Weapon)Globals.Items["Pistol"];

            Display.Write($"\n\n\t{Globals.JsonReader!["ITEM_GIVEN"]}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Display.Write(Globals.Items["AD13"].Name!);
            Console.ResetColor();
            Display.Write($"\n\t{Globals.JsonReader!["ITEM_GAINED"]}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Display.Write($"{Globals.Items["Pistol"].Name!}\n\n");
            Console.ResetColor();
            Globals.Player.EndQuest(Globals.Quests["ZedAccelerator"], QuestStatus.Success);
            DialogueWithZed();
        }

        public static void ZedTrade()
        {
            if (!Globals.Player.HasItem(Globals.Items["Pistol"]))
            {
                if (!Program.Game!.StoryGlobals.Zed_TellsAboutWeapons)
                {
                    Program.Game!.StoryGlobals.Zed_TellsAboutWeapons = true;
                    Display.WriteDialogue($"\t{Globals.JsonReader!["GUN_SHOP.DIA_ZED_08"]}");
                    Thread.Sleep(1000);
                    Display.WriteDialogue($" {Globals.JsonReader!["GUN_SHOP.DIA_ZED_09"]} ");
                    Thread.Sleep(1000);
                    Display.WriteDialogue($"{Globals.JsonReader!["GUN_SHOP.DIA_ZED_10"]}");
                    Thread.Sleep(1500);
                    Display.WriteDialogue($" {Globals.JsonReader!["GUN_SHOP.DIA_ZED_11"]}");
                    Thread.Sleep(1500);
                    Display.WriteDialogue($" {Globals.JsonReader!["GUN_SHOP.DIA_ZED_12"]}");

                    Menu buyPistolMenu = new(new Dictionary<string, Action>()
                    {
                        { $"{Globals.JsonReader!["GUN_SHOP.BUY_PISTOL_MENU.BUY_IT"]}", BuyPistol },
                        { $"{Globals.JsonReader!["GUN_SHOP.BUY_PISTOL_MENU.MADE_UP_MIND"]}", ZedTrade_01 }
                    });
                }
                else
                {
                    Console.WriteLine();

                    Menu buyPistolMenu = new(new Dictionary<string, Action>()
                    {
                        { $"{Globals.JsonReader!["GUN_SHOP.BUY_PISTOL_MENU.BUY_IT"]}", BuyPistol },
                        { $"{Globals.JsonReader!["GUN_SHOP.BUY_PISTOL_MENU.MADE_UP_MIND"]}", ZedTrade_01 }
                    });
                }
            }
            else
            {
                Console.ResetColor();
                Display.Write($"\t{Globals.JsonReader!["GUN_SHOP.DIA_ZED_13"]}");
            }
            DialogueWithZed();
        }

        public static void ZedTrade_01()
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["GUN_SHOP.DIA_ZED_14"]}");
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["GUN_SHOP.DIA_ZED_15"]}");
            DialogueWithZed();
        }

        public static void BuyPistol()
        {
            if (Globals.Player.Money <= 250.0f)
            {
                Display.WriteDialogue($"\t{Globals.JsonReader!["GUN_SHOP.DIA_ZED_16"]}");
                Thread.Sleep(1000);
                Display.WriteDialogue($" {Globals.JsonReader!["GUN_SHOP.DIA_ZED_17"]}");
                Thread.Sleep(1500);

                if (Program.Game!.StoryGlobals.Zed_KnowsAboutBobAndZed)
                {
                    Display.WriteDialogue($" {Globals.JsonReader!["GUN_SHOP.DIA_ZED_18"]}");
                    Thread.Sleep(1000);
                    Display.WriteDialogue($" {Globals.JsonReader!["GUN_SHOP.DIA_ZED_19"]}");
                    Globals.Player.AddItem(Globals.Items["Pistol"]);
                    Globals.Player.Weapon = (Weapon)Globals.Items["Pistol"];

                    Display.Write($"\n\n\t{Globals.JsonReader!["ITEM_GAINED"]}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Display.Write($"{Globals.Items["Pistol"].Name!}\n\n");
                    Console.ResetColor();
                }
                else
                {
                    Display.WriteDialogue($" {Globals.JsonReader!["GUN_SHOP.DIA_ZED_20"]}");
                    Thread.Sleep(1000);
                    Display.WriteDialogue($" {Globals.JsonReader!["GUN_SHOP.DIA_ZED_21"]}");
                    Thread.Sleep(1000);
                    Display.WriteDialogue($" {Globals.JsonReader!["GUN_SHOP.DIA_ZED_22"]}");
                    Thread.Sleep(1500);
                    Display.WriteDialogue($" {Globals.JsonReader!["GUN_SHOP.DIA_ZED_23"]}");
                    Globals.Player.AddQuest(Globals.Quests["ZedAccelerator"]);
                }
            }
            else
            {
                Display.WriteDialogue($"\n\t{Globals.JsonReader!["GUN_SHOP.DIA_ZED_24"]}");
                Globals.Player.AddItem(Globals.Items["Pistol"]);
                Globals.Player.Money -= 250.0f;

                Display.Write($"\n\n\t{Globals.JsonReader!["ITEM_BOUGHT"]}");
                Console.ForegroundColor = ConsoleColor.Blue;
                Display.Write($"{Globals.Items["Pistol"].Name!}");
                Console.ResetColor();
            }

            DialogueWithZed();
        }

        public static void Crossroads()
        {
            EnterGunShop();
        }
    }
}
