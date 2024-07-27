using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Events.Prologue
{
    public static class MiscEvents
    {
        public static async Task LoadingFiles()
        {
            Console.ForegroundColor = ConsoleColor.White;
            await Display.Write("\t||", 100);

            for (int i = 0; i < 21; i++)
            {
                await Task.Delay(1000);
                await Display.Write("=", 100);
            }

            await Display.Write("||", 100);
            await Task.Delay(1000);
            await Display.Write($"\n\t{Globals.JsonReader!["MISC.DOWNLOAD_COMPLETED"]}");
            Console.ResetColor();
        }

        public static async Task NamingHero()
        {
            await Display.WriteNarration($"\t{Globals.JsonReader!["MISC.REMEMBER_YOUR_NAME"]}\n");
            Console.ResetColor();
            Globals.Player.Name = await Input.GetString();
            await SaveManager.UpdateSave();
        }
    }
}
