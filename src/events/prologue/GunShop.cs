﻿using Nocturnal.core;
using Nocturnal.entitites;
using Nocturnal.services;
using Nocturnal.ui;

namespace Nocturnal.events.prologue
{
    public static class GunShopEvents
    {
        ///////////////////////////////////////////////////////////////////////
        //	ZED'S GUN SHOP
        ///////////////////////////////////////////////////////////////////////

        public static async Task EnterGunShop()
        {
            await Display.WriteNarration($"\n\t{LocalizationService.GetString("GUN_SHOP.ENTER_01")}");

            if (!Globals.Npcs["Zed"].IsKnowHero)
            {
                await Task.Delay(1000);
                await Display.WriteNarration($" {LocalizationService.GetString("GUN_SHOP.ENTER_02")}");
                await Task.Delay(1500);
                await Display.WriteNarration($" {LocalizationService.GetString("GUN_SHOP.ENTER_03")}");
                await Display.WriteDialogue($"\n\t{LocalizationService.GetString("GUN_SHOP.ENTER_04")}");
                Globals.Npcs["Zed"].IsKnowHero = true;
                await DialogueWithZed();
                return;
            }

            await Task.Delay(1000);
            await Display.WriteNarration($" {LocalizationService.GetString("GUN_SHOP.ENTER_05")}");

            if (Globals.Player.HasItem(Globals.Items["Pistol"]))
                await Display.WriteDialogue($"\n\t{LocalizationService.GetString("GUN_SHOP.ENTER_06")}");

            await DialogueWithZed();
        }

        private static async Task DialogueWithZed()
        {
            Console.WriteLine();

            InteractiveMenu dialogueWithZedMenu = new();
            dialogueWithZedMenu.ClearOptions();
            var options = new MenuOptions()
            {
                { LocalizationService.GetString("GUN_SHOP.DIA_ZED_MENU.01"), DialogueWithZed_01 },
                { LocalizationService.GetString("GUN_SHOP.DIA_ZED_MENU.02"), DialogueWithZed_02 }
            };

            if (Game.Instance.StoryGlobals.Bob_RecommendsZed && !Game.Instance.StoryGlobals.Zed_KnowsAboutBobAndZed)
                options.Add(LocalizationService.GetString("GUN_SHOP.DIA_ZED_MENU.03"), DialogueWithZed_03);

            if (Globals.Quests["ZedAccelerator"].IsRunning && Globals.Player.HasItem(Globals.Items["AD13"]))
                options.Add(LocalizationService.GetString("GUN_SHOP.DIA_ZED_MENU.04"), DialogueWithZed_04);

            options.Add(LocalizationService.GetString("GUN_SHOP.DIA_ZED_MENU.05"), DialogueWithZed_05);
            dialogueWithZedMenu.AddOptions(options);
            dialogueWithZedMenu.ShowOptions();
            await dialogueWithZedMenu.InputChoice();
        }

        private static async Task DialogueWithZed_01() {
            await ZedTrade();
        }

        private static async Task DialogueWithZed_02()
        {
            if (Game.Instance.StoryGlobals.PC_TalkedAboutBusinessWithZed)
                await Display.WriteDialogue($"\t{LocalizationService.GetString("GUN_SHOP.DIA_ZED_01")}");
            else
            {
                Game.Instance.StoryGlobals.PC_TalkedAboutBusinessWithZed = true;
                await Display.WriteDialogue($"\t{LocalizationService.GetString("GUN_SHOP.DIA_ZED_02")}");
            }

            await DialogueWithZed();
        }

        private static async Task DialogueWithZed_03()
        {
            Game.Instance.StoryGlobals.Zed_KnowsAboutBobAndZed = true;
            await Display.WriteDialogue($"\t{LocalizationService.GetString("GUN_SHOP.DIA_ZED_03")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($" {LocalizationService.GetString("GUN_SHOP.DIA_ZED_04")}");
            await Globals.Npcs["Zed"].SetAttitude(Attitudes.Friendly);
            await DialogueWithZed();
        }

        private static async Task DialogueWithZed_04()
        {
            await ZedGetsAnAccelerator();
            await DialogueWithZed();
        }

        private static async Task DialogueWithZed_05()
        {
            await Display.WriteDialogue($"\t{LocalizationService.GetString("GUN_SHOP.DIA_ZED_05")}\n");
            Console.Clear();

            if (!Globals.Npcs["Caden"].IsKnowHero && !Globals.Npcs["CadensPartner"].IsKnowHero)
                await StreetEvents.MeetingWithPolicemans();
            else
                await Game.Instance.SetCurrentLocation(Globals.Locations["Street"]);
        }

        private static async Task ZedGetsAnAccelerator()
        {
            await Display.WriteDialogue($"\t{LocalizationService.GetString("GUN_SHOP.DIA_ZED_06")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($"\n\t{LocalizationService.GetString("GUN_SHOP.DIA_ZED_07")}");
            await Globals.Player.RemoveItem(Globals.Items["AD13"]);
            Globals.Player.Weapon = (Weapon)Globals.Items["Pistol"];
            await Globals.Player.AddItem(Globals.Items["Pistol"]);

            await Display.Write($"\n\n\t{LocalizationService.GetString("ITEM_GIVEN")}");
            Console.ForegroundColor = ConsoleColor.Blue;
            await Display.Write(Globals.Items["AD13"].Name!);
            Console.ResetColor();
            await Display.Write($"\n\t{LocalizationService.GetString("ITEM_GAINED")}");
            Console.ForegroundColor = ConsoleColor.Blue;
            await Display.Write($"{Globals.Items["Pistol"].Name!}\n\n");
            Console.ResetColor();
            await Globals.Player.EndQuest(Globals.Quests["ZedAccelerator"], QuestStatus.Success);
            await DialogueWithZed();
        }

        private static async Task ZedTrade()
        {
            if (Globals.Player.HasItem(Globals.Items["Pistol"]))
            {
                Console.ResetColor();
                await Display.Write($"\t{LocalizationService.GetString("GUN_SHOP.DIA_ZED_13")}");
                await DialogueWithZed();
                return;
            }

            if (!Game.Instance.StoryGlobals.Zed_TellsAboutWeapons)
            {
                Game.Instance.StoryGlobals.Zed_TellsAboutWeapons = true;
                await Display.WriteDialogue($"\t{LocalizationService.GetString("GUN_SHOP.DIA_ZED_08")}");
                await Task.Delay(1000);
                await Display.WriteDialogue($" {LocalizationService.GetString("GUN_SHOP.DIA_ZED_09")} ");
                await Task.Delay(1000);
                await Display.WriteDialogue($"{LocalizationService.GetString("GUN_SHOP.DIA_ZED_10")}");
                await Task.Delay(1500);
                await Display.WriteDialogue($" {LocalizationService.GetString("GUN_SHOP.DIA_ZED_11")}");
                await Task.Delay(1500);
                await Display.WriteDialogue($" {LocalizationService.GetString("GUN_SHOP.DIA_ZED_12")}");

                _ = new InteractiveMenu(new MenuOptions
                {
                    { LocalizationService.GetString("GUN_SHOP.BUY_PISTOL_MENU.BUY_IT"), BuyPistol },
                    { LocalizationService.GetString("GUN_SHOP.BUY_PISTOL_MENU.MADE_UP_MIND"), ZedTrade_01 }
                });

                await DialogueWithZed();
                return;
            }

            Console.WriteLine();

            _ = new InteractiveMenu(new MenuOptions
            {
                { LocalizationService.GetString("GUN_SHOP.BUY_PISTOL_MENU.BUY_IT"), BuyPistol },
                { LocalizationService.GetString("GUN_SHOP.BUY_PISTOL_MENU.MADE_UP_MIND"), ZedTrade_01 }
            });

            await DialogueWithZed();
        }

        private static async Task ZedTrade_01()
        {
            await Display.WriteNarration($"\t{LocalizationService.GetString("GUN_SHOP.DIA_ZED_14")}");
            await Display.WriteDialogue($"\n\t{LocalizationService.GetString("GUN_SHOP.DIA_ZED_15")}");
            await DialogueWithZed();
        }

        private static async Task BuyPistol()
        {
            if (Globals.Player.Money <= 250.0f)
            {
                await Display.WriteDialogue($"\t{LocalizationService.GetString("GUN_SHOP.DIA_ZED_16")}");
                await Task.Delay(1000);
                await Display.WriteDialogue($" {LocalizationService.GetString("GUN_SHOP.DIA_ZED_17")}");
                await Task.Delay(1500);

                if (Game.Instance.StoryGlobals.Zed_KnowsAboutBobAndZed)
                {
                    await Display.WriteDialogue($" {LocalizationService.GetString("GUN_SHOP.DIA_ZED_18")}");
                    await Task.Delay(1000);
                    await Display.WriteDialogue($" {LocalizationService.GetString("GUN_SHOP.DIA_ZED_19")}");
                    await Globals.Player.AddItem(Globals.Items["Pistol"]);
                    Globals.Player.Weapon = (Weapon)Globals.Items["Pistol"];

                    await Display.Write($"\n\n\t{LocalizationService.GetString("ITEM_GAINED")}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    await Display.Write($"{Globals.Items["Pistol"].Name!}\n\n");
                    Console.ResetColor();
                }
                else
                {
                    await Display.WriteDialogue($" {LocalizationService.GetString("GUN_SHOP.DIA_ZED_20")}");
                    await Task.Delay(1000);
                    await Display.WriteDialogue($" {LocalizationService.GetString("GUN_SHOP.DIA_ZED_21")}");
                    await Task.Delay(1000);
                    await Display.WriteDialogue($" {LocalizationService.GetString("GUN_SHOP.DIA_ZED_22")}");
                    await Task.Delay(1500);
                    await Display.WriteDialogue($" {LocalizationService.GetString("GUN_SHOP.DIA_ZED_23")}");
                    await Globals.Player.AddQuest(Globals.Quests["ZedAccelerator"]);
                }
            }
            else
            {
                await Display.WriteDialogue($"\n\t{LocalizationService.GetString("GUN_SHOP.DIA_ZED_24")}");
                await Globals.Player.AddItem(Globals.Items["Pistol"]);
                Globals.Player.Money -= 250.0f;

                await Display.Write($"\n\n\t{LocalizationService.GetString("ITEM_BOUGHT")}");
                Console.ForegroundColor = ConsoleColor.Blue;
                await Display.Write($"{Globals.Items["Pistol"].Name!}");
                Console.ResetColor();
            }

            await DialogueWithZed();
        }

        public static async Task Crossroads() {
            await EnterGunShop();
        }
    }
}
