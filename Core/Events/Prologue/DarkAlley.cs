using Nocturnal.Core.Entitites;
using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Events.Prologue
{
    public static class DarkAlleyEvents
    {
        // ************************************************************
        // 		DARK ALLEY in 'Eden' nigthclub area
        // ************************************************************

        public static void WakeUp()
        {
            Thread.Sleep(2000);
            Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_01"]}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.WAKE_UP_02"]}");
            Thread.Sleep(1500);
            Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_03"]}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.WAKE_UP_04"]}");
            Thread.Sleep(3000);
            Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_05"]}");
            Thread.Sleep(500);
            Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.WAKE_UP_06"]}");
            Thread.Sleep(3000);
            Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_07"]}");
            Thread.Sleep(1000);
            Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.WAKE_UP_08"]}");
            Thread.Sleep(2500);
            Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_09"]}");

            Menu wakeUpMenu = new(new Dictionary<string, Action>()
            {
                { $"{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_MENU.LOOK_OUT"]}", SearchGarbage },
                { $"{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_MENU.FIND_EXIT"]}", OutOfAlley }
            });
        }

        public static void SearchGarbage()
        {
            AcceleratorFinding();
            Console.WriteLine();
            OutOfAlley();
        }

        public static void AcceleratorFinding()
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["DARK_ALLEY.FINDING_ACCELERATOR_01"]}");
            Thread.Sleep(1500);
            Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.FINDING_ACCELERATOR_02"]}");
            Thread.Sleep(2000);
            Globals.Player.Money += 5.0f;
            Globals.Player.AddItem(Globals.Items["AD13"]);

            Display.Write($"\n\n\t{Globals.JsonReader!["ITEM_FOUND"]}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Display.Write(Globals.Items["AD13"].Name!);
            Console.ResetColor();
            Display.Write($" {Globals.JsonReader!["AND"]} ");
            Console.ForegroundColor = ConsoleColor.Green;
            Display.Write("5$");
            Console.ResetColor();

            Display.Write($"\n\t{Globals.JsonReader!["INVENTORY.TIP"]}\n", 15);
            Thread.Sleep(4000);
            Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.FINDING_ACCELERATOR_03"]}");
            Thread.Sleep(1000);
        }

        public static void OutOfAlley()
        {
            if (!Globals.Player.HasItem(Globals.Items["AD13"]))
            {
                Display.WriteNarration($"\t{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_01"]}");
                Thread.Sleep(1500);
                Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_02"]}");
                Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_03"]}");
            }
            else
            {
                Display.WriteNarration($"\t{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_04"]}");
            }

            Thread.Sleep(3000);
            Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_05"]}");
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_02"]}");

            Menu outOfAlleyMenu = new(new Dictionary<string, Action>()
            {
                { $"{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_MENU.FIND_OUT"]}", OutOfAlley_01 },
                { $"{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_MENU.IGNORE_STRANGER"]}", OutOfAlley_02 }
            });
        }

        public static void OutOfAlley_01()
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_06"]}");
            Thread.Sleep(1500);
            Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_07"]}");
            Thread.Sleep(1000);
            DialogueWithBob();
        }

        public static void OutOfAlley_02()
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_08"]}");
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_03"]}");
            Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_09"]}");
            Thread.Sleep(1000);
            Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
        }

        public static void DialogueWithBob()
        {
            Display.WriteDialogue($"\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_04"]}\n");

            if (!Globals.Npcs["Bob"].IsKnowHero && !Globals.Npcs["Caden"].IsKnowHero && !Globals.Npcs["CadensPartner"].IsKnowHero)
            {
                Menu heroNameMenu = new(new Dictionary<string, Action>()
                {
                    { $"{Globals.JsonReader!["DARK_ALLEY.HERO_NAME_MENU.INTRODUCE_YOURSELF"]}", DialogueWithBob_01 },
                    { $"{Globals.JsonReader!["DARK_ALLEY.HERO_NAME_MENU.KEEP_YOUR_INDETIFY"]}", DialogueWithBob_02 }
                });

                Menu diaBobMenu1 = new(new Dictionary<string, Action>()
                {
                    { $"{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_MENU_01.DONT_WANT_TROUBLE"]}", DialogueWithBob_03 },
                    { $"{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_MENU_01.LUST_LOOKING_AROUND"]}", DialogueWithBob_04 },
                    { $"{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_MENU_01.ITS_NOT_YOUR_BUSINESS"]}", DialogueWithBob_05 }
                });

                if (!Globals.Locations["Street"].IsVisited)
                {
                    Display.WriteNarration($"\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_20"]}");
                    Thread.Sleep(1000);
                    Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.DIA_BOB_21"]}");
                    Thread.Sleep(2000);
                    Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_22"]}");
                    Thread.Sleep(1000);
                    Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.DIA_BOB_23"]}");
                    Display.WriteDialogue($"\n\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_24"]}");
                    Thread.Sleep(500);
                    Display.WriteDialogue($"{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_25"]}");
                    Thread.Sleep(500);
                    Display.WriteDialogue($" {Globals.JsonReader!["DARK_ALLEY.DIA_BOB_26"]}");
                    Thread.Sleep(1500);
                    Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_27"]}");
                    Thread.Sleep(1500);
                    Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.DIA_BOB_28"]}");
                    Thread.Sleep(3500);
                    Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
                }
                else
                {
                    Display.WriteDialogue($"\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_29"]}");
                    Thread.Sleep(1500);
                    Display.WriteDialogue($" {Globals.JsonReader!["DARK_ALLEY.DIA_BOB_30"]}");

                    if (Globals.Npcs["Bob"].Attitude == Attitudes.Angry
                    || Globals.Npcs["Bob"].Attitude == Attitudes.Hostile)
                    {
                        Display.WriteDialogue($" {Globals.JsonReader!["DARK_ALLEY.DIA_BOB_31"]}");
                    }
                    else
                    {
                        Display.WriteDialogue($" {Globals.JsonReader!["DARK_ALLEY.DIA_BOB_32"]}");
                    }

                    Thread.Sleep(1500);
                    Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_27"]}");
                    Thread.Sleep(1500);
                    Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.DIA_BOB_28"]}");
                    Game.Pause();
                    Console.Clear();
                    Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
                }
            }
        }

        public static void DialogueWithBob_01()
        {
            Console.WriteLine();
            MiscEvents.NamingHero();
            Globals.Npcs["Bob"].IsKnowHero = true;
            Display.WriteDialogue($"\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_05"]} {Globals.Player.Name}{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_06"]}");
            Thread.Sleep(1500);
            Display.WriteDialogue($" {Globals.JsonReader!["DARK_ALLEY.DIA_BOB_07"]} {Globals.Npcs["Bob"].Name}.");
            Thread.Sleep(1500);
            Display.WriteDialogue($" {Globals.JsonReader!["DARK_ALLEY.DIA_BOB_08"]}");
            Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_01"]}");
        }

        public static void DialogueWithBob_02()
        {
            Globals.Npcs["Bob"].SetAttitude(Attitudes.Angry);
            Thread.Sleep(500);
            Display.WriteDialogue($"\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_09"]}");
        }

        public static void DialogueWithBob_03()
        {
            Program.Game!.StoryGlobals.Bob_RecommendsZed = true; // Bob recommends Zed's gun shop to the hero
            Display.WriteDialogue($"\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_10"]}");
            Thread.Sleep(1000);
            Display.WriteDialogue($" {Globals.JsonReader!["DARK_ALLEY.DIA_BOB_11"]}");
            Thread.Sleep(1000);
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_12"]}");
        }

        public static void DialogueWithBob_04()
        {
            Display.WriteDialogue($"\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_13"]}");
            Thread.Sleep(1000);
            Display.WriteDialogue($" {Globals.JsonReader!["DARK_ALLEY.DIA_BOB_14"]}");
            Thread.Sleep(1500);
            Display.WriteDialogue($" {Globals.JsonReader!["DARK_ALLEY.DIA_BOB_15"]}");
            Thread.Sleep(1500);
            Display.WriteDialogue($" {Globals.JsonReader!["DARK_ALLEY.DIA_BOB_16"]}");
            Thread.Sleep(1000);
            Display.WriteDialogue($" {Globals.JsonReader!["DARK_ALLEY.DIA_BOB_17"]}");
            AboutParadiseLost();
        }

        public static void DialogueWithBob_05()
        {
            Globals.Npcs["Bob"].SetAttitude(Attitudes.Angry);
            Thread.Sleep(500);
            Display.WriteDialogue($"\t{Globals.JsonReader!["DARK_ALLEY.DIA_BOB_18"]}");
        }

        public static void AboutParadiseLost()
        {
            Console.WriteLine();

            Menu paradiseLostMenu = new(new Dictionary<string, Action>()
            {
                { $"{Globals.JsonReader!["DARK_ALLEY.PARADISE_LOST_MENU.TELL_ME_MORE"]}", AboutParadiseLost_01 },
                { $"{Globals.JsonReader!["DARK_ALLEY.PARADISE_LOST_MENU.REMEMBER_SOMETHING"]}", AboutParadiseLost_02 }
            });
        }

        public static void AboutParadiseLost_01()
        {
            Display.WriteDialogue($"\t{Globals.JsonReader!["DARK_ALLEY.ABOUT_PARADISE_LOST_01"]}");
            Thread.Sleep(500);
            Display.WriteDialogue($" {Globals.JsonReader!["DARK_ALLEY.ABOUT_PARADISE_LOST_02"]}");
            Thread.Sleep(1000);
            Display.WriteDialogue($" {Globals.JsonReader!["DARK_ALLEY.ABOUT_PARADISE_LOST_03"]}");
            Thread.Sleep(1000);
            Display.WriteDialogue($" {Globals.JsonReader!["DARK_ALLEY.ABOUT_PARADISE_LOST_04"]}");
            Thread.Sleep(1000);
            Display.WriteDialogue($"\n\t{Globals.JsonReader!["DARK_ALLEY.ABOUT_PARADISE_LOST_05"]}");
            Thread.Sleep(1500);
            Display.WriteDialogue($" {Globals.JsonReader!["DARK_ALLEY.ABOUT_PARADISE_LOST_06"]}");
        }

        public static void AboutParadiseLost_02()
        {
        }

        public static void Crossroads()
        {
            if (!Globals.Npcs["Bob"].IsKnowHero)
            {
                Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.CROSSROADS_01"]}");
                Display.WriteDialogue($"\n\t{Globals.JsonReader!["DARK_ALLEY.CROSSROADS_02"]}");

                Menu outOfAlleyMenu = new(new Dictionary<string, Action>()
                {
                    { $"{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_MENU.FIND_OUT_FINALLY"]}", OutOfAlley_01 },
                    { $"{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_MENU.IGNORE_STRANGER"]}", OutOfAlley_02 }
                });
            }
            else
            {
                Display.WriteNarration($"\t{Globals.JsonReader!["DARK_ALLEY.CROSSROADS_03"]}");
                Thread.Sleep(1000);
                Display.WriteNarration($"{Globals.JsonReader!["DARK_ALLEY.WAKE_UP_08"]}");

                if (!Globals.Player.HasItem(Globals.Items["AD13"]))
                {
                    Thread.Sleep(1500);
                    Display.WriteNarration($" {Globals.JsonReader!["DARK_ALLEY.WAKE_UP_09"]}");

                    Menu wakeUpMenu = new(new Dictionary<string, Action>()
                    {
                        { $"{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_MENU.FIND_OUT_FINALLY"]}", Crossroads_01 },
                        { $"{Globals.JsonReader!["DARK_ALLEY.OUT_OF_ALLEY_MENU.IGNORE_STRANGER"]}", Crossroads_02 }
                    });
                }
                else
                {
                    Display.WriteNarration($"\n\t{Globals.JsonReader!["DARK_ALLEY.CROSSROADS_04"]}\n\n");
                    Game.Pause();
                    Console.Clear();
                    Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
                }
            }
        }

        public static void Crossroads_01()
        {
            AcceleratorFinding();
            Display.WriteNarration($"{Globals.JsonReader!["DARK_ALLEY.CROSSROADS_04"]}");
            Thread.Sleep(1500);
            Console.Clear();
            Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
        }

        public static void Crossroads_02()
        {
            Console.Clear();
            Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
        }
    }
}
