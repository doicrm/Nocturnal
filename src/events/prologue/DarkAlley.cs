﻿using Nocturnal.core;
using Nocturnal.entitites;
using Nocturnal.services;
using Nocturnal.ui;

namespace Nocturnal.events.prologue
{
    public static class DarkAlleyEvents
    {
        ///////////////////////////////////////////////////////////////////////
        //	DARK ALLEY in 'Eden' nightclub area
        ///////////////////////////////////////////////////////////////////////

        public static async Task WakeUp()
        {
            await Task.Delay(2000);
            await Display.WriteNarration($"\n\t{LocalizationService.GetString("DARK_ALLEY.WAKE_UP_01")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {LocalizationService.GetString("DARK_ALLEY.WAKE_UP_02")}");
            await Task.Delay(1500);
            await Display.WriteNarration($"\n\t{LocalizationService.GetString("DARK_ALLEY.WAKE_UP_03")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {LocalizationService.GetString("DARK_ALLEY.WAKE_UP_04")}");
            await Task.Delay(3000);
            await Display.WriteNarration($"\n\t{LocalizationService.GetString("DARK_ALLEY.WAKE_UP_05")}");
            await Task.Delay(500);
            await Display.WriteNarration($" {LocalizationService.GetString("DARK_ALLEY.WAKE_UP_06")}");
            await Task.Delay(3000);
            await Display.WriteNarration($"\n\t{LocalizationService.GetString("DARK_ALLEY.WAKE_UP_07")}");
            await Task.Delay(1000);
            await Display.WriteNarration($" {LocalizationService.GetString("DARK_ALLEY.WAKE_UP_08")}");
            await Task.Delay(2500);
            await Display.WriteNarration($"\n\t{LocalizationService.GetString("DARK_ALLEY.WAKE_UP_09")}");

            _ = new InteractiveMenu(new MenuOptions
            {
                { LocalizationService.GetString("DARK_ALLEY.WAKE_UP_MENU.LOOK_OUT"), SearchGarbage },
                { LocalizationService.GetString("DARK_ALLEY.WAKE_UP_MENU.FIND_EXIT"), OutOfAlley }
            });
        }

        private static async Task SearchGarbage()
        {
            await AcceleratorFinding();
            Console.WriteLine();
            await OutOfAlley();
        }

        private static async Task AcceleratorFinding()
        {
            await Display.WriteNarration($"\t{LocalizationService.GetString("DARK_ALLEY.FINDING_ACCELERATOR_01")}");
            await Task.Delay(1500);
            await Display.WriteNarration($" {LocalizationService.GetString("DARK_ALLEY.FINDING_ACCELERATOR_02")}");
            await Task.Delay(2000);
            Globals.Player.Money += 5.0f;
            await Globals.Player.AddItem(Globals.Items["AD13"]);

            await Display.Write($"\n\n\t{LocalizationService.GetString("ITEM_FOUND")}");
            Console.ForegroundColor = ConsoleColor.Blue;
            await Display.Write(Globals.Items["AD13"].Name!);
            Console.ResetColor();
            await Display.Write($" {LocalizationService.GetString("AND")} ");
            Console.ForegroundColor = ConsoleColor.Green;
            await Display.Write("5$");
            Console.ResetColor();

            await Display.Write($"\n\t{LocalizationService.GetString("INVENTORY.TIP")}\n");
            await Task.Delay(4000);
            await Display.WriteNarration($"\n\t{LocalizationService.GetString("DARK_ALLEY.FINDING_ACCELERATOR_03")}");
            await Task.Delay(1000);
        }

        private static async Task OutOfAlley()
        {
            if (!Globals.Player.HasItem(Globals.Items["AD13"]))
            {
                await Display.WriteNarration($"\t{LocalizationService.GetString("DARK_ALLEY.OUT_OF_ALLEY_01")}");
                await Task.Delay(1500);
                await Display.WriteNarration($" {LocalizationService.GetString("DARK_ALLEY.OUT_OF_ALLEY_02")}");
                await Display.WriteNarration($" {LocalizationService.GetString("DARK_ALLEY.OUT_OF_ALLEY_03")}");
            }
            else
            {
                await Display.WriteNarration($"\t{LocalizationService.GetString("DARK_ALLEY.OUT_OF_ALLEY_04")}");
            }

            await Task.Delay(3000);
            await Display.WriteNarration($"\n\t{LocalizationService.GetString("DARK_ALLEY.OUT_OF_ALLEY_05")}");
            await Display.WriteDialogue($"\n\t{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_02")}");

            _ = new InteractiveMenu(new MenuOptions
            {
                { LocalizationService.GetString("DARK_ALLEY.OUT_OF_ALLEY_MENU.FIND_OUT"), OutOfAlley_01 },
                { LocalizationService.GetString("DARK_ALLEY.OUT_OF_ALLEY_MENU.IGNORE_STRANGER"), OutOfAlley_02 }
            });
        }

        private static async Task OutOfAlley_01()
        {
            await Display.WriteNarration($"\t{LocalizationService.GetString("DARK_ALLEY.OUT_OF_ALLEY_06")}");
            await Task.Delay(1500);
            await Display.WriteNarration($" {LocalizationService.GetString("DARK_ALLEY.OUT_OF_ALLEY_07")}");
            await Task.Delay(1000);
            await DialogueWithBob();
        }

        private static async Task OutOfAlley_02()
        {
            await Display.WriteNarration($"\t{LocalizationService.GetString("DARK_ALLEY.OUT_OF_ALLEY_08")}");
            await Display.WriteDialogue($"\n\t{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_03")}");
            await Display.WriteNarration($"\n\t{LocalizationService.GetString("DARK_ALLEY.OUT_OF_ALLEY_09")}");
            await Task.Delay(1000);
            await Game.Instance.SetCurrentLocation(Globals.Locations["Street"]);
        }

        private static async Task DialogueWithBob()
        {
            await Display.WriteDialogue($"\t{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_04")}\n");

            if (!Globals.Npcs["Bob"].IsKnowHero
            && !Globals.Npcs["Caden"].IsKnowHero
            && !Globals.Npcs["CadensPartner"].IsKnowHero)
            {
                _ = new InteractiveMenu(new MenuOptions
                {
                    { LocalizationService.GetString("DARK_ALLEY.HERO_NAME_MENU.INTRODUCE_YOURSELF"), DialogueWithBob_01 },
                    { LocalizationService.GetString("DARK_ALLEY.HERO_NAME_MENU.KEEP_YOUR_IDENTIFY"), DialogueWithBob_02 }
                });

                _ = new InteractiveMenu(new MenuOptions
                {
                    { LocalizationService.GetString("DARK_ALLEY.DIA_BOB_MENU_01.DONT_WANT_TROUBLE"), DialogueWithBob_03 },
                    { LocalizationService.GetString("DARK_ALLEY.DIA_BOB_MENU_01.LUST_LOOKING_AROUND"), DialogueWithBob_04 },
                    { LocalizationService.GetString("DARK_ALLEY.DIA_BOB_MENU_01.ITS_NOT_YOUR_BUSINESS"), DialogueWithBob_05 }
                });

                if (!Globals.Locations["Street"].IsVisited)
                {
                    await Display.WriteNarration($"\t{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_20")}");
                    await Task.Delay(1000);
                    await Display.WriteNarration($" {LocalizationService.GetString("DARK_ALLEY.DIA_BOB_21")}");
                    await Task.Delay(2000);
                    await Display.WriteNarration($"\n\t{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_22")}");
                    await Task.Delay(1000);
                    await Display.WriteNarration($" {LocalizationService.GetString("DARK_ALLEY.DIA_BOB_23")}");
                    await Display.WriteDialogue($"\n\t{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_24")}");
                    await Task.Delay(500);
                    await Display.WriteDialogue($"{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_25")}");
                    await Task.Delay(500);
                    await Display.WriteDialogue($" {LocalizationService.GetString("DARK_ALLEY.DIA_BOB_26")}");
                    await Task.Delay(1500);
                    await Display.WriteNarration($"\n\t{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_27")}");
                    await Task.Delay(1500);
                    await Display.WriteNarration($" {LocalizationService.GetString("DARK_ALLEY.DIA_BOB_28")}");
                    await Task.Delay(3500);
                }
                else
                {
                    await Display.WriteDialogue($"\t{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_29")}");
                    await Task.Delay(1500);
                    await Display.WriteDialogue($" {LocalizationService.GetString("DARK_ALLEY.DIA_BOB_30")}");

                    if (Globals.Npcs["Bob"].Attitude == Attitudes.Angry
                    || Globals.Npcs["Bob"].Attitude == Attitudes.Hostile)
                    {
                        await Display.WriteDialogue($" {LocalizationService.GetString("DARK_ALLEY.DIA_BOB_31")}");
                    }
                    else
                    {
                        await Display.WriteDialogue($" {LocalizationService.GetString("DARK_ALLEY.DIA_BOB_32")}");
                    }

                    await Task.Delay(1500);
                    await Display.WriteNarration($"\n\t{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_27")}");
                    await Task.Delay(1500);
                    await Display.WriteNarration($" {LocalizationService.GetString("DARK_ALLEY.DIA_BOB_28")}");
                    await Game.Pause();
                    Console.Clear();
                }

                await Game.Instance.SetCurrentLocation(Globals.Locations["Street"]);
            }
        }

        private static async Task DialogueWithBob_01()
        {
            Console.WriteLine();
            await MiscEvents.NamingHero();
            Globals.Npcs["Bob"].IsKnowHero = true;
            await Display.WriteDialogue($"\t{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_05")} {Globals.Player.Name}{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_06")}");
            await Task.Delay(1500);
            await Display.WriteDialogue($" {LocalizationService.GetString("DARK_ALLEY.DIA_BOB_07")} {Globals.Npcs["Bob"].Name}.");
            await Task.Delay(1500);
            await Display.WriteDialogue($" {LocalizationService.GetString("DARK_ALLEY.DIA_BOB_08")}");
            await Display.WriteNarration($"\n\t{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_01")}");
        }

        private static async Task DialogueWithBob_02()
        {
            await Globals.Npcs["Bob"].SetAttitude(Attitudes.Angry);
            await Task.Delay(500);
            await Display.WriteDialogue($"\t{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_09")}");
        }

        private static async Task DialogueWithBob_03()
        {
            Game.Instance.StoryGlobals.Bob_RecommendsZed = true; // Bob recommends Zed's gun shop to the hero
            await Display.WriteDialogue($"\t{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_10")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($" {LocalizationService.GetString("DARK_ALLEY.DIA_BOB_11")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($"\n\t{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_12")}");
        }

        private static async Task DialogueWithBob_04()
        {
            await Display.WriteDialogue($"\t{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_13")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($" {LocalizationService.GetString("DARK_ALLEY.DIA_BOB_14")}");
            await Task.Delay(1500);
            await Display.WriteDialogue($" {LocalizationService.GetString("DARK_ALLEY.DIA_BOB_15")}");
            await Task.Delay(1500);
            await Display.WriteDialogue($" {LocalizationService.GetString("DARK_ALLEY.DIA_BOB_16")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($" {LocalizationService.GetString("DARK_ALLEY.DIA_BOB_17")}");
            await AboutParadiseLost();
        }

        private static async Task DialogueWithBob_05()
        {
            await Globals.Npcs["Bob"].SetAttitude(Attitudes.Angry);
            await Task.Delay(500);
            await Display.WriteDialogue($"\t{LocalizationService.GetString("DARK_ALLEY.DIA_BOB_18")}");
        }

        private static async Task AboutParadiseLost()
        {
            await Task.Run(() =>
            {
                Console.WriteLine();

                _ = new InteractiveMenu(new MenuOptions
                {
                    { LocalizationService.GetString("DARK_ALLEY.PARADISE_LOST_MENU.TELL_ME_MORE"), AboutParadiseLost_01 },
                    { LocalizationService.GetString("DARK_ALLEY.PARADISE_LOST_MENU.REMEMBER_SOMETHING"), AboutParadiseLost_02 }
                });
            });
        }

        private static async Task AboutParadiseLost_01()
        {
            await Display.WriteDialogue($"\t{LocalizationService.GetString("DARK_ALLEY.ABOUT_PARADISE_LOST_01")}");
            await Task.Delay(500);
            await Display.WriteDialogue($" {LocalizationService.GetString("DARK_ALLEY.ABOUT_PARADISE_LOST_02")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($" {LocalizationService.GetString("DARK_ALLEY.ABOUT_PARADISE_LOST_03")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($" {LocalizationService.GetString("DARK_ALLEY.ABOUT_PARADISE_LOST_04")}");
            await Task.Delay(1000);
            await Display.WriteDialogue($"\n\t{LocalizationService.GetString("DARK_ALLEY.ABOUT_PARADISE_LOST_05")}");
            await Task.Delay(1500);
            await Display.WriteDialogue($" {LocalizationService.GetString("DARK_ALLEY.ABOUT_PARADISE_LOST_06")}");
        }

        private static async Task AboutParadiseLost_02() {
            await Task.Run(() => {});
        }

        public static async Task Crossroads()
        {
            if (!Globals.Npcs["Bob"].IsKnowHero)
            {
                await Display.WriteNarration($"\n\t{LocalizationService.GetString("DARK_ALLEY.CROSSROADS_01")}");
                await Display.WriteDialogue($"\n\t{LocalizationService.GetString("DARK_ALLEY.CROSSROADS_02")}");

                _ = new InteractiveMenu(new MenuOptions
                {
                    { LocalizationService.GetString("DARK_ALLEY.OUT_OF_ALLEY_MENU.FIND_OUT_FINALLY"), OutOfAlley_01 },
                    { LocalizationService.GetString("DARK_ALLEY.OUT_OF_ALLEY_MENU.IGNORE_STRANGER"), OutOfAlley_02 }
                });

                return;
            }

            await Display.WriteNarration($"\t{LocalizationService.GetString("DARK_ALLEY.CROSSROADS_03")}");
            await Task.Delay(1000);
            await Display.WriteNarration($"{LocalizationService.GetString("DARK_ALLEY.WAKE_UP_08")}");

            if (!Globals.Player.HasItem(Globals.Items["AD13"]))
            {
                await Task.Delay(1500);
                await Display.WriteNarration($" {LocalizationService.GetString("DARK_ALLEY.WAKE_UP_09")}");

                _ = new InteractiveMenu(new MenuOptions
                {
                    { LocalizationService.GetString("DARK_ALLEY.OUT_OF_ALLEY_MENU.FIND_OUT_FINALLY"), Crossroads_01 },
                    { LocalizationService.GetString("DARK_ALLEY.OUT_OF_ALLEY_MENU.IGNORE_STRANGER"), Crossroads_02 }
                });

                return;
            }

            await Display.WriteNarration($"\n\t{LocalizationService.GetString("DARK_ALLEY.CROSSROADS_04")}\n\n");
            await Game.Pause();
            Console.Clear();
            await Game.Instance.SetCurrentLocation(Globals.Locations["Street"]);
        }

        private static async Task Crossroads_01()
        {
            await AcceleratorFinding();
            await Display.WriteNarration($"{LocalizationService.GetString("DARK_ALLEY.CROSSROADS_04")}");
            await Task.Delay(1500);
            Console.Clear();
            await Game.Instance.SetCurrentLocation(Globals.Locations["Street"]);
        }

        private static async Task Crossroads_02()
        {
            Console.Clear();
            await Game.Instance.SetCurrentLocation(Globals.Locations["Street"]);
        }
    }
}
