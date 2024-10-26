using Nocturnal.core;
using Nocturnal.services;
using Nocturnal.ui;

namespace Nocturnal.events
{
    public static class Event
    {
        public static async Task HeroDeath()
        {
            await Globals.ClearInstances();
            await Task.Delay(500);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            await Display.Write($"{Localizator.GetString("YOU_ARE_DEAD")}");
            await Task.Delay(1000);
            Console.ResetColor();
            await Display.Write($"{Localizator.GetString("BACK_TO_MENU")}", 25);
            Console.ReadKey();
            Console.Clear();
            await Display.LoadLogo();
        }

        public static async Task GameOver()
        {
            await Globals.ClearInstances();
            await Task.Delay(500);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            await Display.Write($"{Localizator.GetString("GAME_OVER")}", 25);
            await Task.Delay(2000);
            Console.ResetColor();
            Console.Clear();
            await Display.LoadLogo();
        }

        public static async Task EndGame()
        {
            await Globals.ClearInstances();
            Console.Clear();
            await Task.Delay(500);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            await Display.Write($"\t{Localizator.GetString("GAME_OVER")}\n\n", 25);
            await Task.Delay(2000);
            Console.ResetColor();
            await Display.Write($"\t{Localizator.GetString("THANKS_FOR_PLAYING")}");
            await Task.Delay(3500);
            Console.Clear();
            await Game.Instance.End();
        }
    }
}
