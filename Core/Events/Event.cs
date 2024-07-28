using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Events
{
    public static class Event
    {
        public static async Task HeroDeath()
        {
            await ClearInstances();
            await Task.Delay(500);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            await Display.Write($"{Display.GetJsonString("YOU_ARE_DEAD")}");
            await Task.Delay(1000);
            Console.ResetColor();
            await Display.Write($"{Display.GetJsonString("BACK_TO_MENU")}", 25);
            Console.ReadKey();
            Console.Clear();
            await Program.Game!.LoadLogo();
        }

        public static async Task GameOver()
        {
            await ClearInstances();
            await Task.Delay(500);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            await Display.Write($"{Display.GetJsonString("GAME_OVER")}", 25);
            await Task.Delay(2000);
            Console.ResetColor();
            Console.Clear();
            await Program.Game!.LoadLogo();
        }

        public static async Task ClearInstances()
        {
            await Task.Run(() =>
            {
                Globals.Items.Clear();
                Globals.Npcs.Clear();
                Globals.Locations.Clear();
                Globals.Fractions.Clear();
                Globals.Quests.Clear();
            });
        }

        public static async Task EndGame()
        {
            await ClearInstances();
            Console.Clear();
            await Task.Delay(500);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            await Display.Write($"\t{Display.GetJsonString("GAME_OVER")}\n\n", 25);
            await Task.Delay(2000);
            Console.ResetColor();
            await Display.Write($"\t{Display.GetJsonString("THANKS_FOR_PLAYING")}");
            await Task.Delay(3500);
            Console.Clear();
            await Program.Game!.End();
        }
    }
}
