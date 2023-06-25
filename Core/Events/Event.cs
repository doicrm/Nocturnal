using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Events
{
    public static class Event
    {
        public static void HeroDeath()
        {
            ClearInstances();
            Thread.Sleep(500);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Display.Write($"{Display.GetJsonString("YOU_ARE_DEAD")}");
            Thread.Sleep(1000);
            Console.ResetColor();
            Display.Write($"{Display.GetJsonString("BACK_TO_MENU")}", 25);
            Console.ReadKey();
            Console.Clear();
            Program.Game!.LoadLogo();
        }

        public static void GameOver()
        {
            ClearInstances();
            Thread.Sleep(500);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Display.Write($"{Display.GetJsonString("GAME_OVER")}", 25);
            Thread.Sleep(2000);
            Console.ResetColor();
            Console.Clear();
            Program.Game!.LoadLogo();
        }

        public static void ClearInstances()
        {
            Globals.Items.Clear();
            Globals.Npcs.Clear();
            Globals.Locations.Clear();
            Globals.Fractions.Clear();
            Globals.Quests.Clear();
        }

        public static void EndGame()
        {
            ClearInstances();
            Console.Clear();
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Display.Write($"\t{Display.GetJsonString("GAME_OVER")}\n\n", 25);
            Thread.Sleep(2000);
            Console.ResetColor();
            Display.Write($"\t{Display.GetJsonString("THANKS_FOR_PLAYING")}");
            Thread.Sleep(3500);
            Console.Clear();
            Program.Game!.End();
        }
    }
}
