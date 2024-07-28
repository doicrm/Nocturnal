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

        public static async Task EnterGunShop()
        {
            await Display.WriteNarration($"\n\t{Display.GetJsonString("GUN_SHOP.ENTER_01")}");

            if (!Globals.Npcs["Zed"].IsKnowHero)
            {
                await Task.Delay(1000);
                await Display.WriteNarration($" {Display.GetJsonString("GUN_SHOP.ENTER_02")}");
                await Task.Delay(1500);
                await Display.WriteNarration($" {Display.GetJsonString("GUN_SHOP.ENTER_03")}");
                await Display.WriteDialogue($"\n\t{Display.GetJsonString("GUN_SHOP.ENTER_04")}");
                Globals.Npcs["Zed"].IsKnowHero = true;
                await DialogueWithZed();
                return;
            }

            await Task.Delay(1000);
            await Display.WriteNarration($" {Display.GetJsonString("GUN_SHOP.ENTER_05")}");

            if (Globals.Player.HasItem(Globals.Items["Pistol"]))
                await Display.WriteDialogue($"\n\t{Display.GetJsonString("GUN_SHOP.ENTER_06")}");

            await DialogueWithZed();
        }

        public static async Task DialogueWithZed()
        {
            Console.WriteLine();

            InteractiveMenu dialogueWithZedMenu = new();
            dialogueWithZedMenu.ClearOptions();
            Dictionary<string, Func<Task>> options = new()
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
            await dialogueWithZedMenu.InputChoice();
        }

        public static async Task DialogueWithZed_01()
        {
            await ZedTrade();
        }

        public static async Task DialogueWithZed_02()
        {
            if (Program.Game!.StoryGlobals.PC_TalkedAboutBusinessWithZed)
                await Display.WriteDialogue($"\t{Display.GetJsonString("GUN_SHOP.DIA_ZED_01")}");
            else
            {
                Program.Game!.StoryGlobals.PC_TalkedAboutBusinessWithZed = true;
                await Display.WriteDialogue($"\t{Display.GetJsonString("GUN_SHOP.DIA_ZED_02")}");
            }

            await DialogueWithZed();
        }

        public static async Task DialogueWithZed_03()
        {
            Program.Game!.StoryGlobals.Zed_KnowsAboutBobAndZed = true;
            await Display.WriteDialogue($"\t{Display.GetJsonString("GUN_SHOP.DIA_ZED_03")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($" {Display.GetJsonString("GUN_SHOP.DIA_ZED_04")}");
            await Globals.Npcs["Zed"].SetAttitude(Attitudes.Friendly);
            await DialogueWithZed();
        }

        public static async Task DialogueWithZed_04()
        {
            await ZedGetsAnAccelerator();
            await DialogueWithZed();
        }

        public static async Task DialogueWithZed_05()
        {
            await Display.WriteDialogue($"\t{Display.GetJsonString("GUN_SHOP.DIA_ZED_05")}\n");
            Console.Clear();

            if (!Globals.Npcs["Caden"].IsKnowHero && !Globals.Npcs["CadensPartner"].IsKnowHero)
                await StreetEvents.MeetingWithPolicemans();
            else
                await Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
        }

        public static async Task ZedGetsAnAccelerator()
        {
            await Display.WriteDialogue($"\t{Display.GetJsonString("GUN_SHOP.DIA_ZED_06")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("GUN_SHOP.DIA_ZED_07")}");
            await Globals.Player.RemoveItem(Globals.Items["AD13"]);
            Globals.Player.Weapon = (Weapon)Globals.Items["Pistol"];
            await Globals.Player.AddItem(Globals.Items["Pistol"]);

            await Display.Write($"\n\n\t{Display.GetJsonString("ITEM_GIVEN")}");
            Console.ForegroundColor = ConsoleColor.Blue;
            await Display.Write(Globals.Items["AD13"].Name!);
            Console.ResetColor();
            await Display.Write($"\n\t{Display.GetJsonString("ITEM_GAINED")}");
            Console.ForegroundColor = ConsoleColor.Blue;
            await Display.Write($"{Globals.Items["Pistol"].Name!}\n\n");
            Console.ResetColor();
            await Globals.Player.EndQuest(Globals.Quests["ZedAccelerator"], QuestStatus.Success);
            await DialogueWithZed();
        }

        public static async Task ZedTrade()
        {
            if (Globals.Player.HasItem(Globals.Items["Pistol"]))
            {
                Console.ResetColor();
                await Display.Write($"\t{Display.GetJsonString("GUN_SHOP.DIA_ZED_13")}");
                await DialogueWithZed();
                return;
            }

            if (!Program.Game!.StoryGlobals.Zed_TellsAboutWeapons)
            {
                Program.Game!.StoryGlobals.Zed_TellsAboutWeapons = true;
                await Display.WriteDialogue($"\t{Display.GetJsonString("GUN_SHOP.DIA_ZED_08")}");
                await Task.Delay(1000);
                await Display.WriteDialogue($" {Display.GetJsonString("GUN_SHOP.DIA_ZED_09")} ");
                await Task.Delay(1000);
                await Display.WriteDialogue($"{Display.GetJsonString("GUN_SHOP.DIA_ZED_10")}");
                await Task.Delay(1500);
                await Display.WriteDialogue($" {Display.GetJsonString("GUN_SHOP.DIA_ZED_11")}");
                await Task.Delay(1500);
                await Display.WriteDialogue($" {Display.GetJsonString("GUN_SHOP.DIA_ZED_12")}");

                _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
                {
                    { Display.GetJsonString("GUN_SHOP.BUY_PISTOL_MENU.BUY_IT"), BuyPistol },
                    { Display.GetJsonString("GUN_SHOP.BUY_PISTOL_MENU.MADE_UP_MIND"), ZedTrade_01 }
                });

                await DialogueWithZed();
                return;
            }

            Console.WriteLine();

            _ = new InteractiveMenu(new Dictionary<string, Func<Task>>
            {
                { Display.GetJsonString("GUN_SHOP.BUY_PISTOL_MENU.BUY_IT"), BuyPistol },
                { Display.GetJsonString("GUN_SHOP.BUY_PISTOL_MENU.MADE_UP_MIND"), ZedTrade_01 }
            });

            await DialogueWithZed();
        }

        public static async Task ZedTrade_01()
        {
            await Display.WriteNarration($"\t{Display.GetJsonString("GUN_SHOP.DIA_ZED_14")}");
            await Display.WriteDialogue($"\n\t{Display.GetJsonString("GUN_SHOP.DIA_ZED_15")}");
            await DialogueWithZed();
        }

        public static async Task BuyPistol()
        {
            if (Globals.Player.Money <= 250.0f)
            {
                await Display.WriteDialogue($"\t{Display.GetJsonString("GUN_SHOP.DIA_ZED_16")}");
                await Task.Delay(1000);
                await Display.WriteDialogue($" {Display.GetJsonString("GUN_SHOP.DIA_ZED_17")}");
                await Task.Delay(1500);

                if (Program.Game!.StoryGlobals.Zed_KnowsAboutBobAndZed)
                {
                    await Display.WriteDialogue($" {Display.GetJsonString("GUN_SHOP.DIA_ZED_18")}");
                    await Task.Delay(1000);
                    await Display.WriteDialogue($" {Display.GetJsonString("GUN_SHOP.DIA_ZED_19")}");
                    await Globals.Player.AddItem(Globals.Items["Pistol"]);
                    Globals.Player.Weapon = (Weapon)Globals.Items["Pistol"];

                    await Display.Write($"\n\n\t{Display.GetJsonString("ITEM_GAINED")}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    await Display.Write($"{Globals.Items["Pistol"].Name!}\n\n");
                    Console.ResetColor();
                }
                else
                {
                    await Display.WriteDialogue($" {Display.GetJsonString("GUN_SHOP.DIA_ZED_20")}");
                    await Task.Delay(1000);
                    await Display.WriteDialogue($" {Display.GetJsonString("GUN_SHOP.DIA_ZED_21")}");
                    await Task.Delay(1000);
                    await Display.WriteDialogue($" {Display.GetJsonString("GUN_SHOP.DIA_ZED_22")}");
                    await Task.Delay(1500);
                    await Display.WriteDialogue($" {Display.GetJsonString("GUN_SHOP.DIA_ZED_23")}");
                    await Globals.Player.AddQuest(Globals.Quests["ZedAccelerator"]);
                }
            }
            else
            {
                await Display.WriteDialogue($"\n\t{Display.GetJsonString("GUN_SHOP.DIA_ZED_24")}");
                await Globals.Player.AddItem(Globals.Items["Pistol"]);
                Globals.Player.Money -= 250.0f;

                await Display.Write($"\n\n\t{Display.GetJsonString("ITEM_BOUGHT")}");
                Console.ForegroundColor = ConsoleColor.Blue;
                await Display.Write($"{Globals.Items["Pistol"].Name!}");
                Console.ResetColor();
            }

            await DialogueWithZed();
        }

        public static async Task Crossroads()
        {
            await EnterGunShop();
        }
    }
}
